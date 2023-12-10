using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 继承了MonoBehavior的单例模式基类
/// 作用:继承了该类的类就自带单例模式和MonoBehavior
/// </summary>
public class SingletonPatternMonoBase<T>: MonoBehaviour where T : MonoBehaviour//泛型约束了T类需要是MonoBehaviour或者其子类
{
    //构造方法私有化，防止外部new对象
    protected SingletonPatternMonoBase() { }

    //记录单例对象是否存在，用于防止在OnDestroy方法中访问单例对象报错
    public static bool IsExisted { get; private set; } = false;

    //提供一个静态属性返回私有静态对象实例给外部访问
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject go;
                instance = FindObjectOfType<T>();
                //自动单例模式
                if (instance == null )
                {
                    go = new GameObject(typeof(T).Name);//创建游戏对象,这里是通过反射来获取T类型的名字
                    instance = go.AddComponent<T>();//添加脚本给游戏对象，该函数的返回值就是该游戏脚本    
                }
                else
                {
                    go = instance.GetComponent<GameObject>();
                }
                IsExisted=true;
                DontDestroyOnLoad(go);//让游戏对象切换场景不销毁
            }
            return instance;
        }
    }

    protected virtual void OnDestroy()
    {
        IsExisted = false;
    }
}
