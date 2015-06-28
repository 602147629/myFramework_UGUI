using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class KeyValue 
{
    public string key;
    public string value;
    public KeyValue(string key, string value) 
    {
        this.key = key; this.value = value;
    }
}

public class PanelManager : MonoBehaviour 
{
    private Transform parent;

    Transform Parent
    {
        get
        {
            if (parent == null)
                parent = ioo.uiCamera;
            return parent;
        }
    }

    /// <summary>
    /// 创建面板，请求资源管理器
    /// </summary>
    /// <param name="type"></param>
    public void CreatePanel(string name) 
    {
        StartCoroutine(OnCreatePanel(name));
    }

    IEnumerator OnCreatePanel(string name) 
    {
        string assetName = name + "Panel";
        // Load asset from assetBundle.
        string abName = name.ToLower() + ".unity3d";
        AssetBundleAssetOperation request = ResourceManager.LoadAssetAsync(abName, assetName, typeof(GameObject));
        if (request == null) yield break;
        yield return StartCoroutine(request);

        // Get the asset.
        GameObject prefab = request.GetAsset<GameObject>();

        if (Parent.FindChild(name) != null || prefab == null) {
            yield break;
        }
        GameObject go = Instantiate(prefab) as GameObject;
        go.name = assetName;
        go.layer = LayerMask.NameToLayer("UI");
        go.transform.SetParent(Parent);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        go.AddComponent<BaseLua>();

        ResourceManager.UnloadAssetBundle(abName);

        Debug.LogWarning("CreatePanel::>> " + name + " " + prefab);
    }
}
