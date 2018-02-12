using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pineapple.Model;
using Pineapple.DBServices;
using System.Data.SqlClient;
using log4net;
using System.Reflection;
using log4net.Config;

namespace Pineapple.Services
{
    public class LogUsing4net : ILogG
    {

        private readonly log4net.ILog log;

        public LogUsing4net() {this.log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    }

        public void WriteInfo(string text)
        { log.Info(text); }

        public void WriteError(string text)
        { log.Error(text); }

        public void WriteWarning(string text)
        { log.Warn(text); }

        public void WriteDebug(string text)
        { log.Debug(text); }

        public void WriteFatal(string text)
        { log.Fatal(text); }

        public void WriteInfoExp(string text, Exception e)
        {
            log.Info(text,e);
        }

        public void WriteErrorExp(string text, Exception e)
        {
            log.Error(text, e);
        }

        public void WriteWarningExp(string text, Exception e)
        {
            log.Warn(text, e);
        }

        public void WriteDebugExp(string text, Exception e)
        {
            log.Debug(text, e);
        }

        public void WriteFatalExp(string text, Exception e)
        {
            log.Fatal(text, e);
        }

    }
}
