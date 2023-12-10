using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 加载资源管理器
/// </summary>
public class ResourcesManager : SingletonPatternMonoBase<ResourcesManager>
{
    #region 异步加载
    /// <summary>
    /// <para>异步加载Resources文件夹中指定类型的资源,并传入委托使用它</para>
    /// <para>如果有相同类型，并且相同路径的资源，就会加载第一个找到的资源</para>
    /// </summary>
    /// <typeparam name="T">加载的资源类型</typeparam>
    /// <param name="path">资源的加载路径</param>
    /// <param name="callback">加载完资源后执行的委托</param>
    public void LoadAsnc<T>(string path,UnityAction<T> callback=null) where T: Object
    {
        StartCoroutine(LoadAsyncCoroutine(path, callback)); //开启协程，这里用的是继承自Mono的单例基类，如果不用继承自Mono的就需要用到Mono管理器
    }
    IEnumerator LoadAsyncCoroutine<T>(string path, UnityAction<T> callback = null) where T : Object//因为加载出的资源就是拿来用的，就需要把加载的资源传入委托，因此就用到了一个参数类型的委托
    {
        //开始异步加载
        ResourceRequest resourceRequest = Resources.LoadAsync<T>(path);
        //等待资源加载完毕
        yield return resourceRequest;

        callback?.Invoke(resourceRequest.asset as T);//将加载完毕的资源传入委托参数（将资源转换为类型T）
    }
    #endregion

    #region 同步加载

    /// <summary>
    /// 同步加载Resources文件夹中指定类型的资源
    /// 如果有相同类型，并且相同路径的资源，就会加载第一个找到的资源
    /// </summary>
    /// <typeparam name="T">加载的资源类型</typeparam>
    /// <param name="path">资源的加载路径</param>
    /// <returns></returns>
    public T Load<T>(string path) where T : Object
    {
       return Resources.Load<T>(path);
    }

    /// <summary>
    /// 同步加载Resources文件夹中所有指定类型的资源
    /// </summary>
    /// <typeparam name="T">加载的资源类型</typeparam>
    /// <param name="path">资源的加载路径</param>
    /// <returns></returns>
    public T[] LoadAll<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path);
    }
    #endregion

    #region 卸载资源（异步）

    /// <summary>
    /// 异步卸载资源，往往在切换场景时使用
    /// </summary>
    /// <param name="callback">卸载完成后执行的委托</param>
    public void UnloadUnusedAssets(UnityAction callback = null)//卸载没有用的资源
    {
        StartCoroutine (UnloadUnusedAssetsCoroutine(callback));
    }
    IEnumerator UnloadUnusedAssetsCoroutine(UnityAction callback=null)
    {
        //开始卸载资源
       AsyncOperation asyncOperation=Resources.UnloadUnusedAssets();
        //等待资源卸载
       while(asyncOperation.progress<1.0f)
       {
           yield return null;
       }
       //资源卸载完后执行的逻辑
       callback?.Invoke();
    }
    #endregion
}
