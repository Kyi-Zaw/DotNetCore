using KZDotNetCore.Domain.Models.Snake;
using KZDotNetCore.Domain.Services;
using PIDDotNetTraining.Domain.Models;


namespace KZDotNetCore.Domain.Features.Snake
{
    public class SnakeDapperService : ISnakeService
    {
        private readonly DapperService _dapperService;

        public SnakeDapperService(DapperService dapperService)
        {
            _dapperService = dapperService;
        }

        public SnakeResponseModel Create(SnakeRequestModel requestModel)
        {
            SnakeResponseModel model = new SnakeResponseModel();
            try { 
                 string querySelect = @"SELECT [Id] AS SnakeID
                                  ,[MMName]
                                  ,[EngName]
                                  ,[Detail]
                                  ,[IsPoison]
                                  ,[IsDanger]
                              FROM [dbo].[Tbl_Snake] ORDER BY ID desc";
            var lst = _dapperService.Query<SnakeModel>(querySelect).FirstOrDefault();

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

                requestModel.SnakeID = lst is null ? 1 : lst.SnakeID+1;
                _dapperService.Execute(query, requestModel);
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
            SnakeResponseModel model = new SnakeResponseModel();
            try
            {

                string querySelect = $@"SELECT [Id] AS SnakeID
                                  ,[MMName]
                                  ,[EngName]
                                  ,[Detail]
                                  ,[IsPoison]
                                  ,[IsDanger]
                              FROM [dbo].[Tbl_Snake] where Id = @SnakeID ORDER BY ID desc";

                var lst = _dapperService.Query<SnakeModel>(querySelect, new { SnakeID = id}).FirstOrDefault();

                if (lst is null)
                {
                    model.Response = ResponseModel.Error("No data found.");
                    goto Result;
                }

                string query = @"DELETE FROM Tbl_Snake WHERE ID  = @SnakeID";

                _dapperService.Execute(query, new { SnakeID = id });
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
                string query = @"SELECT [Id] AS SnakeID
                                  ,[MMName]
                                  ,[EngName]
                                  ,[Detail]
                                  ,[IsPoison]
                                  ,[IsDanger]
                              FROM [dbo].[Tbl_Snake]";
                var lst = _dapperService.Query<SnakeModel>(query).ToList();

                model.Data = lst;
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
                string querySelect = $@"SELECT [Id] AS SnakeID
                                  ,[MMName]
                                  ,[EngName]
                                  ,[Detail]
                                  ,[IsPoison]
                                  ,[IsDanger]
                              FROM [dbo].[Tbl_Snake] where Id = {id} ORDER BY ID desc";
               
                var lst = _dapperService.Query<SnakeModel>(querySelect).FirstOrDefault();

                if (lst is null)
                {
                    model.Response = ResponseModel.Error("No data found.");
                    goto Result;
                }

                model.Data = lst;
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
                string query = "select [Id] AS SnakeID,[MMName],[EngName],[Detail],[IsPoison],[IsDanger] FROM [dbo].[Tbl_Snake] where ID = @SnakeID";

                var item = _dapperService.QueryFirstOrDefault<SnakeModel>(query, new { SnakeID = id });
                if (item is null)
                {
                    model.Response = ResponseModel.Error("No data found.");
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
                    model.Response = ResponseModel.Error("No data to update.");
                    goto Result;
                }

                conditions = conditions.Substring(0, conditions.Length - 1);
                string queryUpdate = $@"UPDATE [dbo].[Tbl_Snake]
                                   SET 
                                   {conditions}
                                 WHERE ID = @SnakeID";

                requestModel.SnakeID = id;
                _dapperService.Execute(queryUpdate, requestModel);
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
