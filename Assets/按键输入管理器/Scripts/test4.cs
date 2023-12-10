using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test4 : MonoBehaviour
{
    private void Awake()
    {
        InputKeysManager.Instance.SetActive(true);//这里直接进入场景后就开启按键管理
    }
}
