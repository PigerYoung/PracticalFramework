using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// MonoBehavior�ġ�ִ���ߡ��ű��������ű�����ͨ����������Э�̣�Ҳ����ͨ����������Update��FixedUpdate��LateUpdate
/// </summary>
public class MonoCtroller : MonoBehaviour
{
    private MonoCtroller() { }//���ﹹ�캯��˽�л����Է�ֹnew�ö��󣬵��ǲ���ִ��

    event UnityAction updateEvent;//���������ں�����ִ�е��¼�
    event UnityAction fixedUpdateEvent;//��fixedupdateִ�е��¼�
    event UnityAction lateUpdateEvent;//��lateUpdateִ�е��¼�

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
        updateEvent?.Invoke();//�����¼��е������﷨��ʽ��Ч�����Ϸ�һ��
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
    public void AddUpdateListener(UnityAction action)//Ϊ�ⲿ�ṩ����¼��ĺ���
    {
        updateEvent += action;
    }
    public void RemoveUpdateListener(UnityAction action)//�Ƴ��¼�
    {
        updateEvent -= action;
    }
    public void RemoveAllUpdateListener()//�Ƴ����е��¼�
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
    public void RemoveAllListeners()//�Ƴ����м����¼�
    {
        RemoveAllUpdateListener();
        RemoveAllFixedUpdateListener();
        RemoveAllLateUpdateListener();
    }

}
