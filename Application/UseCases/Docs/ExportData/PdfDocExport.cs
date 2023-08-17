using Application.UseCases.Questions.ExportData;
using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Docs.ExportData;

public record GetDocPDF(string FileName, Guid DocId) : IRequest<PDFExportResponse>;
public class GenerateDocPDFHandler : IRequestHandler<GetDocPDF, PDFExportResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GenerateDocPDFHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PDFExportResponse> Handle(GetDocPDF request, CancellationToken cancellationToken)
    {
        var doc = await _context.Docs.FindAsync(request.DocId);

        using MemoryStream ms = new();
        Document document = new();
        _ = document.SetMargins(30, 30, 40, 40);
        _ = document.SetPageSize(PageSize.A4);

        PdfWriter writer = PdfWriter.GetInstance(document, ms);

        document.Open();

        Font sectionTitleFont = new(Font.FontFamily.HELVETICA, 14, Font.BOLD);
        Font regularFont = new(Font.FontFamily.HELVETICA, 12);

        Chunk title = new("Document Information", sectionTitleFont);
        Paragraph titleParagraph = new(title)
        {
            Alignment = Element.ALIGN_CENTER
        };
        _ = document.Add(titleParagraph);

        Chunk creatorUserChunk = new($"\n\nCreator User:\n" +
          $"    Username: {doc.User.Username}\n" +
          $"    First Name: {doc.User.Person.FirstName}\n" +
          $"    Last Name: {doc.User.Person.LastName}\n" +
          $"    Birthdate: {doc.User.Person.Birthdate}\n" +
          $"    Phone Number: {doc.User.Person.PhoneNumber}\n" +
          $"    Quarter: {doc.User.Person.Quarter.QuarterName}\n" +
          $"    Usser Type: {doc.User.UserType.TypeName}\n\n", regularFont);

        Paragraph creatorUserParagraph = new(creatorUserChunk);
        _ = document.Add(creatorUserParagraph);

        Chunk clientInfoChunk = new($"Client:\n" +
            $"  First Name: {doc.Client.Person.FirstName}\n" +
            $"  Last Name: {doc.Client.Person.LastName}\n" +
            $"  Birthdate: {doc.Client.Person.Birthdate}\n" +
            $"  Phone Number: {doc.Client.Person.PhoneNumber}\n" +
            $"  Quarter: {doc.Client.Person.Quarter}\n" +
            $"  Client Type: {doc.Client.ClientType.TypeName}\n\n", regularFont);

        Paragraph clientInfoParagraph = new(clientInfoChunk);
        _ = document.Add(clientInfoParagraph);

        Chunk takenDateChunk = new($"Taken Date: {doc.TakenDate}", regularFont);
        Paragraph takenDateParagraph = new(takenDateChunk);
        _ = document.Add(takenDateParagraph);

        Chunk locationChunk = new($"Location: Latitude - {doc.Latitude}, Longitude - {doc.Longitude}", regularFont);
        Paragraph locationParagraph = new(locationChunk);
        _ = document.Add(locationParagraph);

        Chunk deviceChunk = new($"Device: {doc.Device}", regularFont);
        Paragraph deviceParagraph = new(deviceChunk);
        _ = document.Add(deviceParagraph);

        Chunk answersTitleChunk = new("\n\n\nClient Answers:", sectionTitleFont);
        Paragraph answersTitleParagraph = new(answersTitleChunk);
        _ = document.Add(answersTitleParagraph);

        var answers = doc.ClientAnswers.GroupBy(x => x.Question.CategoryId);
        foreach (var item in answers)
        {
            Chunk category = new($"\nCategory: {item.First().Question.Category.CategoryName}", regularFont);
            Paragraph categorypar = new(category);
            _ = document.Add(categorypar);
            foreach (var answer in item)
            {
                Chunk answerTextChunk = new($"     Question: {answer.Question.QuestionText}      Answer: {answer.AnswerText}", regularFont);
                Paragraph answerParagraph = new(answerTextChunk);
                _ = document.Add(answerParagraph);
            }
        }
        document.Close();

        return await Task.FromResult(new PDFExportResponse(ms.ToArray(), "application/pdf", request.FileName));
    }
}
