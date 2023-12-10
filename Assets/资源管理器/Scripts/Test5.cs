using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region 加载单个资源
        if (Input.GetKeyDown(KeyCode.Q))
        {
           GameObject go= Resources.Load("Prefabs/Capsule") as GameObject;
            Instantiate(go);
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            GameObject go=Resources.Load("Prefabs/Capsule",typeof(GameObject)) as GameObject;//重载，根据反射的类型加载（为了防止同名但是不同类型的资源），但是这里会频繁装箱拆箱消耗性能
            Instantiate(go);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject go = Resources.Load<GameObject>("Prefabs/Capsule");//使用泛型，但是有个确定就是不能热更新，因为lua中不支持泛型
            Instantiate(go);
        }
        #endregion

        #region 加载多个资源
        if(Input.GetKeyDown(KeyCode.A))
        {
            Object[] gos = Resources.LoadAll("Prefabs");//重载和上方一样
            for(int i = 0; i < gos.Length; i++) 
            {
                Instantiate(gos[i] as GameObject);
            }
        }
        #endregion

        #region 异步加载
        if (Input.GetKeyDown(KeyCode.Z))//异步加载异步都需要配合协程使用
        {
            StartCoroutine(LoadAsyncCoroutine());
            
        }
        if(Input.GetKeyDown(KeyCode.X))//使用资源管理器加载
        {
            ResourcesManager.Instance.LoadAsnc<GameObject>("Prefabs/Capsule", TestFunction);
        }
        #endregion
    }
    IEnumerator LoadAsyncCoroutine()
    {
        //开始异步加载
       ResourceRequest resourceRequest=  Resources.LoadAsync<GameObject>("Prefabs/Capsule");
        //等待资源加载完毕
       yield return resourceRequest;//这里就可以执行主线程后续的逻辑等，不必完全等它执行完
        //资源加载完毕后执行的逻辑
        Instantiate(resourceRequest.asset);//实例化成功异步加载的资源
    }
    public void TestFunction(GameObject gameObject)//这个函数是使用加载的资源的函数
    {
        Instantiate(gameObject);
        Debug.Log("这是加载资源后的回调函数，成功实例化了加载的资源");
    }
}
