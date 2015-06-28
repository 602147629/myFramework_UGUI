using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class BaseLuaWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("AddClick", AddClick),
			new LuaMethod("ClearClick", ClearClick),
			new LuaMethod("New", _CreateBaseLua),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("callBack", get_callBack, set_callBack),
			new LuaField("uluaMgr", get_uluaMgr, null),
		};

		LuaScriptMgr.RegisterLib(L, "BaseLua", typeof(BaseLua), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBaseLua(IntPtr L)
	{
		LuaDLL.luaL_error(L, "BaseLua class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(BaseLua);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_callBack(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BaseLua obj = (BaseLua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name callBack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index callBack on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.callBack);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_uluaMgr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BaseLua obj = (BaseLua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uluaMgr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uluaMgr on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.uluaMgr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_callBack(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BaseLua obj = (BaseLua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name callBack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index callBack on a nil value");
			}
		}

		obj.callBack = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		BaseLua obj = (BaseLua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "BaseLua");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		LuaFunction arg1 = LuaScriptMgr.GetLuaFunction(L, 3);
		obj.AddClick(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BaseLua obj = (BaseLua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "BaseLua");
		obj.ClearClick();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

