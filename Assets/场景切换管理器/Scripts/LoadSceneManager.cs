using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景切换管理器
/// </summary>
public class LoadSceneManager : SingletonPatternBase<LoadSceneManager>
{
    /// <summary>
    /// 重新切换到当前场景
    /// </summary>
    public void LoadActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// 切换到下一个场景
    /// </summary>
    /// <param name="isCyclical">超过了最大索引是否循环</param>
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
                Debug.LogWarning($"加载场景失败，要加载的场景索引是{buildIndex},超出了最大索引");
                return;
            }
        }
        SceneManager.LoadScene(buildIndex);
    }

    /// <summary>
    /// 切换上一个场景
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
                Debug.LogWarning($"加载场景失败，要加载的场景索引是{buildIndex},超出了最小索引");
                return;
            }
        }
        SceneManager.LoadScene(buildIndex);
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName">场景名</param>
    /// <param name="loading">更新进度条的委托</param>
    /// <param name="callback">加载完成后的回调函数</param>
    /// <param name="setActiveAfterComplete">加载完毕后是否自动跳转场景</param>
    /// <param name="mode">加载模式，默认采用不叠加式</param>
    public void LoadSceneAsync(string sceneName, UnityAction<float> loading = null, UnityAction<AsyncOperation> callback = null, bool setActiveAfterComplete = true, LoadSceneMode mode = LoadSceneMode.Single)
    {
        MonoManager.Instance.StartCoroutine(LoadSceneCoroutine(sceneName, loading, callback, setActiveAfterComplete, mode));
    }
    IEnumerator LoadSceneCoroutine(string sceneName, UnityAction<float> loading = null, UnityAction<AsyncOperation> callback = null, bool setActiveAfterComplete = true, LoadSceneMode mode = LoadSceneMode.Single)
    {
        //开始加载资源
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName,mode);

        asyncOperation.allowSceneActivation = false;
        //等待资源加载完毕
        while (asyncOperation.progress < 0.9f)
        {
            loading?.Invoke(asyncOperation.progress);//将进度通过委托传到外部
            yield return null;
        }
        //跳出循环后，加载进度为90%
        loading?.Invoke(1);//将委托的进度设置为1，实际进度还是0.9（视觉上是加载完毕了）
        asyncOperation.allowSceneActivation = setActiveAfterComplete;//为true的话，这个时候才继续加载剩余的10%，加载完毕后直接跳转场景

        //资源加载完成后的回调函数
        callback?.Invoke(asyncOperation);//如果上方allowSceneActivation为false的话，就需要用该完成后的回调将气设置为true，这样才能完成异步的加载
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneIndex">场景索引</param>
    /// <param name="loading">更新进度条的委托</param>
    /// <param name="callback">加载完成后的回调函数</param>
    /// <param name="setActiveAfterComplete">加载完毕后是否自动跳转场景</param>
    /// <param name="mode">加载模式，默认采用不叠加式</param>
    public void LoadSceneAsync(int sceneIndex,UnityAction<float> loading=null, UnityAction<AsyncOperation> callback = null,bool setActiveAfterComplete=true, LoadSceneMode mode = LoadSceneMode.Single)
    {
        MonoManager.Instance.StartCoroutine(LoadSceneCoroutine(sceneIndex, loading,callback, setActiveAfterComplete, mode));
    }
    IEnumerator LoadSceneCoroutine(int sceneIndex, UnityAction<float> loading = null, UnityAction<AsyncOperation> callback = null, bool setActiveAfterComplete = true, LoadSceneMode mode = LoadSceneMode.Single)
    {
        //开始加载资源
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex, mode);

        asyncOperation.allowSceneActivation = false;
        //等待资源加载完毕
        while(asyncOperation.progress<0.9f)
        {
            loading?.Invoke(asyncOperation.progress);//将进度通过委托传到外部
            yield return null;
        }
        //跳出循环后，加载进度为90%
        loading?.Invoke(1);//将委托的进度设置为1，实际进度还是0.9（视觉上是加载完毕了）
        asyncOperation.allowSceneActivation = setActiveAfterComplete;//为true的话，这个时候才继续加载剩余的10%，加载完毕后直接跳转场景

        //资源加载完成后的回调函数
        callback?.Invoke(asyncOperation);//如果上方allowSceneActivation为false的话，就需要用该完成后的回调将气设置为true，这样才能完成异步的加载
    }
}
