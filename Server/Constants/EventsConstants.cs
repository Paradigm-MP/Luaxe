using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luaxe.Server.Constants
{
    /// <summary>
    /// All event name strings to be used in the Lua event system.
    /// </summary>
    class Events
    {
        public static readonly string NewConnection = "NewConnection";
        public static readonly string ConsoleCommand = "ConsoleCommand";
    }
}
