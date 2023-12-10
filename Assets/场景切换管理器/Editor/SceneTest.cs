using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 编辑器扩展
/// </summary>
public class SceneTest 
{
   [MenuItem("我的菜单/同步切换场景/重新切换到当前场景")]
   static void Method1()
   {
       LoadSceneManager.Instance.LoadActiveScene();
    }
    [MenuItem("我的菜单/同步切换场景/切换到上一个场景")]
    static void Method2()
    {
        LoadSceneManager.Instance.LoadPreviousScene(true);
    }
    [MenuItem("我的菜单/同步切换场景/切换到下一个场景")]
    static void Method3()
    {
        LoadSceneManager.Instance.LoadNextScene(true);
    }

    [MenuItem("我的菜单/异步切换场景/切换到场景1")]
    static void Method4()
    {
        LoadSceneManager.Instance.LoadSceneAsync("scene1", (obj) => { Debug.Log("加载进度" + obj * 100 + "%");}, (obj) => { Debug.Log("加载完成了！"); });
    }
    [MenuItem("我的菜单/异步切换场景/切换到场景2")]
    static void Method5()
    {
        LoadSceneManager.Instance.LoadSceneAsync("scene2", (obj) => { Debug.Log("加载进度" + obj * 100 + "%"); }, (obj) => { Debug.Log("加载完成了！"); });
    }
    [MenuItem("我的菜单/异步切换场景/切换到场景3")]
    static void Method6()
    {
        LoadSceneManager.Instance.LoadSceneAsync("scene3", (obj) => { Debug.Log("加载进度" + obj * 100 + "%"); }, (obj) => { Debug.Log("加载完成了！"); });
    }
}

