using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono管理器脚本，管理器脚本一般为单例模式
/// 外部都是访问该脚本，通过该脚本去让执行者调用开启协程或者帧更新
/// </summary>
public class MonoManager : SingletonPatternBase<MonoManager>
{
    private MonoManager() { }//构造方法私有化

    private MonoCtroller monoCtroller;
    private MonoCtroller MonoCtroller
    {
        get
        {
            if(monoCtroller == null)
            {
                GameObject go = new GameObject(typeof(MonoCtroller).Name);
                monoCtroller=go.AddComponent<MonoCtroller>();
            }
            return monoCtroller;
        }
    }


    public Coroutine StartCoroutine(IEnumerator routine)//开启协程方法这不是untity提供的协程，因为该脚本没有继承MonoBehavior，所以应该是用MonoCtroller来开启协程
    {
       return MonoCtroller.StartCoroutine(routine);
    }

    public void StopCoroutine(IEnumerator coroutine)//停止协程
    {
        if (coroutine != null)
        {
            MonoCtroller.StopCoroutine(coroutine);
        }       
    }
    public void StopCoroutine(Coroutine coroutine)//重载
    {
        if(coroutine != null)
        {
            MonoCtroller.StopCoroutine(coroutine);
        }     
    }
    public void StopAllCoroutine()//让外部通过它来停止所有协程
    {
        MonoCtroller.StopAllCoroutines();
    }

    public void AddUpdateListener(UnityAction action)//让外部通过该方法来添加update的监听事件
    {
        MonoCtroller.AddUpdateListener(action);
    }
    public void RemoveUpdateListener(UnityAction action)//移除事件
    {
        MonoCtroller.RemoveUpdateListener(action);
    }
    public void RemoveAllUpdateListener()//移除所有的事件
    {
        MonoCtroller.RemoveAllUpdateListener();
    }
    
    public void AddFixedUpdateListener(UnityAction action)
    {
        MonoCtroller.AddFixedUpdateListener(action);
    }
    public void RemoveFixedUpdateListener(UnityAction action)
    {
        MonoCtroller.RemoveFixedUpdateListener(action);
    }
    public void RemoveAllFixedUpdateListener()
    {
        MonoCtroller.RemoveAllFixedUpdateListener();
    }

    public void AddLateUpdateListener(UnityAction action)
    {
        MonoCtroller.AddLateUpdateListener(action);
    }
    public void RemoveLateUpdateListener(UnityAction action)
    {
        MonoCtroller.RemoveLateUpdateListener(action);
    }
    public void RemoveAllLateUpdateListener()
    {
        MonoCtroller.RemoveAllLateUpdateListener();
    }

    public void RemoveAllListenrs()//移除所有监听事件
    {
        MonoCtroller.RemoveAllListeners();
    }
}
