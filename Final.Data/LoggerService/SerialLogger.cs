using System;
using Serilog;

namespace Final.Data.LoggerService
{
    public class SerialLogger : ILoggerService
    {
        public void Write(string message, int ServerError)
        {
            if(ServerError == 500)
            {
                Log.Error(message);
            }
            else if(ServerError == 200 || ServerError == 201)
            {
                Log.Debug(message);
            }
            else
            {
                Log.Warning(message);
            }
        }
    }
}

