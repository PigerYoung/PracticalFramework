using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// �����л�������
/// </summary>
public class LoadSceneManager : SingletonPatternBase<LoadSceneManager>
{
    /// <summary>
    /// �����л�����ǰ����
    /// </summary>
    public void LoadActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// �л�����һ������
    /// </summary>
    /// <param name="isCyclical">��������������Ƿ�ѭ��</param>
    public void LoadNextScene(bool isCyclical)
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(buildIndex>SceneManager.sceneCountInBuildSettings-1)
        {
            if(isCyclical)
            {
                buildIndex = 0;
            }
            else
            {
                Debug.LogWarning($"���س���ʧ�ܣ�Ҫ���صĳ���������{buildIndex},�������������");
                return;
            }
        }
        SceneManager.LoadScene(buildIndex);
    }

    /// <summary>
    /// �л���һ������
    /// </summary>
    /// <param name="isCyclical"></param>
    public void LoadPreviousScene(bool isCyclical)
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex -1;
        if (buildIndex <0)
        {
            if (isCyclical)
            {
                buildIndex = SceneManager.sceneCountInBuildSettings - 1;
            }
            else
            {
                Debug.LogWarning($"���س���ʧ�ܣ�Ҫ���صĳ���������{buildIndex},��������С����");
                return;
            }
        }
        SceneManager.LoadScene(buildIndex);
    }

    /// <summary>
    /// �첽���س���
    /// </summary>
    /// <param name="sceneName">������</param>
    /// <param name="loading">���½�������ί��</param>
    /// <param name="callback">������ɺ�Ļص�����</param>
    /// <param name="setActiveAfterComplete">������Ϻ��Ƿ��Զ���ת����</param>
    /// <param name="mode">����ģʽ��Ĭ�ϲ��ò�����ʽ</param>
    public void LoadSceneAsync(string sceneName, UnityAction<float> loading = null, UnityAction<AsyncOperation> callback = null, bool setActiveAfterComplete = true, LoadSceneMode mode = LoadSceneMode.Single)
    {
        MonoManager.Instance.StartCoroutine(LoadSceneCoroutine(sceneName, loading, callback, setActiveAfterComplete, mode));
    }
    IEnumerator LoadSceneCoroutine(string sceneName, UnityAction<float> loading = null, UnityAction<AsyncOperation> callback = null, bool setActiveAfterComplete = true, LoadSceneMode mode = LoadSceneMode.Single)
    {
        //��ʼ������Դ
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName,mode);

        asyncOperation.allowSceneActivation = false;
        //�ȴ���Դ�������
        while (asyncOperation.progress < 0.9f)
        {
            loading?.Invoke(asyncOperation.progress);//������ͨ��ί�д����ⲿ
            yield return null;
        }
        //����ѭ���󣬼��ؽ���Ϊ90%
        loading?.Invoke(1);//��ί�еĽ�������Ϊ1��ʵ�ʽ��Ȼ���0.9���Ӿ����Ǽ�������ˣ�
        asyncOperation.allowSceneActivation = setActiveAfterComplete;//Ϊtrue�Ļ������ʱ��ż�������ʣ���10%��������Ϻ�ֱ����ת����

        //��Դ������ɺ�Ļص�����
        callback?.Invoke(asyncOperation);//����Ϸ�allowSceneActivationΪfalse�Ļ�������Ҫ�ø���ɺ�Ļص���������Ϊtrue��������������첽�ļ���
    }

    /// <summary>
    /// �첽���س���
    /// </summary>
    /// <param name="sceneIndex">��������</param>
    /// <param name="loading">���½�������ί��</param>
    /// <param name="callback">������ɺ�Ļص�����</param>
    /// <param name="setActiveAfterComplete">������Ϻ��Ƿ��Զ���ת����</param>
    /// <param name="mode">����ģʽ��Ĭ�ϲ��ò�����ʽ</param>
    public void LoadSceneAsync(int sceneIndex,UnityAction<float> loading=null, UnityAction<AsyncOperation> callback = null,bool setActiveAfterComplete=true, LoadSceneMode mode = LoadSceneMode.Single)
    {
        MonoManager.Instance.StartCoroutine(LoadSceneCoroutine(sceneIndex, loading,callback, setActiveAfterComplete, mode));
    }
    IEnumerator LoadSceneCoroutine(int sceneIndex, UnityAction<float> loading = null, UnityAction<AsyncOperation> callback = null, bool setActiveAfterComplete = true, LoadSceneMode mode = LoadSceneMode.Single)
    {
        //��ʼ������Դ
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex, mode);

        asyncOperation.allowSceneActivation = false;
        //�ȴ���Դ�������
        while(asyncOperation.progress<0.9f)
        {
            loading?.Invoke(asyncOperation.progress);//������ͨ��ί�д����ⲿ
            yield return null;
        }
        //����ѭ���󣬼��ؽ���Ϊ90%
        loading?.Invoke(1);//��ί�еĽ�������Ϊ1��ʵ�ʽ��Ȼ���0.9���Ӿ����Ǽ�������ˣ�
        asyncOperation.allowSceneActivation = setActiveAfterComplete;//Ϊtrue�Ļ������ʱ��ż�������ʣ���10%��������Ϻ�ֱ����ת����

        //��Դ������ɺ�Ļص�����
        callback?.Invoke(asyncOperation);//����Ϸ�allowSceneActivationΪfalse�Ļ�������Ҫ�ø���ɺ�Ļص���������Ϊtrue��������������첽�ļ���
    }
}
