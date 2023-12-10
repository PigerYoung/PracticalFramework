using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// û�м̳�MonoBehaviour��һ����ͨ�ű�������������
/// </summary>
public class Player 
{
    Coroutine coroutine;//��coroutine��������Э�̼�¼����������������ͣЭ��
    public void Show()
    {
        coroutine = MonoManager.Instance.StartCoroutine(MyCoroutine());//����Ϳ��Կ�����Ȼû�м̳���MonoBehaviour�࣬���ǻ��ǿ�������Mono����������Э��
    }

    public void Hide()
    {
        //MonoManager.Instance.StopCoroutine(MyCoroutine());//��������д���bug���ᵼ��ֹͣ����Э�̣�ԭ������Ϊunity����Ϊ������Э�̺͹رյ�Э���ǲ�ͬ������Э��      
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
            Debug.Log("Э��ִ����");
            yield return null;
        }
    }

    public void Update()//�����Update������MonoBehavior�е�Update��ֻ��ȡ������
    {
        MonoManager.Instance.AddUpdateListener(DebugUpdate);//�������ʹ��lambda���ʽ����¼��Ļ��Ͳ����Ƴ����¼�����Ϊ��unity�л���Ϊ����һ���¼������Ƴ�ʱ������Ǹ�lambda���ʽ��unity���������ǵڶ����¼�������ͬһ���¼�
    }
    public void StopUpdate()//ֹͣupdate
    {
        MonoManager.Instance.RemoveUpdateListener(DebugUpdate);
    }
    public void StopAllUpdate()//ֹͣ���е�update
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
