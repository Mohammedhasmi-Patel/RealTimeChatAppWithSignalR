namespace ChatApp.Application.DTO.Common;

public class ApiResponse<T> 
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool Success { get; set; }

    public T? Data {get;set;}

    public static ApiResponse<T> SuccessResponse(T data,int statusCode,string message="Success.")
    {
        return new ApiResponse<T>()
        {
            Success = true,
            Message = message,
            StatusCode = statusCode,
            Data = data
        };
    }

    public static ApiResponse<T> FailureResponse(int statusCode=500,string message="Something went wrong.")
    {
        return new ApiResponse<T>()
        {
            Success = false,
            Message = message,
            StatusCode = statusCode,
            Data = default
        };
    }

}
