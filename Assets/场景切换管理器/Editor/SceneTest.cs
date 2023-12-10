using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �༭����չ
/// </summary>
public class SceneTest 
{
   [MenuItem("�ҵĲ˵�/ͬ���л�����/�����л�����ǰ����")]
   static void Method1()
   {
       LoadSceneManager.Instance.LoadActiveScene();
    }
    [MenuItem("�ҵĲ˵�/ͬ���л�����/�л�����һ������")]
    static void Method2()
    {
        LoadSceneManager.Instance.LoadPreviousScene(true);
    }
    [MenuItem("�ҵĲ˵�/ͬ���л�����/�л�����һ������")]
    static void Method3()
    {
        LoadSceneManager.Instance.LoadNextScene(true);
    }

    [MenuItem("�ҵĲ˵�/�첽�л�����/�л�������1")]
    static void Method4()
    {
        LoadSceneManager.Instance.LoadSceneAsync("scene1", (obj) => { Debug.Log("���ؽ���" + obj * 100 + "%");}, (obj) => { Debug.Log("��������ˣ�"); });
    }
    [MenuItem("�ҵĲ˵�/�첽�л�����/�л�������2")]
    static void Method5()
    {
        LoadSceneManager.Instance.LoadSceneAsync("scene2", (obj) => { Debug.Log("���ؽ���" + obj * 100 + "%"); }, (obj) => { Debug.Log("��������ˣ�"); });
    }
    [MenuItem("�ҵĲ˵�/�첽�л�����/�л�������3")]
    static void Method6()
    {
        LoadSceneManager.Instance.LoadSceneAsync("scene3", (obj) => { Debug.Log("���ؽ���" + obj * 100 + "%"); }, (obj) => { Debug.Log("��������ˣ�"); });
    }
}

