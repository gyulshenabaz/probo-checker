namespace ProboChecker.Services.Interfaces
{
    public interface IRandomKeyGenerator
    {
        string GenerateRandomKey(int byteLength);
    }
}