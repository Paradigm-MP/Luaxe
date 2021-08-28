using Client.LuaFunctions;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class LuaStateBuilder
    {
        static public Lua BuildState()
        {
            Lua luaState = new Lua();

            AddLuaFunction(luaState, typeof(LuaPrint), "Print", "print");

            return luaState;
        }

        static protected void AddLuaFunction(Lua luaState, Type classType, string methodName, string luaFunctionName)
        {
            MethodInfo methodInfo = classType.GetMethod(methodName);
            luaState.RegisterFunction(luaFunctionName, methodInfo);
        }
    }
}
