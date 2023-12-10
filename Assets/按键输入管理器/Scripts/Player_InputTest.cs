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
    public void CheckInputKeys(KeyCode keyCode)//����ƶ��ķ���
    {
        switch (keyCode)
        {
            case KeyCode.W: 
                Debug.Log("�����ƶ�");
                break;
            case KeyCode.S:
                Debug.Log("�����ƶ�");
                break;
            case KeyCode.D:
                Debug.Log("�����ƶ�");
                break;
            case KeyCode.A:
                Debug.Log("�����ƶ�");
                break;
        }
    }
    public void CheckInputKeyUp(KeyCode keyCode)//̧�𰴼�
    {
        switch (keyCode)
        {
            case KeyCode.W:
                Debug.Log("ֹͣ�����ƶ�");
                break;
            case KeyCode.S:
                Debug.Log("ֹͣ�����ƶ�");
                break;
            case KeyCode.D:
                Debug.Log("ֹͣ�����ƶ�");
                break;
            case KeyCode.A:
                Debug.Log("ֹͣ�����ƶ�");
                break;
        }
    }
    private void OnDestroy()
    {
        EventCenterManager.Instance.RemoveListener<KeyCode>(EventDefine.GetKey, CheckInputKeys);
        EventCenterManager.Instance.RemoveListener<KeyCode>(EventDefine.GetkeyUp, CheckInputKeyUp);
    }
}
