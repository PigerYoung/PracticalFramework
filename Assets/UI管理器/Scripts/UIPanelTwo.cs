using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelTwo : BasePanel//面板一在面板的声明周期函数中具体要实现的内容
{
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        if(canvasGroup==null) canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void OnEnter()//进入面板二的具体实现
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();//这里是防止面板在实例化出来后，立马调用了OnEnter()方法，导致 canvasGroup还没赋值而报空
        
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        Debug.Log("这是进入了面板二的OnEnter方法");
    }
    public override void OnExit()//关闭面板二的具体实现
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        Debug.Log("这是退出面板二的OnExit方法");
    }
    public override void OnPause()//暂停面板二的具体实现
    {
        canvasGroup.blocksRaycasts = false;
        Debug.Log("这是暂停面板二的OnPause方法");
    }
    public override void OnResume()//恢复面板二具体实现
    {
        canvasGroup.blocksRaycasts = true;
        Debug.Log("这是恢复面板二的OnResume方法");
    }

}
