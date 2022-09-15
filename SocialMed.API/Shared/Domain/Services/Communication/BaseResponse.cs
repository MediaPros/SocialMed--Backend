namespace SocialMed.API.Shared.Domain.Services.Communication;

public abstract  class BaseResponse<T>
{
    
    public string Message { get; set; }
    public T Resource { get; set; }
    public bool Success { get; set; }
    
    protected BaseResponse(string message)
    {
        Success = false;
        Message = message;
        Resource = default;
    }
    
    protected BaseResponse(T resource)
    {
        Success = true;
        Message = string.Empty;
        Resource = resource;
    }
}