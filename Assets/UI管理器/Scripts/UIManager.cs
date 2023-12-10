using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager :SingletonPatternMonoBase<UIManager>
{
    private UIManager() { }//构造函数私有化
    private Transform canvasTransform;//获取画布
    private Dictionary<UIPanelType, string> panelPathDic;//面板路径字典，存放每个面板对应的路径
    private Dictionary<UIPanelType, BasePanel> panelDict;//面板脚本字典（里氏转换因此用父类存放）,获取到面板的脚本就可以调用里面的方法
    private Stack<BasePanel> panelStack;//存放面板的栈
    private void Awake()
    {
        ParseUIPanelTypeJson();
    }

    public Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                CreatOverlayCanvas();
                canvasTransform = transform.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    private void CreatOverlayCanvas()//创建OverlayCanvas的画布
    {
        //修改UIManager的layer层，因为父物体的lyaer层可以影响子物体（如果父物体不是UI层，但是子物体是，子物体的UI层是不生效的）
        gameObject.layer = LayerMask.NameToLayer("UI");

        //添加Canvas和Enentsystem
        GameObject CanvasObj = new GameObject("Canvas");
        CanvasObj.transform.SetParent(this.transform, false);
        Canvas canvas= CanvasObj.AddComponent<Canvas>();//添加Canvas组件
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler canvasscaler = CanvasObj.AddComponent<CanvasScaler>();//添加CanvasScaler组件
        canvasscaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasscaler.referenceResolution = new Vector2(Screen.width,Screen.height);
        canvasscaler.matchWidthOrHeight = Screen.width > Screen.height ? 0 : 1;
        GraphicRaycaster canvasgraphicRaycaster = CanvasObj.AddComponent<GraphicRaycaster>();//添加GraphicRaycaster组件

        GameObject EventSystemObj= new GameObject("EventSystem");
        EventSystemObj.transform.SetParent(this.transform, false);
        EventSystem eventSystem = EventSystemObj.AddComponent<EventSystem>();
        StandaloneInputModule standaloneInputModule = EventSystemObj.AddComponent<StandaloneInputModule>();
    }

    private void ParseUIPanelTypeJson()//解析存放了面板路径的json文件，并存放在面板路径的字典中
    {
        panelPathDic = new Dictionary<UIPanelType, string>();
        TextAsset textAsset = ResourcesManager.Instance.Load<TextAsset>("UIPanelType");//这里用到了加载资源管理器（同步加载是和普通的资源加载区别不大）

        List<UIPanelInfo> uiPanelInfos = JsonMapper.ToObject<List<UIPanelInfo>>(textAsset.text);
        foreach (UIPanelInfo info in uiPanelInfos)
        {
            panelPathDic.Add(info.UIPanelType, info.Path);
        }
    }

    private BasePanel GetPanel(UIPanelType uiPanelType)//根据面板码获取面板
    {
        if(panelDict==null)
        {
            panelDict=new Dictionary<UIPanelType, BasePanel>();
        }
        BasePanel panel; 
        panelDict.TryGetValue(uiPanelType, out panel);//这里可以用字典的扩展方法
        
        if(panel == null)
        {
            string path;
            panelPathDic.TryGetValue(uiPanelType, out path);
            if(path == null)
            {
                throw new Exception($"没有对应的事件码{uiPanelType}");
            }
            GameObject instPanel = GameObject.Instantiate(ResourcesManager.Instance.Load<GameObject>(path),CanvasTransform,false);
            panelDict.Add(uiPanelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent <BasePanel>();
        }
        else
        {
            return panel;
        }      
    }

    /// <summary>
    /// 面板入栈，把页面显示在界面上
    /// </summary>
    /// <param name="uiPanelType">页面码</param>
    public void PushPanel(UIPanelType uiPanelType)
    {
        if(panelDict==null)
        {
            panelStack = new Stack<BasePanel>();
        }
        //判断栈里面是否有页面
        if(panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();//因为要入新的栈，因此就将栈顶元素暂停
        }
        BasePanel panel=GetPanel(uiPanelType);//根据面板码获取面板
        panelStack.Push(panel);
        panel.OnEnter();//新入栈的页面调用OnEnter方法
    }

    /// <summary>
    /// 页面出栈，把栈顶的页面出栈，移除
    /// </summary>
    public void PopPanel()
    {
        if(panelStack==null)
        {
            panelStack= new Stack<BasePanel>();
        }
        if( panelStack.Count <= 0 )
        {
            return;
        }
        BasePanel topPanel= panelStack.Pop();
        topPanel.OnExit();
        if(panelStack.Count > 0 )//如果把栈顶页面出栈后还有页面，就需要恢复该页面
        {
            BasePanel topPanel2=panelStack.Peek();
            topPanel2.OnResume();
        }
    }
}
