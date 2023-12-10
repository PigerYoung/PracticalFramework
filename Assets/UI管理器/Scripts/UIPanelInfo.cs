using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 专门用来接收解析json的类
/// 这里直接用到的是枚举做成员变量，因为json中用的是int类型，如果json中用的字符串的话，这里还需要要用序列化的接口，将字符串转换为枚举
/// </summary>

[Serializable]
public class UIPanelInfo 
{
    public UIPanelType UIPanelType;
    public string Path;
}
