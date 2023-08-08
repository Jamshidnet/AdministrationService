using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.QuestionTypes.Responses
{
    public class GetLIstQuestionTypeResponse
    {
        public Guid Id { get; set; }

        public string QuestionTypeName { get; set; }
    }
}
