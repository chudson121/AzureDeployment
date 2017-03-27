using System;

namespace Logging.Contracts
{
    public interface ILogger
    {
        string Name { get; }
        void Info(string message);
        void Error(string message);
        void Error(string message, Exception ex);
    }
}
