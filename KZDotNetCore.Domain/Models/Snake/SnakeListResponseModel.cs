using PIDDotNetTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KZDotNetCore.Domain.Models.Snake
{
    public class SnakeListResponseModel
    {
        public ResponseModel Response { get; set; }
        public List<SnakeModel> Data { get; set; }
    }
}
