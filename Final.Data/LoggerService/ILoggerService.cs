using System;
namespace Final.Data.LoggerService
{
	public interface ILoggerService
	{
        public void Write(string message, int ServerError);
    }
}

