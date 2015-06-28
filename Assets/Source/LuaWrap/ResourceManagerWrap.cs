using System;
using UnityEngine;
using System.Collections;
using LuaInterface;
using Object = UnityEngine.Object;

public class ResourceManagerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("LoadAssetBundleManifest", LoadAssetBundleManifest),
			new LuaMethod("GetLoadedAssetBundle", GetLoadedAssetBundle),
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("UnloadAssetBundle", UnloadAssetBundle),
			new LuaMethod("LoadAssetAsync", LoadAssetAsync),
			new LuaMethod("LoadLevelAsync", LoadLevelAsync),
			new LuaMethod("LoadUIAsset", LoadUIAsset),
			new LuaMethod("LoadAsset", LoadAsset),
			new LuaMethod("LoadLevel", LoadLevel),
			new LuaMethod("New", _CreateResourceManager),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("BaseDownloadingURL", get_BaseDownloadingURL, set_BaseDownloadingURL),
			new LuaField("Variants", get_Variants, set_Variants),
			new LuaField("AssetBundleManifestObject", null, set_AssetBundleManifestObject),
		};

		LuaScriptMgr.RegisterLib(L, "ResourceManager", typeof(ResourceManager), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateResourceManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ResourceManager class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(ResourceManager);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BaseDownloadingURL(IntPtr L)
	{
		LuaScriptMgr.Push(L, ResourceManager.BaseDownloadingURL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Variants(IntPtr L)
	{
		LuaScriptMgr.PushArray(L, ResourceManager.Variants);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BaseDownloadingURL(IntPtr L)
	{
		ResourceManager.BaseDownloadingURL = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Variants(IntPtr L)
	{
		ResourceManager.Variants = LuaScriptMgr.GetArrayString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AssetBundleManifestObject(IntPtr L)
	{
		ResourceManager.AssetBundleManifestObject = (AssetBundleManifest)LuaScriptMgr.GetUnityObject(L, 3, typeof(AssetBundleManifest));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAssetBundleManifest(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ResourceManager obj = (ResourceManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ResourceManager");
		IEnumerator o = obj.LoadAssetBundleManifest();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLoadedAssetBundle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string arg1 = null;
		AssetBundleInfo o = ResourceManager.GetLoadedAssetBundle(arg0,out arg1);
		LuaScriptMgr.PushObject(L, o);
		LuaScriptMgr.Push(L, arg1);
		return 2;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		AssetBundleManifestOperation o = ResourceManager.Initialize(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnloadAssetBundle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		ResourceManager.UnloadAssetBundle(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAssetAsync(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		Type arg2 = LuaScriptMgr.GetTypeObject(L, 3);
		AssetBundleAssetOperation o = ResourceManager.LoadAssetAsync(arg0,arg1,arg2);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadLevelAsync(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 3);
		AssetBundleLoadLevelOperation o = ResourceManager.LoadLevelAsync(arg0,arg1,arg2);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadUIAsset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		ResourceManager obj = (ResourceManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ResourceManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		LuaFunction arg2 = LuaScriptMgr.GetLuaFunction(L, 4);
		obj.LoadUIAsset(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAsset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		ResourceManager obj = (ResourceManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ResourceManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		Action<GameObject> arg2 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg2 = (Action<GameObject>)LuaScriptMgr.GetNetObject(L, 4, typeof(Action<GameObject>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 4);
			arg2 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		obj.LoadAsset(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadLevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		ResourceManager obj = (ResourceManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ResourceManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		Action<GameObject> arg3 = null;
		LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

		if (funcType3 != LuaTypes.LUA_TFUNCTION)
		{
			 arg3 = (Action<GameObject>)LuaScriptMgr.GetNetObject(L, 5, typeof(Action<GameObject>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 5);
			arg3 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		obj.LoadLevel(arg0,arg1,arg2,arg3);
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

