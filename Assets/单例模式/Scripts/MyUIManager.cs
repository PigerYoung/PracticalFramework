using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ҵ�ui���������̳�MonoBehavior�ĵ���ģʽ
/// </summary>
public class MyUIManager : MonoBehaviour
{
    //���췽��˽�л�����ֹ�ⲿnew����
    private MyUIManager() { }

    //�ṩһ����̬���ԣ����߷���������˽�о�̬����ʵ�����ⲿ����
    private static MyUIManager instance;
    public static MyUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go;
                instance = FindObjectOfType<MyUIManager>();             
                //�Զ�����ģʽ
                if (instance == null)
                {
                    go = new GameObject("MyUIManager");//������Ϸ����
                    instance= go.AddComponent<MyUIManager>();//��ӽű�����Ϸ���󣬸ú����ķ���ֵ���Ǹ���Ϸ�ű�                
                }
                else
                {
                    go = instance.GetComponent<GameObject>();
                }
                DontDestroyOnLoad(go);//����Ϸ�����л�����������
            }         
            return instance;
        }
    }

    public void Show()
    {
        Debug.Log("��ʾ���");
    }
    public void Hide()
    {
        Debug.Log("�������");
    }
}
