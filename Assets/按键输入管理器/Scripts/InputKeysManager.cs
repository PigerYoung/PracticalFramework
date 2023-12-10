using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������������
/// </summary>
public class InputKeysManager :SingletonPatternMonoBase<InputKeysManager>
{
    public bool IsActive { get; private set; }//�����������ı�ʶλ���Ƿ����ýű��������������

    public void SetActive(bool isActive)//�����������Ŀ���
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

        foreach (KeyCode item in Enum.GetValues(typeof(KeyCode)))//�����ǶԸ÷������Ż�����Ϊ��������е�KeyCodeö�����Ͷ���if�ж�д�ڸ÷����У��ͻ�ǳ��鷳����˾��ñ�������ʽÿһ֡��������ö��(�൱��ÿ��������һ�飬������ñ����ķ���������)
        {
            CheckKeysCode(item);
        }
    }
    void CheckKeysCode(KeyCode key)//�ñ����ķ�ʽ���滻if(Input.GetKeyDown(KeyCode.W)){} else if(Input.GetKeyDown(KeyCode.S)){}.......
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
