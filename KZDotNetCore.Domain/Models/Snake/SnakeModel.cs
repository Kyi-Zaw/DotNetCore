using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KZDotNetCore.Domain.Models.Snake
{
    public class SnakeModel
    {
        public int SnakeID { get; set; }
        public string MMName { get; set; }
        public string EngName { get; set; }
        public string Detail { get; set; }
        public string IsPoison { get; set; }
        public string IsDanger { get; set; }
    }
}
