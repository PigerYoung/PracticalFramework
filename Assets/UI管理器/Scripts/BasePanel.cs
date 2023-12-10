using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各个面板的基类，每个面板都需要继承自该基类
/// 该基类有四个面板的声明周期
/// </summary>
public class BasePanel : MonoBehaviour
{
    /// <summary>
    /// 显示出界面
    /// </summary>
    public virtual void OnEnter()
    {

    }
    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {

    }
    /// <summary>
    /// 页面继续
    /// </summary>
    public virtual void OnResume()
    {

    }
    /// <summary>
    /// 关闭界面
    /// </summary>
    public virtual void OnExit()
    {

    }
        
}
