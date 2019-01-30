namespace CacheSurvy.DataAccess
{
    public interface IMemoryCacheHelper
    {
        string GetServiceUrl(string destinationId);

        void SetServiceUrl(string destinationId, string url);

        string GetInitData();

        string GetInitTimeoutData();
    }
}
