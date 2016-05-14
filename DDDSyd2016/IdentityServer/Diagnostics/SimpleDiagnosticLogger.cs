
using IdentityServer3.Core.Logging;
using System;
using System.Diagnostics;

namespace DDDSyd2016.IdentityServer.Diagnostics
{
    
    public class SimpleDiagnosticLoggerProvider : ILogProvider
    {
        string _directory;

        public SimpleDiagnosticLoggerProvider(string directory)
        {
            _directory = directory;
        }
        SimpleDiagnosticLogger _logger;
        private static object _lockHandle = new object();

        public Logger GetLogger(string name)
        {
            CreateLoggerIfRequired(name);
            return _logger.Log;
        }

        private void CreateLoggerIfRequired(string name)
        {
            if (_logger == null)
            {
                lock (_lockHandle)
                {
                    if (_logger == null)
                    {
                        _logger = new SimpleDiagnosticLogger(name,_directory);
                    }
                }
            }
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Incredibly basic logger. Great for demo's, sux for prod.
    /// </summary>
    public class SimpleDiagnosticLogger
    {
        private readonly string _name;
        private readonly string _directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticsTraceLogger"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public SimpleDiagnosticLogger(string name, string directory)
        {
            _name = $"<{name}>";
            _directory = directory;
        }

        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters)
        {
            if (messageFunc == null)
            {
                return true;
            }
            var msgContent = messageFunc();
            var msg = (formatParameters != null && formatParameters.Length > 0) 
                                ? string.Format(msgContent, formatParameters) 
                                : msgContent;
            var exceptionMessage = exception != null ? $"{Environment.NewLine}\t{exception.Message}{Environment.NewLine}" : Environment.NewLine;
            var fullMessage = string.Format("[({0}) {1}: {2}]:> {3}{4}",
                        logLevel, 
                        _name, 
                        DateTime.UtcNow.ToString(), 
                        msg, 
                        exceptionMessage);
            System.IO.File.AppendAllText(_directory + "\\Diagnostics.log", fullMessage);
            return true;
        }
    }
}