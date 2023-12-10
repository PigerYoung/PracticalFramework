using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �����
/// </summary>
public class ObjectPool :MonoBehaviour
{
    public GameObject prefab;//������е���Ϸ����

    public int capicity=100;//����صĳ�ʼ����(���������Ŀ�������)

    public Queue<GameObject> pool = new Queue<GameObject>();//�ö����������
    public Transform usedPool;//����ʹ�õĶ���ͳһ����usedPool������

   

    private void Start()
    {
        
    }
    public void InitPool()//��ʼ������أ��ö���������ʼ����һ�������Ķ���
    {
        if (prefab == null||capicity<=0) return;

        GameObject temp;
        for(int i = 0; i < capicity; i++)
        {
            temp = Instantiate(prefab, usedPool);
            pool.Enqueue(temp);
            temp.SetActive(false);
        }
    }

    //�Ӷ����ȡ����
    public GameObject GetObject()
    {
        if(prefab==null)
        {
            throw new Exception($"�����δָ�����ڶ���{prefab}");
        }
        GameObject temp;
        if(pool!=null&&pool.Count>0)//�������ж϶���ش����Ҷ���������ж���
        {
            temp= pool.Dequeue();//�����еĵ�һ���������
            temp.SetActive(true);//������Ϊ����״̬           
        }
        else//�����������û�ж�����(����Ĵ�����ֱ��ʵ��������Ҳ����ʵ�����ڶ�����У����Ը�����Ŀ�������)
        {
            temp= Instantiate(prefab);
        }
        return temp;
    }

    //�Żض��󣬽��������������
    public void ReturnObject(GameObject gameObject )
    {
        if (usedPool == null) return;
        gameObject.SetActive(false);
        pool.Enqueue(gameObject);
    }
}
