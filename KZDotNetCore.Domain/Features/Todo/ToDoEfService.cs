using KZDotNetCore.Domain.Database;
using Microsoft.EntityFrameworkCore;
using PIDDotNetTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KZDotNetCore.Domain.Features.Todo
{
    public class ToDoEfService : ITodoServices
    {
        private readonly AppDbContext _db;

        public ToDoEfService(AppDbContext db)
        {
            _db = db;
        }
        public TodoResponseModel Create(TodoRequestModel requestModel)
        {
            var model = new TodoResponseModel();
            try
            {                
                _db.Todos.Add(new TodoDataModel
                {
                    Todo_TodoId = Guid.NewGuid().ToString(),
                    Todo_Title = requestModel.Title,
                    Todo_Status = requestModel.Status ?? "Pending"
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

        public TodoResponseModel Delete(string id)
        {
            throw new NotImplementedException();
        }

        public TodoListResponseModel Get()
        {
            TodoListResponseModel model = new TodoListResponseModel();
            try
            {               
                model.Data = _db.Todos.AsNoTracking()
                            .ToList()
                            .Select(x => new TodoModel 
                            { TodoId = x.Todo_TodoId, Status = x.Todo_Status, Title = x.Todo_Title }).ToList();
                model.Response = ResponseModel.Success();
            }
            catch (Exception ex)
            {
                model.Response = ResponseModel.Error(ex.ToString());
            }          
            return model;
        }

        public TodoResponseModel GetByID(string id)
        {
            TodoResponseModel model = new TodoResponseModel();
            try
            {
                TodoDataModel lst = _db.Todos.FirstOrDefault(x => x.Todo_TodoId == id);

                if (lst is null)
                {
                    model.Response = ResponseModel.Error("No data found.");
                    goto Result;
                }
                model.Data = new TodoModel
                {
                    TodoId = lst.Todo_TodoId,
                    Title = lst.Todo_Title,
                    Status = lst.Todo_Status,
                };

                model.Response = ResponseModel.Success();
            }
            catch(Exception ex)
            {
                model.Response = ResponseModel.Error(ex.ToString());
            }

           Result:
            return model;
        }

        public TodoResponseModel Update(string id, TodoRequestModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
