using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KZDotNetCore.Domain.Database
{
    [Table("Tbl_Snake")]
    public class SnakeDataModel
    {
        [Key]
        [Column("Id")]
        public int SnakeID { get; set; }

        [Column("MMName")]
        public string MMName { get; set; }

        [Column("EngName")]
        public string EngName { get; set; }

        [Column("Detail")]
        public string Detail { get; set; }

        [Column("IsPoison")]
        public string IsPoison { get; set; }

        [Column("IsDanger")]
        public string IsDanger { get; set; }

    }
}
