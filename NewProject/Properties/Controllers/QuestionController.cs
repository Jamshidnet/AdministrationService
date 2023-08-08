using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Application.UseCases.Questions.Queries;
using Application.UseCases.Questions.Responses;
using Application.UseCases.Questions.Commands;
using Application.UseCases.Questions.ExportData;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using static Application.UseCases.Questions.ExportData.GetQuestionExcel;
using Microsoft.AspNetCore.Authorization;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionController : ApiBaseController
{
    [HttpGet("[action]")]
    [Authorize(Roles ="GetQuestionById")]
    public async ValueTask<QuestionResponse> GetQuestionById(Guid QuestionId)
        => await _mediator.Send(new GetByIdQuestionQuery(QuestionId));

    [HttpGet("[action]")]
    [Authorize(Roles ="GetAllQuestion")]
    public async ValueTask<IEnumerable<GetListQuestionResponse>> GetAllQuestion(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<GetListQuestionResponse> query = (await _mediator
             .Send(new GetAllQuestionQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    [Authorize(Roles ="CreateQuestion")]
    public async ValueTask<Guid> CreateQuestion(CreateQuestionCommand command)
        => await _mediator.Send(command);


    [HttpPut("[action]")]
    [Authorize(Roles ="UpdateQuestion")]
    public async ValueTask<IActionResult> UpdateQuestion(UpdateQuestionCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    [Authorize(Roles ="DeleteQuestion")]
    public async ValueTask<IActionResult> DeleteQuestion(DeleteQuestionCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("[action]")]
    [Authorize(Roles ="ExportExcelQuestions")]
    public async Task<FileResult> ExportExcelQuestions(string fileName = "Questions")
    {
        var result = await _mediator.Send(new GetQuestionExcelQuery(fileName));
        return File(result.FileContents, result.Option, result.FileName);
    }


    [HttpGet("[action]")]
    [Authorize(Roles ="ExportPdfQuestions")]
    public async Task<FileResult> ExportPdfQuestions(string fileName = "orders")
    {
        var result = await _mediator.Send(new GetQuestionPDF(fileName,Guid.NewGuid()));
        return File(result.FileContents, result.Options, result.FileName);
    }


}