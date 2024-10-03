namespace Company.Middleware
{
    public class BusinessValidationException:Exception 
    {
        public BusinessValidationException(string message) : base(message)
        {

        }
    }
}
