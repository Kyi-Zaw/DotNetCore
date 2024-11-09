using KZDotNetCore.Domain.Models.Snake;
using PIDDotNetTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KZDotNetCore.Domain.Features.Snake
{
    public interface ISnakeService
    {
        SnakeResponseModel Create(SnakeRequestModel requestModel);
        SnakeResponseModel Delete(int id);
        Task<SnakeListResponseModel> Get();
        Task<SnakeResponseModel> GetByID(int id);
        SnakeResponseModel Update(int id, SnakeRequestModel requestModel);
    }
}
