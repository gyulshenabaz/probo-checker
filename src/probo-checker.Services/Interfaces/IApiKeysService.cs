﻿namespace probo_checker.Services.Interfaces
{
    public interface IApiKeysService
    {
        string Generate();
        bool IsValidKey(string apiKey);
    }
}