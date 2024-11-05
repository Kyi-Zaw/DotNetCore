using KZDotNetCore.Domain.Models.Todo;

namespace KZDotNetCore.Domain.Features.Todo
{
    public interface ITodoServices
    {
        TodoResponseModel Create(TodoRequestModel requestModel);
        TodoResponseModel Delete(string id);
        TodoListResponseModel Get();
        TodoResponseModel GetByID(string id);
        TodoResponseModel Update(string id, TodoRequestModel requestModel);
    }
}
