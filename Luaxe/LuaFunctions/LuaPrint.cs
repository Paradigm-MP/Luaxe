using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Luaxe.LuaFunctions
{
    public class LuaPrint
    {
        // print() function called from lua script. The functions registered with the lua state must be public. 
        static public void Print(params object[] args)
        {
            foreach (object arg in args)
            {
                Debug.Log(arg.ToString());
            }
        }
    }
}
