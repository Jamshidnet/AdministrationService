using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Categories.Responses
{
    public  class TranslateCategoryResponse
    {
        public string TranslateText { get; set; }

        public string ColumnName { get; set; }

        public Guid? LangaugeId { get; set; }
    }
}
