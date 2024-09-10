namespace StarWarsWebApi.Models.Dto;

public class PaginaedResponse<T>
{
    public int Count { get; set; }
    public T Data { get; set; }
}