using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �¼����Ĺ�����
/// </summary>
public class EventCenterManager : SingletonPatternBase<EventCenterManager>
{
    private EventCenterManager() { }//���캯��˽�л�

    //����ʶ�¼�����
    //ֵ��ʾҪִ�е��߼�
    private Dictionary<EventDefine,Delegate> eventsDictionary = new Dictionary<EventDefine,Delegate>();

    private void OnListenerAdding(EventDefine eventDefine,Delegate callBack)//�÷�����������ȫУ��ģ�����һ���¼������������ӵ�ί���Ƿ�Ϊһ�����ͣ����и����ܾ���Ϊ�ֵ�����µ��¼�����
    {
        if(!eventsDictionary.ContainsKey(eventDefine))
        {
            eventsDictionary.Add(eventDefine, null);//���û�и��¼���������µ��ֵ���
        }
        Delegate d= eventsDictionary[eventDefine];
        if(d != null&&d.GetType()!=callBack.GetType())//���ǻ�ȡί�����Ͳ��ж�
        {
            throw new Exception($"����Ϊ�¼�{eventDefine}��Ӳ�ͬ���͵�ί�У���ǰ�¼���Ӧ��ί����{d.GetType()},Ҫ����¼���ί������Ϊ{callBack.GetType()}");
        }        
    }
    private void OnListenerRemoving(EventDefine eventDefine,Delegate callBack)//�Ƴ������¼��İ�ȫУ�飬1.�Ƿ��и��¼��룬2.У����Ҫ�Ƴ����¼������Ƿ���ȷ����������Ƿ�Ϊ�պ��Ƿ�����һ�£���
    {
        if(eventsDictionary.ContainsKey(eventDefine))//�ж��Ƿ����¼���
        {
            Delegate d= eventsDictionary[eventDefine];
            if(d==null)//�ж��¼��Ƿ�Ϊ��
            {
                throw new Exception($"�Ƴ����������¼�{eventDefine}û�ж�Ӧ��ί��");
            }
            else if(d.GetType()!=callBack.GetType())//�ж��¼������Ƿ�һ��
            {
                throw new Exception($"�Ƴ��������󣺳���Ϊ�¼�{eventDefine}�Ƴ���ͬ���͵�ί�У���ǰί������Ϊ{d.GetType()}��Ҫ�Ƴ���ί������Ϊ{callBack.GetType()}");
            }
        }
        else
        {
            throw new Exception($"�Ƴ���������û���¼���{eventDefine}");
        }
    }
    public void OnListenerRemoved(EventDefine eventDefine)//�Ƴ�û�ж�Ӧί���¼����¼���
    {
        if (eventsDictionary[eventDefine]==null)
        {
            eventsDictionary.Remove(eventDefine);
        }
    }
    #region ��Ӽ���
    //����޲����͵ļ���
    public void AddListener(EventDefine eventDefine,CallBack callBack)//��Ӽ����¼���Ҳ���ǽ��¼�����¼����а󶨣�
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

    #region �Ƴ�����
    //�Ƴ��޲����͵ļ���
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

    #region �㲥����
    //�㲥�޲����͵ļ���
    public void Broadcast(EventDefine eventDefine)
    {
        Delegate d;
        if(eventsDictionary.TryGetValue(eventDefine, out d))//TryGetValue�����ķ���ֵ��boolֵ
        {
            CallBack callBack = d as CallBack;//����ת�ͣ����d��ί�����ͺ�ת�����Ͳ�һ�¾ͻᵼ��dΪnull
            if(callBack != null)
            {
               // callBack();�����ߵ�������������ί�����Ϊ�յĻ����û��׳��쳣
               callBack?.Invoke();
            }
            else
            {
                throw new Exception($"�㲥�¼������¼�{eventDefine}��Ӧί�о��в�ͬ������");//1.һ���¼����Ӧ�˼�����ͬ��ί�����ͣ�2.�¼����Ӧ��ί��Ϊ��
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
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧί�о��в�ͬ������", eventType));
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
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧί�о��в�ͬ������", eventType));
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
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧί�о��в�ͬ������", eventType));
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
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧί�о��в�ͬ������", eventType));
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
                throw new Exception(string.Format("�㲥�¼������¼�{0}��Ӧί�о��в�ͬ������", eventType));
            }
        }
    }
    #endregion
}
