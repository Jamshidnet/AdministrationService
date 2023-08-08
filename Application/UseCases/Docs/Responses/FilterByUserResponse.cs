using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.UseCases.Docs.Responses
{
    [Keyless]
    public class FilterByUserResponse
    {
        [Column("region_name")]
        public string Region { get; set; }

        [Column("district_name")]
        public string District { get; set; }

        [Column("quarter_name")]
        public string  Quarter { get; set; }

        [Column("user_type_name")]
        public string UserType { get; set; }

        [Column("user_count")]
        public int TotalUsersSum { get; set; }

        [Column("doc_count")]
        public int DocCreatedUsersSum { get; set; }
    }
}
