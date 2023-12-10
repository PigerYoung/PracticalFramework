using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono�������ű����������ű�һ��Ϊ����ģʽ
/// �ⲿ���Ƿ��ʸýű���ͨ���ýű�ȥ��ִ���ߵ��ÿ���Э�̻���֡����
/// </summary>
public class MonoManager : SingletonPatternBase<MonoManager>
{
    private MonoManager() { }//���췽��˽�л�

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


    public Coroutine StartCoroutine(IEnumerator routine)//����Э�̷����ⲻ��untity�ṩ��Э�̣���Ϊ�ýű�û�м̳�MonoBehavior������Ӧ������MonoCtroller������Э��
    {
       return MonoCtroller.StartCoroutine(routine);
    }

    public void StopCoroutine(IEnumerator coroutine)//ֹͣЭ��
    {
        if (coroutine != null)
        {
            MonoCtroller.StopCoroutine(coroutine);
        }       
    }
    public void StopCoroutine(Coroutine coroutine)//����
    {
        if(coroutine != null)
        {
            MonoCtroller.StopCoroutine(coroutine);
        }     
    }
    public void StopAllCoroutine()//���ⲿͨ������ֹͣ����Э��
    {
        MonoCtroller.StopAllCoroutines();
    }

    public void AddUpdateListener(UnityAction action)//���ⲿͨ���÷��������update�ļ����¼�
    {
        MonoCtroller.AddUpdateListener(action);
    }
    public void RemoveUpdateListener(UnityAction action)//�Ƴ��¼�
    {
        MonoCtroller.RemoveUpdateListener(action);
    }
    public void RemoveAllUpdateListener()//�Ƴ����е��¼�
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

    public void RemoveAllListenrs()//�Ƴ����м����¼�
    {
        MonoCtroller.RemoveAllListeners();
    }
}
