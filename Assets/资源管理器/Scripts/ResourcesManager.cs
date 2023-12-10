using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ������Դ������
/// </summary>
public class ResourcesManager : SingletonPatternMonoBase<ResourcesManager>
{
    #region �첽����
    /// <summary>
    /// <para>�첽����Resources�ļ�����ָ�����͵���Դ,������ί��ʹ����</para>
    /// <para>�������ͬ���ͣ�������ͬ·������Դ���ͻ���ص�һ���ҵ�����Դ</para>
    /// </summary>
    /// <typeparam name="T">���ص���Դ����</typeparam>
    /// <param name="path">��Դ�ļ���·��</param>
    /// <param name="callback">��������Դ��ִ�е�ί��</param>
    public void LoadAsnc<T>(string path,UnityAction<T> callback=null) where T: Object
    {
        StartCoroutine(LoadAsyncCoroutine(path, callback)); //����Э�̣������õ��Ǽ̳���Mono�ĵ������࣬������ü̳���Mono�ľ���Ҫ�õ�Mono������
    }
    IEnumerator LoadAsyncCoroutine<T>(string path, UnityAction<T> callback = null) where T : Object//��Ϊ���س�����Դ���������õģ�����Ҫ�Ѽ��ص���Դ����ί�У���˾��õ���һ���������͵�ί��
    {
        //��ʼ�첽����
        ResourceRequest resourceRequest = Resources.LoadAsync<T>(path);
        //�ȴ���Դ�������
        yield return resourceRequest;

        callback?.Invoke(resourceRequest.asset as T);//��������ϵ���Դ����ί�в���������Դת��Ϊ����T��
    }
    #endregion

    #region ͬ������

    /// <summary>
    /// ͬ������Resources�ļ�����ָ�����͵���Դ
    /// �������ͬ���ͣ�������ͬ·������Դ���ͻ���ص�һ���ҵ�����Դ
    /// </summary>
    /// <typeparam name="T">���ص���Դ����</typeparam>
    /// <param name="path">��Դ�ļ���·��</param>
    /// <returns></returns>
    public T Load<T>(string path) where T : Object
    {
       return Resources.Load<T>(path);
    }

    /// <summary>
    /// ͬ������Resources�ļ���������ָ�����͵���Դ
    /// </summary>
    /// <typeparam name="T">���ص���Դ����</typeparam>
    /// <param name="path">��Դ�ļ���·��</param>
    /// <returns></returns>
    public T[] LoadAll<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path);
    }
    #endregion

    #region ж����Դ���첽��

    /// <summary>
    /// �첽ж����Դ���������л�����ʱʹ��
    /// </summary>
    /// <param name="callback">ж����ɺ�ִ�е�ί��</param>
    public void UnloadUnusedAssets(UnityAction callback = null)//ж��û���õ���Դ
    {
        StartCoroutine (UnloadUnusedAssetsCoroutine(callback));
    }
    IEnumerator UnloadUnusedAssetsCoroutine(UnityAction callback=null)
    {
        //��ʼж����Դ
       AsyncOperation asyncOperation=Resources.UnloadUnusedAssets();
        //�ȴ���Դж��
       while(asyncOperation.progress<1.0f)
       {
           yield return null;
       }
       //��Դж�����ִ�е��߼�
       callback?.Invoke();
    }
    #endregion
}
