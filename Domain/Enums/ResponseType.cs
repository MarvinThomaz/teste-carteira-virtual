namespace teste_carteira_virtual.Domain.Enums
{
    public enum ResponseType
    {
        SuccessStatusOfRequest = 200,
        SuccessCreatedObject = 201,
        NotFoundedObject = 404,
        ValidationErrorOfObject = 422,
        UndefinedError = 500
    }
}