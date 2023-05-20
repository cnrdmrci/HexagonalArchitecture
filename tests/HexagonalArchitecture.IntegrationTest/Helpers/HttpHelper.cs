using HexagonalArchitecture.IntegrationTest.Models;
using Newtonsoft.Json;

namespace HexagonalArchitecture.IntegrationTest.Helpers;

public static class HttpHelper
{
    public static async Task<ServiceResponseModel<T>?> DeserializeAsync<T>(this HttpResponseMessage httpResponseMessage) where T: class
    {
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        var serviceResponse = JsonConvert.DeserializeObject<ServiceResponseModel<T>>(content);
        return serviceResponse;
    }
}