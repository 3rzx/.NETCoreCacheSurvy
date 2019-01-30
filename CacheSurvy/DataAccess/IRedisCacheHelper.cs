namespace CacheSurvy.DataAccess
{
    public interface IRedisCacheHelper
    {
        string GetServiceUrl(string destinationId);

        void SetServiceUrl(string destinationId, string url);
    }
}