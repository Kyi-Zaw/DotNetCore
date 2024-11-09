using KZDotNetCore.Domain.Database;
using KZDotNetCore.Domain.Models.Snake;
using Microsoft.EntityFrameworkCore;
using PIDDotNetTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KZDotNetCore.Domain.Features.Snake
{
    public class SnakeEfService : ISnakeService
    {
        private readonly AppDbContext _db;

        public SnakeEfService(AppDbContext db)
        {
            _db = db;
        }
        public SnakeResponseModel Create(SnakeRequestModel requestModel)
        {
            var model = new SnakeResponseModel();
            var snakeDataModel = _db.snakes.ToList().OrderByDescending(x => x.SnakeID).FirstOrDefault();
            try
            {
                _db.snakes.Add(new SnakeDataModel
                {
                    SnakeID = snakeDataModel is null  ? 1: snakeDataModel.SnakeID + 1,
                    MMName = requestModel.MMName,
                    EngName = requestModel.EngName,
                    Detail = requestModel.Detail,
                    IsPoison = requestModel.IsPoison,
                    IsDanger = requestModel.IsDanger
                });
                _db.SaveChanges();

                model.Response = ResponseModel.Success();
            }
            catch (Exception ex)
            {
                model.Response = ResponseModel.Error(ex.ToString());
            }


            return model;
        }

        public SnakeResponseModel Delete(int id)
        {
            var model = new SnakeResponseModel();

            try
            {
                var item = _db.snakes.FirstOrDefault(x => x.SnakeID == id);
                if (item is null)
                {
                    model.Response = ResponseModel.Error("No data found.");
                    goto Result;
                }

                _db.Entry(item).State = EntityState.Deleted;
                _db.SaveChanges();

                model.Response = ResponseModel.Success();
            }
            catch (Exception ex)
            {
                model.Response = ResponseModel.Error(ex.ToString());
            }

        Result:
            return model;
        }

        public async Task<SnakeListResponseModel> Get()
        {
            SnakeListResponseModel model = new SnakeListResponseModel();
            try
            {
                model.Data = _db.snakes.AsNoTracking()
                            .ToList()
                            .Select(x => new SnakeModel
                            { SnakeID = x.SnakeID, MMName = x.MMName,
                                EngName = x.EngName , Detail = x.Detail,
                                  IsPoison = x.IsPoison , IsDanger = x.IsDanger}).ToList();
                model.Response = ResponseModel.Success();
            }
            catch (Exception ex)
            {
                model.Response = ResponseModel.Error(ex.ToString());
            }
            return model;
        }

        public async Task<SnakeResponseModel> GetByID(int id)
        {
            SnakeResponseModel model = new SnakeResponseModel();
            try
            {
                SnakeDataModel lst = await _db.snakes.FirstOrDefaultAsync(x => x.SnakeID == id);

                if (lst is null)
                {
                    model.Response = ResponseModel.Error("No data found.");
                    goto Result;
                }
                model.Data = new SnakeModel
                {
                    SnakeID = lst.SnakeID,
                    MMName = lst.MMName,
                    EngName = lst.EngName,
                    Detail = lst.Detail,
                    IsPoison = lst.IsPoison,
                    IsDanger = lst.IsDanger                  
                };

                model.Response = ResponseModel.Success();
            }
            catch (Exception ex)
            {
                model.Response = ResponseModel.Error(ex.ToString());
            }

        Result:
            return model;
        }

        public SnakeResponseModel Update(int id, SnakeRequestModel requestModel)
        {
            SnakeResponseModel model = new SnakeResponseModel();

            try
            {
                var item = _db.snakes.FirstOrDefault(x => x.SnakeID == id);
                if (item is null)
                {
                    model.Response = ResponseModel.Error("No data found.");
                    goto Result;
                }

                if (!string.IsNullOrEmpty(requestModel.MMName))
                    item.MMName = requestModel.MMName;

                if (!string.IsNullOrEmpty(requestModel.EngName))
                    item.EngName = requestModel.EngName;

                if (!string.IsNullOrEmpty(requestModel.Detail))
                    item.Detail = requestModel.Detail;

                if (!string.IsNullOrEmpty(requestModel.IsPoison))
                    item.IsPoison = requestModel.IsPoison;

                if (!string.IsNullOrEmpty(requestModel.IsDanger))
                    item.IsDanger = requestModel.IsDanger;
;

                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                model.Response = ResponseModel.Success();
            }
            catch (Exception ex)
            {
                model.Response = ResponseModel.Error(ex.ToString());
            }

        Result:
            return model;
        }
    }
}
