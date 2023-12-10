using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件中心管理器
/// </summary>
public class EventCenterManager : SingletonPatternBase<EventCenterManager>
{
    private EventCenterManager() { }//构造函数私有化

    //键标识事件名字
    //值表示要执行的逻辑
    private Dictionary<EventDefine,Delegate> eventsDictionary = new Dictionary<EventDefine,Delegate>();

    private void OnListenerAdding(EventDefine eventDefine,Delegate callBack)//该方法是用来安全校验的，检验一个事件名里面的所添加的委托是否为一个类型；还有个功能就是为字典添加新的事件名字
    {
        if(!eventsDictionary.ContainsKey(eventDefine))
        {
            eventsDictionary.Add(eventDefine, null);//如果没有该事件名就添加新的字典名
        }
        Delegate d= eventsDictionary[eventDefine];
        if(d != null&&d.GetType()!=callBack.GetType())//这是获取委托类型并判断
        {
            throw new Exception($"尝试为事件{eventDefine}添加不同类型的委托，当前事件对应的委托是{d.GetType()},要添加事件的委托类型为{callBack.GetType()}");
        }        
    }
    private void OnListenerRemoving(EventDefine eventDefine,Delegate callBack)//移除监听事件的安全校验，1.是否有该事件码，2.校验了要移除的事件类型是否正确（这里包括是否为空和是否类型一致），
    {
        if(eventsDictionary.ContainsKey(eventDefine))//判断是否有事件码
        {
            Delegate d= eventsDictionary[eventDefine];
            if(d==null)//判断事件是否为空
            {
                throw new Exception($"移除监听错误：事件{eventDefine}没有对应的委托");
            }
            else if(d.GetType()!=callBack.GetType())//判断事件类型是否一致
            {
                throw new Exception($"移除监听错误：尝试为事件{eventDefine}移除不同类型的委托，当前委托类型为{d.GetType()}，要移除的委托类型为{callBack.GetType()}");
            }
        }
        else
        {
            throw new Exception($"移除监听错误：没有事件码{eventDefine}");
        }
    }
    public void OnListenerRemoved(EventDefine eventDefine)//移除没有对应委托事件的事件码
    {
        if (eventsDictionary[eventDefine]==null)
        {
            eventsDictionary.Remove(eventDefine);
        }
    }
    #region 添加监听
    //添加无参类型的监听
    public void AddListener(EventDefine eventDefine,CallBack callBack)//添加监听事件（也就是将事件码和事件进行绑定）
    {
        OnListenerAdding(eventDefine, callBack);
        eventsDictionary[eventDefine] = (CallBack)eventsDictionary[eventDefine] + callBack;
    }
    //Single parameters
    public void AddListener<T>(EventDefine eventDefine, CallBack<T> callBack)
    {
        OnListenerAdding(eventDefine, callBack);
        eventsDictionary[eventDefine] = (CallBack<T>)eventsDictionary[eventDefine] + callBack;
    }
    //two parameters
    public void AddListener<T, X>(EventDefine eventDefine, CallBack<T, X> callBack)
    {
        OnListenerAdding(eventDefine, callBack);
        eventsDictionary[eventDefine] = (CallBack<T, X>)eventsDictionary[eventDefine] + callBack;
    }
    //three parameters
    public void AddListener<T, X, Y>(EventDefine eventDefine, CallBack<T, X, Y> callBack)
    {
        OnListenerAdding(eventDefine, callBack);
        eventsDictionary[eventDefine] = (CallBack<T, X, Y>)eventsDictionary[eventDefine] + callBack;
    }
    //four parameters
    public void AddListener<T, X, Y, Z>(EventDefine eventDefine, CallBack<T, X, Y, Z> callBack)
    {
        OnListenerAdding(eventDefine, callBack);
        eventsDictionary[eventDefine] = (CallBack<T, X, Y, Z>)eventsDictionary[eventDefine] + callBack;
    }
    //five parameters
    public void AddListener<T, X, Y, Z, W>(EventDefine eventDefine, CallBack<T, X, Y, Z, W> callBack)
    {
        OnListenerAdding(eventDefine, callBack);
        eventsDictionary[eventDefine] = (CallBack<T, X, Y, Z, W>)eventsDictionary[eventDefine] + callBack;
    }
    #endregion

