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
    public class LogUsing4net
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public static void  WriteInfo(string text)
        { log.Info(text); }

        public static void  WriteError(string text)
        { log.Error(text); }

        public static void  WriteWarning(string text)
        { log.Warn(text); }

        public static void  WriteDebug(string text)
        { log.Debug(text); }

        public static void  WriteFatal(string text)
        { log.Fatal(text); }

        public static void  WriteInfoExp(string text, Exception e)
        {
            log.Info(text,e);
        }

        public static void  WriteErrorExp(string text, Exception e)
        {
            log.Error(text, e);
        }

        public static void  WriteWarningExp(string text, Exception e)
        {
            log.Warn(text, e);
        }

        public static void  WriteDebugExp(string text, Exception e)
        {
            log.Debug(text, e);
        }

        public static void  WriteFatalExp(string text, Exception e)
        {
            log.Fatal(text, e);
        }

    }
}
