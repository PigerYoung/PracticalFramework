using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 我的ui管理器，继承MonoBehavior的单例模式
/// </summary>
public class MyUIManager : MonoBehaviour
{
    //构造方法私有化，防止外部new对象
    private MyUIManager() { }

    //提供一个静态属性（或者方法）返回私有静态对象实例给外部访问
    private static MyUIManager instance;
    public static MyUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go;
                instance = FindObjectOfType<MyUIManager>();             
                //自动单例模式
                if (instance == null)
                {
                    go = new GameObject("MyUIManager");//创建游戏对象
                    instance= go.AddComponent<MyUIManager>();//添加脚本给游戏对象，该函数的返回值就是该游戏脚本                
                }
                else
                {
                    go = instance.GetComponent<GameObject>();
                }
                DontDestroyOnLoad(go);//让游戏对象切换场景不销毁
            }         
            return instance;
        }
    }

    public void Show()
    {
        Debug.Log("显示面板");
    }
    public void Hide()
    {
        Debug.Log("隐藏面板");
    }
}
