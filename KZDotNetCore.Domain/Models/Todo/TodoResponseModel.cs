using PIDDotNetTraining.Domain.Models;

namespace KZDotNetCore.Domain.Models.Todo;

// Server
public class TodoResponseModel
{
    public ResponseModel Response { get; set; }
    public TodoModel Data { get; set; }
}
