namespace probo_checker.Services.Interfaces
{
    public interface IRandomKeyGenerator
    {
        string GenerateRandomKey(int byteLength);
    }
}