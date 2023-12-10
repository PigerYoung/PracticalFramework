using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Player_InputTest : MonoBehaviour
{
    private void Awake()
    {
        EventCenterManager.Instance.AddListener<KeyCode>(EventDefine.GetKey, CheckInputKeys);
        EventCenterManager.Instance.AddListener<KeyCode>(EventDefine.GetkeyUp, CheckInputKeyUp);
    }
    public void CheckInputKeys(KeyCode keyCode)//玩家移动的方法
    {
        switch (keyCode)
        {
            case KeyCode.W: 
                Debug.Log("向上移动");
                break;
            case KeyCode.S:
                Debug.Log("向下移动");
                break;
            case KeyCode.D:
                Debug.Log("向右移动");
                break;
            case KeyCode.A:
                Debug.Log("向左移动");
                break;
        }
    }
    public void CheckInputKeyUp(KeyCode keyCode)//抬起按键
    {
        switch (keyCode)
        {
            case KeyCode.W:
                Debug.Log("停止向上移动");
                break;
            case KeyCode.S:
                Debug.Log("停止向下移动");
                break;
            case KeyCode.D:
                Debug.Log("停止向右移动");
                break;
            case KeyCode.A:
                Debug.Log("停止向左移动");
                break;
        }
    }
    private void OnDestroy()
    {
        EventCenterManager.Instance.RemoveListener<KeyCode>(EventDefine.GetKey, CheckInputKeys);
        EventCenterManager.Instance.RemoveListener<KeyCode>(EventDefine.GetkeyUp, CheckInputKeyUp);
    }
}
