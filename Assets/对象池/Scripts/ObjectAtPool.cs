using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �����ڶ�����еĶ�����صĽű�
///<para> ��������Ϸ���󼤻��ʧЧ���Ӷ����ȡ����ͷ������أ�ʱ���ú���</para>
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
        Debug.Log("������ϴ����");
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
