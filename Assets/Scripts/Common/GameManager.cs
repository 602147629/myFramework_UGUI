using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using System.Reflection;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

public class GameManager : BaseLua
{
    public LuaScriptMgr uluaMgr;
    public string message;

    /// <summary>
    /// 初始化游戏管理器
    /// </summary>
    void Awake ()
    {
        if (Application.isMobilePlatform)
            Const.DebugMode = false;

        Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Init()
    {
        DontDestroyOnLoad(gameObject);  //防止销毁自己

        Util.Add<PanelManager>(gameObject);
        Util.Add<MusicManager>(gameObject);
        Util.Add<TimerManager>(gameObject);
        Util.Add<SocketClient>(gameObject);
        Util.Add<NetworkManager>(gameObject);
        Util.Add<ResourceManager>(gameObject);

        CheckExtractResource(); //释放资源
        ZipConstants.DefaultCodePage = 65001;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = Const.GameFrameRate;

        // 将Unity的Log转发到一个窗口上
        RemoteLog.Instance.Start(Const.LocalIP, 2010); // 目前默认端口是2010
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void CheckExtractResource()
    {
        bool isExists = Directory.Exists(Util.DataPath) &&
          Directory.Exists(Util.DataPath + "lua/") && File.Exists(Util.DataPath + "files.txt");

        if (isExists || Const.DebugMode)
        {
            StartCoroutine(OnUpdateResource());
            return;   //文件已经解压过了，自己可添加检查文件列表逻辑
        }

        StartCoroutine(OnExtractResource());    //启动释放协成 
    }

    IEnumerator OnExtractResource()
    {
        string dataPath = Util.DataPath;  //数据目录

        string resPath = Util.AppContentPath() + "/" + Const.AssetDirName + "/"; //游戏包资源目录

        if (Directory.Exists(dataPath)) Directory.Delete(dataPath);
        Directory.CreateDirectory(dataPath);

        string infile = resPath + "files.txt";
        string outfile = dataPath + "files.txt";
        if (File.Exists(outfile)) File.Delete(outfile);

        message = "正在解包文件:>files.txt\n";
        Debug.Log(message);
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW www = new WWW(infile);
            yield return www;

            if (www.isDone)
                File.WriteAllBytes(outfile, www.bytes);
          
            yield return 0;
        }
        else
            File.Copy(infile, outfile, true);

        yield return new WaitForEndOfFrame();

        //释放所有文件到数据目录
        string[] files = File.ReadAllLines(outfile);
        foreach (string file in files)
        {
            string[] fs = file.Split('|');
            infile = resPath + fs[0];  //
            outfile = dataPath + fs[0];
            message = "正在解包文件:>" + fs[0] + "\n";
            Debug.Log("正在解包文件:>" + infile);

            string dir = Path.GetDirectoryName(outfile);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            if (Application.platform == RuntimePlatform.Android)
            {
                WWW www = new WWW(infile);
                yield return www;

                if (www.isDone)
                    File.WriteAllBytes(outfile, www.bytes);
         
                yield return 0;
            }
            else 
                File.Copy(infile, outfile, true);

            yield return new WaitForEndOfFrame();
        }

        if (0 == files.Length)
            message += "未释放所有文件到数据目录!!!\n";
        else
            message += "解包完成!!!\n";

        yield return new WaitForSeconds(0.1f);

        //释放完成，开始启动更新资源
        StartCoroutine(OnUpdateResource());
    }

    /// <summary>
    /// 启动更新下载，这里只是个思路演示，此处可启动线程下载更新
    /// </summary>
    IEnumerator OnUpdateResource()
    {
        if (!Const.UpdateMode)
        {
            StartCoroutine(OnResourceInited());   
            yield break;
        }

        WWW www = null;
        string dataPath = Util.DataPath;  //数据目录
        string url = Const.WebUrl;

        string random = DateTime.Now.ToString("yyyymmddhhmmss");
        string listUrl = url + "files.txt?v=" + random;
        if (Debug.isDebugBuild) Debug.LogWarning("LoadUpdate---->>>" + listUrl);

        message += "连接远程服务器更新!!!---->>>" + listUrl + "\n";

        www = new WWW(listUrl); 
        yield return www;

        if (www.error != null)
        {
            OnUpdateFailed(string.Empty);
            yield break;
        }

        if (!Directory.Exists(dataPath))
            Directory.CreateDirectory(dataPath);
     
        File.WriteAllBytes(dataPath + "files.txt", www.bytes);
        string filesText = www.text;
        string[] files = filesText.Split('\n');

        for (int i = 0; i < files.Length; i++)
        {
            if (string.IsNullOrEmpty(files[i])) continue;
            string[] keyValue = files[i].Split('|');

                string localfile = (dataPath + keyValue[0]).Trim();
                string path = Path.GetDirectoryName(localfile);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileUrl = url + keyValue[0] + "?v=" + random;

                bool canUpdate = !File.Exists(localfile);
                if (!canUpdate)
                {
                    string remoteMd5 = keyValue[1].Trim();
                    string localMd5 = Util.md5file(localfile);

                    canUpdate = !remoteMd5.Equals(localMd5);
                    if (canUpdate) 
                        File.Delete(localfile);
                }

                if (canUpdate)
                {
                    //更新本地文件
                    Debug.Log(fileUrl);
                    message += "更新本地文件 downloading>>" + fileUrl + "\n";

                    www = new WWW(fileUrl);
                    yield return www;

                    if (www.error != null)
                    {
                        OnUpdateFailed(path);
                        yield break;
                    }

                    File.WriteAllBytes(localfile, www.bytes);
                }
            }

            yield return new WaitForEndOfFrame();
            message = "更新完成!!\n";

            StartCoroutine(OnResourceInited());   
        }

    void OnUpdateFailed(string file)
    {
        message += "更新失败!>" + file;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 960, 640), message);

        if (GUI.Button(new Rect(100, 100, 50, 50), "click"))
        {
            ioo.resourceManager.LoadLevel("scene", "test", true, null);
        }
    }

    /// <summary>
    /// 资源初始化结束
    /// </summary>
    private IEnumerator OnResourceInited() 
    {
        // 先加载AssetBundleManifest
        yield return StartCoroutine(ioo.resourceManager.LoadAssetBundleManifest());

        uluaMgr = new LuaScriptMgr();
        uluaMgr.Start();

        uluaMgr.DoFile("logic/game");      //加载游戏
        uluaMgr.DoFile("logic/network");   //加载网络
      
        ioo.networkManager.OnInit();    //初始化网络

        object[] panels = CallMethod("LuaScriptPanel");
        //---------------------Lua面板---------------------------
        foreach (object o in panels)
        {
            string name = o.ToString().Trim();
            if (string.IsNullOrEmpty(name)) continue;
            name += "Panel";    //添加

            uluaMgr.DoFile("logic/" + name);

            Debug.LogWarning("LoadLua---->>>>" + name + ".lua");
        }

        //------------------------------------------------------------
        CallMethod("OnInitOK");   //初始化完成*/
    }

    /// <summary>
    /// 初始化场景
    /// </summary>
    public void OnInitScene() 
    {
        Debug.Log("OnInitScene-->>" + Application.loadedLevelName);
    }

    /// <summary>
    /// 析构函数
    /// </summary>
    void OnDestroy()
    {
        Debug.Log("~GameManager was destroyed");
    }
}
