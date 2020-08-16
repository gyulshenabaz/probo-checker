namespace ProboChecker.Services.Interfaces
{
    public interface IApiKeysService
    {
        string Generate();
        public bool IsKeyValid(string apiKey);
    }
}