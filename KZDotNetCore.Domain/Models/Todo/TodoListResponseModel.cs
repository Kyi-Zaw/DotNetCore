using PIDDotNetTraining.Domain.Models;

namespace KZDotNetCore.Domain.Models.Todo;

public class TodoListResponseModel
{
    public ResponseModel Response { get; set; }
    public List<TodoModel> Data { get; set; }
}
