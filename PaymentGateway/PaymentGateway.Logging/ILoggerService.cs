﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Logging
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
