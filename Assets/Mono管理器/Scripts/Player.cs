using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 没有继承MonoBehaviour的一个普通脚本，用来测试用
/// </summary>
public class Player 
{
    Coroutine coroutine;//用coroutine将开启的协程记录下来，这样才能暂停协程
    public void Show()
    {
        coroutine = MonoManager.Instance.StartCoroutine(MyCoroutine());//这里就可以看到虽然没有继承自MonoBehaviour类，但是还是可以利用Mono管理器开启协程
    }

    public void Hide()
    {
        //MonoManager.Instance.StopCoroutine(MyCoroutine());//这里这样写会出bug，会导致停止不了协程，原因是因为unity会认为开启的协程和关闭的协程是不同的两个协程      
            MonoManager.Instance.StopCoroutine(coroutine);      
    }
    public void HideAll()
    {
        MonoManager.Instance.StopAllCoroutine();
    }

    IEnumerator MyCoroutine()
    {
        while (true)
        {
            Debug.Log("协程执行中");
            yield return null;
        }
    }

    public void Update()//这里的Update并不是MonoBehavior中的Update，只是取名而且
    {
        MonoManager.Instance.AddUpdateListener(DebugUpdate);//如果这里使用lambda表达式添加事件的话就不能移除该事件，因为在unity中会认为这是一个事件，在移除时如果还是该lambda表达式，unity会任务这是第二个事件，不是同一个事件
    }
    public void StopUpdate()//停止update
    {
        MonoManager.Instance.RemoveUpdateListener(DebugUpdate);
    }
    public void StopAllUpdate()//停止所有的update
    {
        MonoManager.Instance.RemoveAllUpdateListener();
    }
    public void DebugUpdate()
    {
        Debug.Log("update");
    }

    public void FixedUpdate()
    {
        MonoManager.Instance.AddFixedUpdateListener(DebugFixedUpdateUpdate);
    }
    public void StopFixedUpdate()
    {
        MonoManager.Instance.RemoveFixedUpdateListener(DebugFixedUpdateUpdate);
    }
    public void StopAllFixedUpdate()
    {
        MonoManager.Instance.RemoveAllFixedUpdateListener();
    }
    public void DebugFixedUpdateUpdate()
    {
        Debug.Log("FixedUpdate");
    }

    public void LateUpdate()
    {
        MonoManager.Instance.AddLateUpdateListener(DebugLateUpdateUpdate);
    }
    public void StopLateUpdate()
    {
        MonoManager.Instance.RemoveLateUpdateListener(DebugLateUpdateUpdate);
    }
    public void StopAllLateUpdate()
    {
        MonoManager.Instance.RemoveAllLateUpdateListener();
    }
    public void DebugLateUpdateUpdate()
    {
        Debug.Log("LateUpdate");
    }

    public void RemoveAllListeners()
    {
        MonoManager.Instance.RemoveAllListenrs();
    }
}
