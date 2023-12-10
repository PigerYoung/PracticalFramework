using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 这是在对象池中的对象挂载的脚本
///<para> 用来在游戏对象激活和失效（从对象池取对象和放入对象池）时调用函数</para>
/// </summary>
public class ObjectAtPool : MonoBehaviour
{
    public void OnEnable()
    {
        StartCoroutine(HideObject());
    }
    public void OnDisable()
    {
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Debug.Log("这里清洗数据");
    }
    IEnumerator HideObject()
    {
        yield return new WaitForSeconds(3);
        ObjectPoolManager.Instance.ReturnObject(this.gameObject);
        
    }
    public void Start()
    {
        if(gameObject.transform.parent == null)
        {
            Destroy(gameObject,3f);
        }
    }
}
