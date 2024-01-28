namespace IntigrationWeb.Models.Utility
{
    public interface IHttpWebClients
    {
        string PostRequest(string URI, string parameterValues);
        string GetRequest(string URI, object parameterValues);
    }
}
