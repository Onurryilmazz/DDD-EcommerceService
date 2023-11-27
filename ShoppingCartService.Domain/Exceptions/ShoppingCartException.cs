
public class ShoppingCartException : Exception
{
    
    public ShoppingCartException()
    {
    }
    
    public ShoppingCartException(string message) : base(message)
    {
    }
    
    
    public ShoppingCartException(string message, Exception innerException) : base(message, innerException)
    {
    }
    
    
}