using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Categories.Responses
{
    public class UpdateCategoryTranslateResponse : CreateCategoryTranslateResponse
    {
        public Guid Id { get; set; }
    }
}
