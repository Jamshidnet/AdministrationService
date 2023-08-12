using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Questions.ExportData;


public record GetQuestionPDF(string FileName, Guid DocId) : IRequest<PDFExportResponse>;

public class GetQuestionPDFHandler : IRequestHandler<GetQuestionPDF, PDFExportResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuestionPDFHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PDFExportResponse> Handle(GetQuestionPDF request, CancellationToken cancellationToken)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            Document document = new Document();
            document.SetMargins(0, 0, 40, 20); // Adjusted margins
            document.SetPageSize(PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(document, ms);

            HeaderFooterHelper headerFooter = new HeaderFooterHelper();
            writer.PageEvent = headerFooter;

            document.Open();

            PdfPTable table = new PdfPTable(5);
            float[] columnWidths = { 6f, 3f, 1.5f, 1.5f, 1.5f }; // Adjusted column widths
            table.SetWidths(columnWidths);

            PdfPCell headerCell = new PdfPCell(new Phrase("Questions", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD))); // Increased font size
            headerCell.Colspan = 5;
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            table.AddCell(headerCell);
            table.CompleteRow();

            PdfPCell cell = new PdfPCell();
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;

            cell.Phrase = new Phrase("Id", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)); // Adjusted font size
            table.AddCell(cell);

            cell.Phrase = new Phrase("Question Text", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
            table.AddCell(cell);

            cell.Phrase = new Phrase("Question Type", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
            table.AddCell(cell);

            cell.Phrase = new Phrase("Creator User", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
            table.AddCell(cell);

            cell.Phrase = new Phrase("Category Name", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
            table.AddCell(cell);

            table.CompleteRow();

            var questions = await _context.Questions.ToListAsync();
            foreach (var question in questions)
            {
                table.AddCell(new Phrase(question.Id.ToString(), new Font(Font.FontFamily.HELVETICA, 10))); // Reduced font size
                table.AddCell(new Phrase(question.QuestionText, new Font(Font.FontFamily.HELVETICA, 10)));
                table.AddCell(new Phrase(question.QuestionType.QuestionTypeName, new Font(Font.FontFamily.HELVETICA, 10)));
                table.AddCell(new Phrase(question.CreatorUser.Username, new Font(Font.FontFamily.HELVETICA, 10)));
                table.AddCell(new Phrase(question.Category.CategoryName, new Font(Font.FontFamily.HELVETICA, 10)));
                table.CompleteRow();
            }

            document.Add(table);
            document.Close();

            return await Task.FromResult(new PDFExportResponse(ms.ToArray(), "application/pdf", request.FileName));
        }
    }
}

public class HeaderFooterHelper : PdfPageEventHelper
{
    public override void OnEndPage(PdfWriter writer, Document document)
    {
        PdfPTable footerTable = new PdfPTable(1);
        footerTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
        footerTable.DefaultCell.Border = Rectangle.NO_BORDER;
        footerTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        footerTable.AddCell(new Phrase($"Date: {DateTime.Now.ToString("yyyy-MM-dd")}", new Font(Font.FontFamily.HELVETICA, 8)));

        footerTable.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin, writer.DirectContent);
    }
}

public record PDFExportResponse(byte[] FileContents, string Options, string FileName);
