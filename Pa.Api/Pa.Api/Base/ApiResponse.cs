namespace Pa.Api;

public class ApiResponse<T>
{
    public T Data { get; set; }
    public string Error { get; set; }
    public bool IsSuccess { get; set; }

    public ApiResponse(T data)
    {
        this.Data = data;
        this.IsSuccess = true;
        this.Error = string.Empty;
    }
    
    public ApiResponse(string message)
    {
        this.IsSuccess = false;
        this.Error = message;
    }
}