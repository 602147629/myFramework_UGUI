using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Const
{   
    public static bool DebugMode = true;                       //在编辑器下用来调试打包资源
    public static bool UpdateMode = true;                       //是否开启更新模式

    public static int TimerInterval = 1;
    public static int GameFrameRate = 30;                       //游戏帧频

    public static bool UsePbc = true;                           //PBC
    public static bool UseLpeg = true;                          //LPEG
    public static bool UsePbLua = true;                         //Protobuff-lua-gen
    public static bool UseCJson = true;                         //CJson
    public static bool LuaEncode = true;                        //使用LUA编码

    public static string UserId = string.Empty;                 //用户ID
    public static string AppName = "demo";           //应用程序名称
    public static string AppPrefix = AppName + "_";             //应用程序前缀
    public static string ExtName = ".unity3d";                  //素材扩展名
    public static string AssetDirName = "AssetBundles";         //素材目录 

    public static string LocalIP = "192.168.1.103";
    public static string LocalPort = "8080";
    public static string WebUrl = string.Concat("http://", LocalIP, ":", LocalPort, "/AssetBundles/");  //测试更新地址  
    public static int SocketPort = 0;                           //Socket服务器端口
    public static string SocketAddress = string.Empty;          //Socket服务器地址
}
