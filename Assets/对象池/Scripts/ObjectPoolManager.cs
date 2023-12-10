using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 对象池管理器，可以通过这个管理器从对象池生成对象，也可以回收游戏对象进对象池
/// 是用来管理多个对象池的管理器
/// </summary>
public class ObjectPoolManager : SingletonPatternBase<ObjectPoolManager>
{
    GameObject poolsParent;//所有对象池的父物体
    readonly string poolsParentName = "ObjectPools"; //所有对象池的共同父物体在Hierarchy窗口的名字
    private List<ObjectPool> objectPoolsList= new List<ObjectPool>();//对象池列表，用来存放当前管理器中的对象池

    /// <summary>
    /// 根据预制件从对象池中获取游戏脚本
    /// </summary>
    /// <param name="prefab">想要获取的游戏对象</param>
    /// <returns></returns>
    public GameObject GetObject(GameObject prefab)
    {
        if (prefab == null) return null;

        CreatPoolParentIfNull();//如果对象池父物体为空，就创建

        ObjectPool objectPool = FindObjectPool(prefab);

        GameObject gameObject = objectPool.GetObject();

        return gameObject;
    }

    public void ReturnObject(GameObject gameObject)
    {
        if(gameObject == null) return;

        foreach (ObjectPool pool in objectPoolsList)
        {
            if(pool.prefab== gameObject.transform.parent.GetComponent<ObjectPool>().prefab)
            { 
                pool.ReturnObject(gameObject);
            }
        }
        
    }
    private void CreatPoolParentIfNull()//如果没有对象池的父物体用来装多个对象池，就需要创建父对象
    {
        if(poolsParent == null)
        {
            poolsParent=new GameObject(poolsParentName);
        }
    }
    private ObjectPool FindObjectPool(GameObject prefab)//因为每个对象池都需要对应一个对象池中的游戏物体，因此可以用该游戏物体来识别唯一的对象池
    {
        if (prefab == null) return null;
        CreatPoolParentIfNull();
        foreach (ObjectPool pool in objectPoolsList)
        {
            if(pool.prefab==prefab)
            {
                return pool;
            }
        }

        ObjectPool objectPool = new GameObject($"ObjectPool{prefab.name}").AddComponent<ObjectPool>();//如果objectPoolsList没有对象池，就创建并添加
        objectPool.usedPool= objectPool.GetComponent<Transform>();
        objectPool.prefab = prefab;
        objectPool.transform.SetParent(poolsParent.transform);
        objectPoolsList.Add(objectPool);
        return objectPool;
    }

    /// <summary>
    /// 指定生成一定数量的游戏对象，最好在使用对象池之前先预加载一定数量的游戏对象
    /// </summary>
    /// <param name="prefab">生成的游戏对象</param>
    /// <param name="capicity">生成的数量</param>
    public void InitPool(GameObject prefab,int capicity)//根据指定容量初始化对象池
    {
        if (prefab == null || capicity < 0) return;

        ObjectPool objectPool =FindObjectPool(prefab);
        objectPool.capicity = capicity;
        objectPool.InitPool();
    }
}
