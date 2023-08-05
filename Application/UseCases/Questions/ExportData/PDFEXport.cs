using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;
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
                document.SetMargins(20, 20, 40, 40);
                document.SetPageSize(PageSize.A4);

                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                HeaderFooterHelper headerFooter = new HeaderFooterHelper();
                writer.PageEvent = headerFooter;

                document.Open();

                PdfPTable table = new PdfPTable(3);

                table.SetWidths(new float[] { 0.2f, 3f, 1f });
                PdfPCell headerCell = new PdfPCell(new Phrase("Questions", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                headerCell.Colspan = 5;
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(headerCell);
                table.CompleteRow();


                table.AddCell("QuestionText");
                table.AddCell("Category");
                table.CompleteRow();

                foreach (var question in _context.Questions)
                {
                    table.AddCell(question.QuestionText);
                    table.AddCell(_context.Categories.Find(question.CategoryId)?.CategoryName);
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
 