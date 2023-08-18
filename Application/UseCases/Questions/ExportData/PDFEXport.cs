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
        using MemoryStream ms = new();
        Document document = new();
         document.SetMargins(0, 0, 40, 20); // Adjusted margins
         document.SetPageSize(PageSize.A4);

        PdfWriter writer = PdfWriter.GetInstance(document, ms);

        HeaderFooterHelper headerFooter = new();
        writer.PageEvent = headerFooter;

        document.Open();

        PdfPTable table = new(5);
        float[] columnWidths = { 6f, 3f, 1.5f, 1.5f, 1.5f }; // Adjusted column widths
        table.SetWidths(columnWidths);

        PdfPCell headerCell = new(new Phrase("Questions", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)))
        {
            Colspan = 5,
            HorizontalAlignment = Element.ALIGN_CENTER,
            BackgroundColor = BaseColor.LIGHT_GRAY
        }; // Increased font size
         table.AddCell(headerCell);
        table.CompleteRow();

        PdfPCell cell = new()
        {
            BackgroundColor = BaseColor.LIGHT_GRAY,
            HorizontalAlignment = Element.ALIGN_CENTER,

            Phrase = new Phrase("Id", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)) // Adjusted font size
        };
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

public class HeaderFooterHelper : PdfPageEventHelper
{
    public override void OnEndPage(PdfWriter writer, Document document)
    {
        PdfPTable footerTable = new(1)
        {
            TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin
        };
        footerTable.DefaultCell.Border = Rectangle.NO_BORDER;
        footerTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        footerTable.AddCell(new Phrase($"Date: {DateTime.Now:yyyy-MM-dd}", new Font(Font.FontFamily.HELVETICA, 8)));

         footerTable.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin, writer.DirectContent);
    }
}

public record PDFExportResponse(byte[] FileContents, string Options, string FileName);
