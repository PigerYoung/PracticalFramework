using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 对象池
/// </summary>
public class ObjectPool :MonoBehaviour
{
    public GameObject prefab;//对象池中的游戏对象

    public int capicity=100;//对象池的初始容量(这里根据项目需求决定)

    public Queue<GameObject> pool = new Queue<GameObject>();//用队列做对象池
    public Transform usedPool;//正在使用的对象统一放在usedPool对象下

   

    private void Start()
    {
        
    }
    public void InitPool()//初始化对象池，让对象池中在最开始存在一定数量的对象
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

    //从对象池取对象
    public GameObject GetObject()
    {
        if(prefab==null)
        {
            throw new Exception($"对象池未指定池内对象{prefab}");
        }
        GameObject temp;
        if(pool!=null&&pool.Count>0)//这里是判断对象池存在且对象池里面有对象
        {
            temp= pool.Dequeue();//将队列的第一个对象出队
            temp.SetActive(true);//并设置为激活状态           
        }
        else//如果池子里面没有对象了(这里的处理是直接实例化对象，也可以实例化在对象池中，可以根据项目需求调整)
        {
            temp= Instantiate(prefab);
        }
        return temp;
    }

    //放回对象，将对象放入对象池中
    public void ReturnObject(GameObject gameObject )
    {
        if (usedPool == null) return;
        gameObject.SetActive(false);
        pool.Enqueue(gameObject);
    }
}
