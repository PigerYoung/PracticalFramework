using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̳�MonoBehaviour�ĵ���ģʽ�Ļ���
/// ����:�̳��˸��������Դ�����ģʽ
/// 
/// ������new()�ķ���Լ�������һ���bug��new()�ķ���Լ������˼������T�й������캯������Ϊ����Ҫ�ø�ʵ������ֵ�õ�new T():�����ѡ�õ���
/// ����T�Ĺ��캯�����������Ӹķ���Լ���Ļ��Ͳ���ʹ��T�Ĺ��캯���������������д�Ļ��ͱ���������T�����й����Ĺ��캯��
/// �����Ļ��ͻ�������ⲿ������new�ķ�ʽnew���ܶ�T����������Υ���˵���ģʽ�Ĺ���˽�л��������������ǹ���˽�л��Ļ�������new()�ķ���Լ��
/// ������÷���ķ�ʽ���������󣬱�����new��������
/// </summary>
public class SingletonPatternBase<T> where T: SingletonPatternBase<T>//,new()//����Լ����������ͱ���Ϊ���౾�����������,�������new��Ϊ�˲���new T()������ΪT��һ�����޲ι��캯������Ҫ�÷���Լ��ָ��
{
   protected SingletonPatternBase(){ }//��ΪҪ���캯��˽�л���������Ҫ����������Լ̳и��࣬������õ���protected��ֹ�ⲿSingletonPatternBase<T>����

   //�߳����������̷߳���ʱ��ͬһʱ��ֻ����һ���̷߳���
   private static object locker=new object();

    //volatile�ؼ��������ֶΣ�������̶߳����������޸�ʱ������ȷ������ֶ����κ�ʱ�̳��ֵĶ�������ֵ
    private volatile static T instance;
   
   public static T Instance
   {
        get
        {
            if(instance == null)//�������Ϊ�յĻ��ͼ����߳���
            {
                lock (locker)//�����߳����󣬷�ֹ���߳�ʱ�ظ�����
                {
                    if (instance == null)
                    {
                        //instance=new T();
                        instance = Activator.CreateInstance(typeof(T),true) as T;//�����õ��˷��䴴����һ������
                    }                    
                }
            }
            return instance;
        }
   }
}
