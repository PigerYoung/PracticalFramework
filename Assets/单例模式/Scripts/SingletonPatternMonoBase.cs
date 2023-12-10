using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̳���MonoBehavior�ĵ���ģʽ����
/// ����:�̳��˸��������Դ�����ģʽ��MonoBehavior
/// </summary>
public class SingletonPatternMonoBase<T>: MonoBehaviour where T : MonoBehaviour//����Լ����T����Ҫ��MonoBehaviour����������
{
    //���췽��˽�л�����ֹ�ⲿnew����
    protected SingletonPatternMonoBase() { }

    //��¼���������Ƿ���ڣ����ڷ�ֹ��OnDestroy�����з��ʵ������󱨴�
    public static bool IsExisted { get; private set; } = false;

    //�ṩһ����̬���Է���˽�о�̬����ʵ�����ⲿ����
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject go;
                instance = FindObjectOfType<T>();
                //�Զ�����ģʽ
                if (instance == null )
                {
                    go = new GameObject(typeof(T).Name);//������Ϸ����,������ͨ����������ȡT���͵�����
                    instance = go.AddComponent<T>();//��ӽű�����Ϸ���󣬸ú����ķ���ֵ���Ǹ���Ϸ�ű�    
                }
                else
                {
                    go = instance.GetComponent<GameObject>();
                }
                IsExisted=true;
                DontDestroyOnLoad(go);//����Ϸ�����л�����������
            }
            return instance;
        }
    }

    protected virtual void OnDestroy()
    {
        IsExisted = false;
    }
}
