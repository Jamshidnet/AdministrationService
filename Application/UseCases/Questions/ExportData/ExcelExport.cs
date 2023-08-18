using Application.UseCases.Questions.Responses;
using AutoMapper;
using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;
using System.Data;

namespace Application.UseCases.Questions.ExportData;


public class GetQuestionExcel : IRequest<ExcelReportResponse>
{
    public record GetQuestionExcelQuery(string FileName) : IRequest<ExcelReportResponse>;


    public class GetQuestionExcelHandler : IRequestHandler<GetQuestionExcelQuery, ExcelReportResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetQuestionExcelHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExcelReportResponse> Handle(GetQuestionExcelQuery request, CancellationToken cancellationToken)
        {
            using XLWorkbook wb = new();
            var orderData = await GetQuestionAsync(cancellationToken);
            var sheet1 = wb.AddWorksheet(orderData, "Questions");


            sheet1.Column(1).Style.Font.FontColor = XLColor.Red;

            sheet1.Columns(2, 4).Style.Font.FontColor = XLColor.Blue;

            sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;

            sheet1.Row(1).Style.Font.FontColor = XLColor.White;

            sheet1.Row(1).Style.Font.Bold = true;
            sheet1.Row(1).Style.Font.Shadow = true;
            sheet1.Row(1).Style.Font.Underline = XLFontUnderlineValues.Single;
            sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
            sheet1.Row(1).Style.Font.Italic = true;

            sheet1.RowHeight = 20;
            sheet1.Column(1).Width = 38;
            sheet1.Column(2).Width = 50;
            sheet1.Column(3).Width = 40;
            sheet1.Column(4).Width = 20;
            sheet1.Column(5).Width = 20;

            using MemoryStream ms = new();
            wb.SaveAs(ms);
            return new ExcelReportResponse(ms.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{request.FileName}.xlsx");
        }

        private async Task<DataTable> GetQuestionAsync(CancellationToken cancellationToken = default)
        {
            var AllQuestions = await _context.Questions.ToListAsync(cancellationToken);

            DataTable dt = new()
            {
                TableName = "Questions"
            };
             dt.Columns.Add("Id", typeof(Guid));
             dt.Columns.Add("Question Text", typeof(string));
             dt.Columns.Add("Creator Username", typeof(string));
             dt.Columns.Add("Category Name", typeof(string));
             dt.Columns.Add("Question type", typeof(string));


            var _list = _mapper.Map<List<GetListQuestionResponse>>(AllQuestions);
            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                     dt.Rows.Add(
                        item.Id,
                        item.QuestionText,
                        item.CreatorUser,
                        item.Category,
                        item.QuestionType
                        );
                });
            }

            return dt;
        }



    }
}

public record ExcelReportResponse(byte[] FileContents, string Option, string FileName);
