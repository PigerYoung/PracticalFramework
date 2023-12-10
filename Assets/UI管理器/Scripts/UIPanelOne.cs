using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelOne : BasePanel//���һ�������������ں����о���Ҫʵ�ֵ�����
{
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        if(canvasGroup==null) canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void OnEnter()//�������һ�ľ���ʵ��
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();//�����Ƿ�ֹ�����ʵ�������������������OnEnter()���������� canvasGroup��û��ֵ������
        
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        Debug.Log("���ǽ��������һ��OnEnter����");
    }
    public override void OnExit()//�ر����һ�ľ���ʵ��
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        Debug.Log("�����˳����һ��OnExit����");
    }
    public override void OnPause()//��ͣ���һ�ľ���ʵ��
    {
        canvasGroup.blocksRaycasts = false;
        Debug.Log("������ͣ���һ��OnPause����");
    }
    public override void OnResume()//�ָ����һ����ʵ��
    {
        canvasGroup.blocksRaycasts = true;
        Debug.Log("���ǻָ����һ��OnResume����");
    }

}
