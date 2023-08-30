namespace MuzShop.ExceptionMiddleware.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"object with id {key} was not found in {name}!")
        {
            Detail = "Запрашиваемый объект не найден!";
        }
        public string Detail { get; }
    }
}
