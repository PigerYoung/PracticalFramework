using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// MonoBehavior的“执行者”脚本，其它脚本可以通过它来开启协程，也可以通过它来监听Update，FixedUpdate，LateUpdate
/// </summary>
public class MonoCtroller : MonoBehaviour
{
    private MonoCtroller() { }//这里构造函数私有化可以防止new该对象，但是不能执行

    event UnityAction updateEvent;//在生命周期函数中执行的事件
    event UnityAction fixedUpdateEvent;//在fixedupdate执行的事件
    event UnityAction lateUpdateEvent;//在lateUpdate执行的事件

    private void FixedUpdate()
    {
        fixedUpdateEvent?.Invoke();
    }

    private void Update()
    {
        //if (updateEvent != null)
        //{
        //    updateEvent.Invoke();
        //}
        updateEvent?.Invoke();//这是事件中的特殊语法形式，效果和上方一样
    }
    private void LateUpdate()
    {
        lateUpdateEvent?.Invoke();
    }
    //---------------------------------------FixedUpdate-----------------------------------------------
    public void AddFixedUpdateListener(UnityAction action)
    {
        fixedUpdateEvent += action;
    }
    public void RemoveFixedUpdateListener(UnityAction action)
    {
        fixedUpdateEvent -= action;
    }
    public void RemoveAllFixedUpdateListener()
    {
        fixedUpdateEvent = null;
    }
    //-----------------------------------------------Update------------------------------------------------
    public void AddUpdateListener(UnityAction action)//为外部提供添加事件的函数
    {
        updateEvent += action;
    }
    public void RemoveUpdateListener(UnityAction action)//移除事件
    {
        updateEvent -= action;
    }
    public void RemoveAllUpdateListener()//移除所有的事件
    {
        updateEvent=null;
    }
    //-----------------------------------------------LateUpdate------------------------------------------------
    public void AddLateUpdateListener(UnityAction action)
    {
        lateUpdateEvent += action;
    }
    public void RemoveLateUpdateListener(UnityAction action)
    {
        lateUpdateEvent -= action;
    }
    public void RemoveAllLateUpdateListener()
    {
        lateUpdateEvent = null;
    }
    //--------------------------------------------------------------------------------------------------------
    public void RemoveAllListeners()//移除所有监听事件
    {
        RemoveAllUpdateListener();
        RemoveAllFixedUpdateListener();
        RemoveAllLateUpdateListener();
    }

}
