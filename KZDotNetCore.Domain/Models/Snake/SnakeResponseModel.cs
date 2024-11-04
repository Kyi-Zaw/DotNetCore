using PIDDotNetTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KZDotNetCore.Domain.Models.Snake
{
    public class SnakeResponseModel
    {
        public ResponseModel Response { get; set; }
        public SnakeModel Data { get; set; }
    }
}
