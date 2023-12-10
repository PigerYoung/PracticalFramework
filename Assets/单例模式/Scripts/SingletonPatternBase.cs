using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 不继承MonoBehaviour的单例模式的基类
/// 作用:继承了该类的类就自带单例模式
/// 
/// 这里用new()的泛型约束会出现一点儿bug，new()的泛型约束的意思是类型T有公共构造函数，因为这里要用给实例对象赋值用到new T():这个就选用到了
/// 泛型T的构造函数，如果不添加改泛型约束的话就不能使用T的构造函数。但是如果这样写的话就表明了类型T必须有公共的构造函数
/// 这样的话就会造成在外部可以用new的方式new出很多T对象这样是违背了单例模式的构造私有化，因此如果不考虑构造私有化的话可以用new()的泛型约束
/// 这里就用反射的方式来创建对象，避免用new创建对象
/// </summary>
public class SingletonPatternBase<T> where T: SingletonPatternBase<T>//,new()//泛型约束，这个泛型必须为该类本身或者其子类,这里添加new是为了不让new T()报错，因为T不一定有无参构造函数，需要用泛型约束指定
{
   protected SingletonPatternBase(){ }//因为要构造函数私有化，但是又要让其它类可以继承该类，这里就用到了protected防止外部SingletonPatternBase<T>对象

   //线程锁。当多线程访问时，同一时刻只允许一个线程访问
   private static object locker=new object();

    //volatile关键字修饰字段，当多个线程都对它进行修改时，可以确保这个字段在任何时刻呈现的都是最新值
    private volatile static T instance;
   
   public static T Instance
   {
        get
        {
            if(instance == null)//如果对象为空的话就加上线程锁
            {
                lock (locker)//加上线程锁后，防止多线程时重复创建
                {
                    if (instance == null)
                    {
                        //instance=new T();
                        instance = Activator.CreateInstance(typeof(T),true) as T;//这里用到了反射创建了一个对象
                    }                    
                }
            }
            return instance;
        }
   }
}
