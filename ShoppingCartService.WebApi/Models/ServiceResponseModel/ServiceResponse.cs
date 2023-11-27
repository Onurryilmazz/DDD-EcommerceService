namespace ShoppingCartService.WebApi.Models.ServiceResponseModel;

public class ServiceResponse<T> where T : class
{
    public bool result { get; set; }
    
    public T message { get; set; }
}