    #region 移除监听
    //移除无参类型的监听
    public void RemoveListener(EventDefine eventDefine,CallBack callBack)
    {
        OnListenerRemoving (eventDefine, callBack);
        eventsDictionary[eventDefine]= (CallBack)eventsDictionary[eventDefine] - callBack;
        OnListenerRemoved (eventDefine);
    }
    //single parameters
    public void RemoveListener<T>(EventDefine eventDefine, CallBack<T> callBack)
    {
        OnListenerRemoving(eventDefine, callBack);
        eventsDictionary[eventDefine] = (CallBack<T>)eventsDictionary[eventDefine] - callBack;
        OnListenerRemoved(eventDefine);
    }
    //two parameters
    public void RemoveListener<T, X>(EventDefine eventDefine, CallBack<T, X> callBack)
    {
        OnListenerRemoving(eventDefine, callBack);
        eventsDictionary[eventDefine] = (CallBack<T, X>)eventsDictionary[eventDefine] - callBack;
        OnListenerRemoved(eventDefine);
    }
    //three parameters
    public void RemoveListener<T, X, Y>(EventDefine eventDefine, CallBack<T, X, Y> callBack)
    {
        OnListenerRemoving(eventDefine, callBack);
        eventsDictionary[eventDefine] = (CallBack<T, X, Y>)eventsDictionary[eventDefine] - callBack;
        OnListenerRemoved(eventDefine);
    }
    //four parameters
    public void RemoveListener<T, X, Y, Z>(EventDefine eventDefine, CallBack<T, X, Y, Z> callBack)
    {
        OnListenerRemoving(eventDefine, callBack);
        eventsDictionary[eventDefine] = (CallBack<T, X, Y, Z>)eventsDictionary[eventDefine] - callBack;
        OnListenerRemoved(eventDefine);
    }
    //five parameters
    public void RemoveListener<T, X, Y, Z, W>(EventDefine eventDefine, CallBack<T, X, Y, Z, W> callBack)
    {
        OnListenerRemoving(eventDefine, callBack);
        eventsDictionary[eventDefine] = (CallBack<T, X, Y, Z, W>)eventsDictionary[eventDefine] - callBack;
        OnListenerRemoved(eventDefine);
    }
    #endregion

    #region 广播监听
    //广播无参类型的监听
    public void Broadcast(EventDefine eventDefine)
    {
        Delegate d;
        if(eventsDictionary.TryGetValue(eventDefine, out d))//TryGetValue方法的返回值是bool值
        {
            CallBack callBack = d as CallBack;//向下转型，如果d的委托类型和转换类型不一致就会导致d为null
            if(callBack != null)
            {
               // callBack();这两者的区别就在于这个委托如果为空的话调用会抛出异常
               callBack?.Invoke();
            }
            else
            {
                throw new Exception($"广播事件错误：事件{eventDefine}对应委托具有不同的类型");//1.一个事件码对应了几个不同的委托类型，2.事件码对应的委托为空
            }
        }       
    }
    //single parameters
    public void Broadcast<T>(EventDefine eventType, T arg)
    {
        Delegate d;
        if (eventsDictionary.TryGetValue(eventType, out d))
        {
            CallBack<T> callBack = d as CallBack<T>;
            if (callBack != null)
            {
                callBack(arg);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    //two parameters
    public void Broadcast<T, X>(EventDefine eventType, T arg1, X arg2)
    {
        Delegate d;
        if (eventsDictionary.TryGetValue(eventType, out d))
        {
            CallBack<T, X> callBack = d as CallBack<T, X>;
            if (callBack != null)
            {
                callBack(arg1, arg2);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    //three parameters
    public void Broadcast<T, X, Y>(EventDefine eventType, T arg1, X arg2, Y arg3)
    {
        Delegate d;
        if (eventsDictionary.TryGetValue(eventType, out d))
        {
            CallBack<T, X, Y> callBack = d as CallBack<T, X, Y>;
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    //four parameters
    public void Broadcast<T, X, Y, Z>(EventDefine eventType, T arg1, X arg2, Y arg3, Z arg4)
    {
        Delegate d;
        if (eventsDictionary.TryGetValue(eventType, out d))
        {
            CallBack<T, X, Y, Z> callBack = d as CallBack<T, X, Y, Z>;
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3, arg4);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    //five parameters
    public void Broadcast<T, X, Y, Z, W>(EventDefine eventType, T arg1, X arg2, Y arg3, Z arg4, W arg5)
    {
        Delegate d;
        if (eventsDictionary.TryGetValue(eventType, out d))
        {
            CallBack<T, X, Y, Z, W> callBack = d as CallBack<T, X, Y, Z, W>;
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3, arg4, arg5);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
    #endregion
}
