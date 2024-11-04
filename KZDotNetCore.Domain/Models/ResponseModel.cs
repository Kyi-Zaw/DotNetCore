namespace PIDDotNetTraining.Domain.Models;

public class ResponseModel
{
    public ResponseModel(string code, string desc, string type, bool isSuccess = false)
    {
        Code = code;
        Desc = desc;
        Type = type;
        IsSuccess = isSuccess;
    }

    public string Code { get; set; } 
    public string Desc { get; set; }
    public string Type { get; set; } 
    public bool IsSuccess { get; set; }
    public bool IsError { get { return !IsSuccess; } }

    public static ResponseModel Success(string message = "Operation Successful.")
    {
        return new ResponseModel("000", message, "Success", true );
    }

    public static ResponseModel Error(string message)
    {
        return new ResponseModel("999", message, "Error", false);
    }
}