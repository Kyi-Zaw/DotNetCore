namespace PIDDotNetTraining.Domain.Models;

public class TodoListResponseModel
{
    public ResponseModel Response { get; set; }
    public List<TodoModel> Data { get; set; }
}
