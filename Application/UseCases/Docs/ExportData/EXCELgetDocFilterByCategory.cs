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
       
        sheet1.Row(1).Style.Font.FontSize = 13;

        sheet1.RowHeight = 20;
        sheet1.Column(1).Width = 38;
        sheet1.Column(2).Width = 50;
        sheet1.Column(3).Width = 40;
        sheet1.Columns(4, _context.Categories.Count() + 4).Width = 20;
        using MemoryStream ms = new();
        wb.SaveAs(ms);
        return new ExcelReportResponse(ms.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{request.FileName}.xlsx");
    }

    private async Task<DataTable> GetDocsAsync(CancellationToken cancellationToken = default)
    {
        var docs = await _context.Docs.ToListAsync();

        var filteredDocs = docs.GroupBy(d => d.Client.Person.QuarterId).ToList();

        DataTable dt = new()
        {
            TableName = "Documents"
        };

        _ = dt.Columns.Add("Region Name", typeof(string));
        _ = dt.Columns.Add("District Name", typeof(string));
        _ = dt.Columns.Add("Quarter Name", typeof(string));
        var categories = await _context.Categories.ToListAsync(cancellationToken: cancellationToken);

        foreach (var item in categories)
        {
            _ = dt.Columns.Add($"{item.CategoryName} Category", typeof(long));
        }

        if (filteredDocs.Any())
        {
            filteredDocs.ForEach(item =>
            {
                Dictionary<string, int> counts = new();
                categories.ForEach(x =>
                {
                    var matchedDocs = item.Where(d => d.ClientAnswers
                        .Any(a => a.Question.CategoryId == x.Id)).ToList();

                    counts.Add(x.CategoryName, matchedDocs.Count);
                });

                DataRow row = dt.NewRow();

                row["Region Name"] = item.First().Client.Person.Quarter.District.Region.RegionName;
                row["District Name"] = item.First().Client.Person.Quarter.District.DistrictName;
                row["Quarter Name"] = item.First().Client.Person.Quarter.QuarterName;
                foreach (var count in counts)
                {
                    row[$"{count.Key} Category"] = (long)count.Value;
                }
                _ = dt.Rows.Add(row.ItemArray);
            });
        }

        return dt;
    }



}
