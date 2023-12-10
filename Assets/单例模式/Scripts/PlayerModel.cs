using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 不继承成MonoBehavior的单例模式
/// 玩家数据类
/// 这里统一默认为懒汉单例模式
/// </summary>

public class PlayerModel 
{
    //构造方法私有化，防止外部new对象
    private PlayerModel() { }


    //这里用属性提供给外部访问，属性的get方法是是获取到单例对象，这里出来属性还可以用public函数来写
    private static PlayerModel instance;
    public static PlayerModel Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PlayerModel();
            }
            return instance;
        }
    }

    public int money=666;//金钱
    public int diamond = 888;//砖石
}
