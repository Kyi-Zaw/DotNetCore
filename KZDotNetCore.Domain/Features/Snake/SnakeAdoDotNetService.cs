using Azure;
using KZDotNetCore.Domain.Models.Snake;
using KZDotNetCore.Domain.Services;
using PIDDotNetTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KZDotNetCore.Domain.Features.Snake
{
    public class SnakeAdoDotNetService : ISnakeService
    {
        private readonly AdoDotNetService _adoDotNetService;

        public SnakeAdoDotNetService(AdoDotNetService adoDotNetService)
        {
            _adoDotNetService = adoDotNetService;   
        }
        public SnakeResponseModel Create(SnakeRequestModel requestModel)
        {
            SnakeResponseModel model = new SnakeResponseModel();
            try
            {
                string querySelect = @"SELECT top 1 [Id] AS SnakeID
                                  
                              FROM [dbo].[Tbl_Snake] ORDER BY ID desc";
                var dt = _adoDotNetService.Query(querySelect);
                
                int GenerateID = 0;
                foreach (DataRow dr in dt.Rows) { GenerateID = Convert.ToInt32(dr["SnakeID"]); }

                string query = @"INSERT INTO [dbo].[Tbl_Snake]
                                   ([Id]
                                   ,[MMName]
                                   ,[EngName]
                                   ,[Detail]
                                   ,[IsPoison]
                                   ,[IsDanger])
                             VALUES
                                   (@SnakeId
                                   ,@MMName
                                   ,@EngName
                                   ,@Detail
                                   ,@IsPoison
                                   ,@IsDanger)";
                int result = _adoDotNetService.Execute(query,new SqlParameterModel
                { 
                    Name="@SnakeID",
                    Value = (GenerateID+1).ToString() 
                },new SqlParameterModel
                {
                    Name = "@MMName",
                    Value = requestModel.MMName
                },new SqlParameterModel
                {
                    Name = "@EngName",
                    Value = requestModel.EngName
                },new SqlParameterModel
                {
                    Name = "@Detail",
                    Value = requestModel.Detail               
                },new SqlParameterModel
                {
                    Name = "@IsPoison",
                    Value = requestModel.IsPoison           
                },new SqlParameterModel
                {
                    Name = "@IsDanger",
                    Value = requestModel.IsDanger
                });
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
            SnakeResponseModel snakeResponseModel = new SnakeResponseModel();
            SnakeModel snakeModel = new SnakeModel();

            string selectQuery = @"SELECT [Id] AS SnakeID
                                  ,[MMName]
                                  ,[EngName]
                                  ,[Detail]
                                  ,[IsPoison]
                                  ,[IsDanger]
                              FROM [dbo].[Tbl_Snake] where Id = @SnakeID";
            var dt = _adoDotNetService.Query(selectQuery, new SqlParameterModel("@SnakeID", id.ToString()));

            if (dt.Rows.Count == 0)
            {
                snakeResponseModel.Response = ResponseModel.Error("No data found.");
                goto Result;
            }

            string query = @"DELETE FROM [dbo].[Tbl_Snake] where Id = @SnakeID";
            int result = _adoDotNetService.Execute(query, new SqlParameterModel("@SnakeID", id.ToString()));
            if (result == 1)
            {
                snakeResponseModel.Response = ResponseModel.Success();

            }
           
            Result:
            return snakeResponseModel;
        }

        public async Task<SnakeListResponseModel> Get()
        {
            SnakeListResponseModel response = new SnakeListResponseModel();

            string query = @"SELECT [Id] AS SnakeID
                                  ,[MMName]
                                  ,[EngName]
                                  ,[Detail]
                                  ,[IsPoison]
                                  ,[IsDanger]
                              FROM [dbo].[Tbl_Snake]";
            var dt = _adoDotNetService.Query(query);

            List<SnakeModel> lst = new List<SnakeModel>();
            foreach (DataRow dr in dt.Rows)
            {
                SnakeModel snakeModel = new SnakeModel();
                snakeModel.SnakeID = Convert.ToInt32(dr["SnakeID"]);
                snakeModel.MMName = dr["MMName"].ToString();
                snakeModel.EngName = dr["EngName"].ToString();
                snakeModel.Detail = dr["Detail"].ToString();
                snakeModel.IsPoison = dr["IsPoison"].ToString();
                snakeModel.IsDanger = dr["IsDanger"].ToString();
                lst.Add(snakeModel);
            }
            response.Data =  lst;
            response.Response = ResponseModel.Success();

            return response;
        }

        public async Task<SnakeResponseModel> GetByID(int id)
        {
            SnakeResponseModel snakeResponseModel = new SnakeResponseModel();
            SnakeModel snakeModel = new SnakeModel();

            string query = @"SELECT [Id] AS SnakeID
                                  ,[MMName]
                                  ,[EngName]
                                  ,[Detail]
                                  ,[IsPoison]
                                  ,[IsDanger]
                              FROM [dbo].[Tbl_Snake] where Id = @SnakeID";
            var dt = _adoDotNetService.Query(query,new SqlParameterModel( "@SnakeID" ,id.ToString() ));

            if (dt.Rows.Count==0)
            {
                snakeResponseModel.Response = ResponseModel.Error("No data found.");
                goto Result;
            }

            foreach (DataRow dr in dt.Rows)
            {
                
                snakeModel.SnakeID = Convert.ToInt32(dr["SnakeID"]);
                snakeModel.MMName = dr["MMName"].ToString();
                snakeModel.EngName = dr["EngName"].ToString();
                snakeModel.Detail = dr["Detail"].ToString();
                snakeModel.IsPoison = dr["IsPoison"].ToString();
                snakeModel.IsDanger = dr["IsDanger"].ToString();
            }
            snakeResponseModel.Data = snakeModel;
            snakeResponseModel.Response = ResponseModel.Success();

            Result:
            return snakeResponseModel;

        }

        public SnakeResponseModel Update(int id, SnakeRequestModel requestModel)
        {
            SnakeResponseModel snakeResponseModel = new SnakeResponseModel();
            try
            {
                string query = @"SELECT [Id] AS SnakeID
                                  ,[MMName]
                                  ,[EngName]
                                  ,[Detail]
                                  ,[IsPoison]
                                  ,[IsDanger]
                              FROM [dbo].[Tbl_Snake] where Id = @SnakeID";
                var dt = _adoDotNetService.Query(query, new SqlParameterModel("@SnakeID", id.ToString()));

                if (dt.Rows.Count == 0)
                {
                    snakeResponseModel.Response = ResponseModel.Error("No data found.");
                    goto Result;
                }

                string conditions = "";
                if (!string.IsNullOrEmpty(requestModel.MMName))
                {
                    conditions += "[MMName] = @MMName,";
                }
                if (!string.IsNullOrEmpty(requestModel.EngName))
                {
                    conditions += "[EngName] = @EngName,";
                }
                if (!string.IsNullOrEmpty(requestModel.Detail))
                {
                    conditions += "[Detail] = @Detail,";
                }
                if (!string.IsNullOrEmpty(requestModel.IsPoison))
                {
                    conditions += "[IsPoison] = @IsPoison,";
                }
                if (!string.IsNullOrEmpty(requestModel.IsDanger))
                {
                    conditions += "[IsDanger] = @IsDanger,";
                }


                if (conditions.Length == 0)
                {
                    snakeResponseModel.Response = ResponseModel.Error("No data to update.");
                    goto Result;
                }

                conditions = conditions.Substring(0, conditions.Length - 1);
                string queryUpdate = $@"UPDATE [dbo].[Tbl_Snake]
                                   SET 
                                   {conditions}
                                 WHERE ID = @SnakeID";

                int result = _adoDotNetService.Execute(queryUpdate, new SqlParameterModel
                {
                    Name = "@SnakeID",
                    Value = id.ToString()
                }, new SqlParameterModel
                {
                    Name = "@MMName",
                    Value = requestModel.MMName
                }, new SqlParameterModel
                {
                    Name = "@EngName",
                    Value = requestModel.EngName
                }, new SqlParameterModel
                {
                    Name = "@Detail",
                    Value = requestModel.Detail
                }, new SqlParameterModel
                {
                    Name = "@IsPoison",
                    Value = requestModel.IsPoison
                }, new SqlParameterModel
                {
                    Name = "@IsDanger",
                    Value = requestModel.IsDanger
                });
                if (result > 0)
                {
                    snakeResponseModel.Response = ResponseModel.Success();
                }
                
            }
            catch (Exception ex)
            {
                snakeResponseModel.Response = ResponseModel.Error(ex.ToString());
            }

        Result:
            return snakeResponseModel;
        }
    }
}
