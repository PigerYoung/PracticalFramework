using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 按键输入管理器
/// </summary>
public class InputKeysManager :SingletonPatternMonoBase<InputKeysManager>
{
    public bool IsActive { get; private set; }//按键管理器的标识位，是否启用脚本，监听玩家输入

    public void SetActive(bool isActive)//按键管理器的开关
    {
        IsActive= isActive;
    }
    public void Update()
    {
        CheckKeys();
    }
    public void CheckKeys()
    {
        if (!IsActive) return;

        foreach (KeyCode item in Enum.GetValues(typeof(KeyCode)))//这里是对该方法的优化，因为如果将所有的KeyCode枚举类型都用if判断写在该方法中，就会非常麻烦，因此就用遍历的形式每一帧遍历所有枚举(相当于每个键都按一遍，这里就用遍历的方法代替了)
        {
            CheckKeysCode(item);
        }
    }
    void CheckKeysCode(KeyCode key)//用遍历的方式来替换if(Input.GetKeyDown(KeyCode.W)){} else if(Input.GetKeyDown(KeyCode.S)){}.......
    {
        if(Input.GetKeyDown(key))
        {
            EventCenterManager.Instance.Broadcast<KeyCode>(EventDefine.GetkeyDown,key);
        }
        if (Input.GetKeyDown(key))
        {
            EventCenterManager.Instance.Broadcast<KeyCode>(EventDefine.GetKey, key);
        }
        if(Input.GetKeyUp(key))
        {
            EventCenterManager.Instance.Broadcast<KeyCode>(EventDefine.GetkeyUp, key);
        }
    }
}
