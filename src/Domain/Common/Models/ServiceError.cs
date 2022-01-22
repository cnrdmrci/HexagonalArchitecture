namespace Domain.Common.Models;

[Serializable]
public class ServiceError
{
    public string Message { get; }
    public int Code { get; }
    
    public ServiceError(string message, int code)
    {
        Message = message;
        Code = code;
    }

    public static ServiceError DefaultError => new ServiceError("An exception occured.", 999);
    public static ServiceError ModelStateError(string validationError) => new ServiceError(validationError, 998);
    public static ServiceError CustomMessage(string errorMessage) => new ServiceError(errorMessage, 997);
    public static ServiceError NotFount => new ServiceError("The specified resource was not found.", 990);
    public static ServiceError ValidationFormat => new ServiceError("Request object format is not true.", 901);
    public static ServiceError Validation => new ServiceError("One or more validation errors occurred.", 900);
}