
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace KZDotNetCore.Domain.Database
{
    [Table("Tbl_Todos")]
    public class TodoDataModel
    {
        [Key]
        [Column("TodoId")]
        public string Todo_TodoId { get; set; }

        [Column("Title")]
        public string Todo_Title { get; set; }

        [Column("Status")]
        public string Todo_Status { get; set; }
    }
}
