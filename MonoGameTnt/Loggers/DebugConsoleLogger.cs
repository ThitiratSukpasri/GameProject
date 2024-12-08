using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanaNita.MonoGameTnt
{
    public class DebugConsoleLogger : Logger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
