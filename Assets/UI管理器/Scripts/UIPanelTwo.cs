using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelTwo : BasePanel//���һ�������������ں����о���Ҫʵ�ֵ�����
{
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        if(canvasGroup==null) canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void OnEnter()//���������ľ���ʵ��
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();//�����Ƿ�ֹ�����ʵ�������������������OnEnter()���������� canvasGroup��û��ֵ������
        
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        Debug.Log("���ǽ�����������OnEnter����");
    }
    public override void OnExit()//�ر������ľ���ʵ��
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        Debug.Log("�����˳�������OnExit����");
    }
    public override void OnPause()//��ͣ�����ľ���ʵ��
    {
        canvasGroup.blocksRaycasts = false;
        Debug.Log("������ͣ������OnPause����");
    }
    public override void OnResume()//�ָ���������ʵ��
    {
        canvasGroup.blocksRaycasts = true;
        Debug.Log("���ǻָ�������OnResume����");
    }

}
