using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ����ع�����������ͨ������������Ӷ�������ɶ���Ҳ���Ի�����Ϸ����������
/// ����������������صĹ�����
/// </summary>
public class ObjectPoolManager : SingletonPatternBase<ObjectPoolManager>
{
    GameObject poolsParent;//���ж���صĸ�����
    readonly string poolsParentName = "ObjectPools"; //���ж���صĹ�ͬ��������Hierarchy���ڵ�����
    private List<ObjectPool> objectPoolsList= new List<ObjectPool>();//������б�������ŵ�ǰ�������еĶ����

    /// <summary>
    /// ����Ԥ�Ƽ��Ӷ�����л�ȡ��Ϸ�ű�
    /// </summary>
    /// <param name="prefab">��Ҫ��ȡ����Ϸ����</param>
    /// <returns></returns>
    public GameObject GetObject(GameObject prefab)
    {
        if (prefab == null) return null;

        CreatPoolParentIfNull();//�������ظ�����Ϊ�գ��ʹ���

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
    private void CreatPoolParentIfNull()//���û�ж���صĸ���������װ�������أ�����Ҫ����������
    {
        if(poolsParent == null)
        {
            poolsParent=new GameObject(poolsParentName);
        }
    }
    private ObjectPool FindObjectPool(GameObject prefab)//��Ϊÿ������ض���Ҫ��Ӧһ��������е���Ϸ���壬��˿����ø���Ϸ������ʶ��Ψһ�Ķ����
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

        ObjectPool objectPool = new GameObject($"ObjectPool{prefab.name}").AddComponent<ObjectPool>();//���objectPoolsListû�ж���أ��ʹ��������
        objectPool.usedPool= objectPool.GetComponent<Transform>();
        objectPool.prefab = prefab;
        objectPool.transform.SetParent(poolsParent.transform);
        objectPoolsList.Add(objectPool);
        return objectPool;
    }

    /// <summary>
    /// ָ������һ����������Ϸ���������ʹ�ö����֮ǰ��Ԥ����һ����������Ϸ����
    /// </summary>
    /// <param name="prefab">���ɵ���Ϸ����</param>
    /// <param name="capicity">���ɵ�����</param>
    public void InitPool(GameObject prefab,int capicity)//����ָ��������ʼ�������
    {
        if (prefab == null || capicity < 0) return;

        ObjectPool objectPool =FindObjectPool(prefab);
        objectPool.capicity = capicity;
        objectPool.InitPool();
    }
}
