using Application.UseCases.Questions.ExportData;
using AutoMapper;
using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;
using System.Data;

namespace Application.UseCases.Docs.ExportData;

public record GetDocsExcelQuery(string FileName) : IRequest<ExcelReportResponse>;


public class GetDocsExcelQueryHandler : IRequestHandler<GetDocsExcelQuery, ExcelReportResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDocsExcelQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ExcelReportResponse> Handle(GetDocsExcelQuery request, CancellationToken cancellationToken)
    {
        using XLWorkbook wb = new();
        var orderData = await GetDocsAsync(cancellationToken);
        var sheet1 = wb.AddWorksheet(orderData, "Questions");


        sheet1.Column(1).Style.Font.FontColor = XLColor.Red;

        sheet1.Columns(2, 4).Style.Font.FontColor = XLColor.Blue;

        sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;

        sheet1.Row(1).Style.Font.FontColor = XLColor.White;

        sheet1.Row(1).Style.Font.Bold = true;
        sheet1.Row(1).Style.Font.Shadow = true;
     //   sheet1.Row(1).Style.Font.Underline = XLFontUnderlineValues.Single;
       // sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
        sheet1.Row(1).Style.Font.FontSize = 13;

        sheet1.RowHeight = 20;
        sheet1.Column(1).Width = 38;
        sheet1.Column(2).Width = 50;
        sheet1.Column(3).Width = 40;
        sheet1.Column(4).Width = 20;
        sheet1.Column(5).Width = 20;
        sheet1.Columns(6, _context.Categories.Count() + 6).Width = 20;
        using MemoryStream ms = new();
        wb.SaveAs(ms);
        return new ExcelReportResponse(ms.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{request.FileName}.xlsx");
    }

    private async Task<DataTable> GetDocsAsync(CancellationToken cancellationToken = default)
    {
        var docs = await _context.Docs.ToListAsync();

        DataTable dt = new()
        {
            TableName = "Documents"
        };
        dt.Columns.Add("Id", typeof(Guid));
        dt.Columns.Add("User FullName", typeof(string));
        dt.Columns.Add("Client FullName", typeof(string));
        dt.Columns.Add("Device", typeof(string));
        dt.Columns.Add("Taken date", typeof(string));

        var categories = await _context.Categories.ToListAsync();

        foreach (var item in categories)
        {
            dt.Columns.Add($"{item.CategoryName} Category");
        }

        if (docs.Count > 0)
        {
            docs.ForEach( item =>
            {
                var answers =  item.ClientAnswers.GroupBy(x=>x.Question.CategoryId).ToList();

                Dictionary<string, int> counts = new();
                categories.ForEach(x =>
                {
                    bool HasValue = false;
                    foreach (var y in answers)
                    {
                        if (y.First().Question.CategoryId == x.Id)
                        {
                            counts.Add(x.CategoryName,y.Count()); HasValue = true;
                            break;
                        }
                    };
                    if (!HasValue) counts.Add(x.CategoryName, 0);
                });

                DataRow row = dt.NewRow();
                
                row["Id"] = item.Id;
                row["User FullName"] = $"{item.User.Person.FirstName} {item.User.Person.LastName}";
                row["Client FullName"] = $"{item.Client.Person.FirstName} {item.Client.Person.LastName}";
                row["Device"] = item.Device;
                row["Taken date"] = item.TakenDate;

                foreach (var count in counts)
                {
                    row[$"{count.Key} Category"] = count.Value;
                }
                    dt.Rows.Add(row.ItemArray);
            });
        }

        return dt;
    }



}
