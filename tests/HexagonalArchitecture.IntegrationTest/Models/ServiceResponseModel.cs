namespace HexagonalArchitecture.IntegrationTest.Models;

public class ServiceResponseModel<T> where T : class
{
    public T? Data { get; set; }
    public bool Succeeded { get; set; }
}