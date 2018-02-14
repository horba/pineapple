using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Services
{
    public interface ILogG
    {
        void WriteInfo(string text);
        void WriteError(string text);
        void WriteWarning(string text);
        void WriteDebug(string text);
        void WriteFatal(string text);

        void WriteInfoExp(string text, Exception e);
        void WriteErrorExp(string text, Exception e);
        void WriteWarningExp(string text, Exception e);
        void WriteDebugExp(string text, Exception e);
        void WriteFatalExp(string text, Exception e);
        
    }
}
