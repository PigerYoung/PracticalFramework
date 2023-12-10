using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager :SingletonPatternMonoBase<UIManager>
{
    private UIManager() { }//���캯��˽�л�
    private Transform canvasTransform;//��ȡ����
    private Dictionary<UIPanelType, string> panelPathDic;//���·���ֵ䣬���ÿ������Ӧ��·��
    private Dictionary<UIPanelType, BasePanel> panelDict;//���ű��ֵ䣨����ת������ø����ţ�,��ȡ�����Ľű��Ϳ��Ե�������ķ���
    private Stack<BasePanel> panelStack;//�������ջ
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
    private void CreatOverlayCanvas()//����OverlayCanvas�Ļ���
    {
        //�޸�UIManager��layer�㣬��Ϊ�������lyaer�����Ӱ�������壨��������岻��UI�㣬�����������ǣ��������UI���ǲ���Ч�ģ�
        gameObject.layer = LayerMask.NameToLayer("UI");

        //���Canvas��Enentsystem
        GameObject CanvasObj = new GameObject("Canvas");
        CanvasObj.transform.SetParent(this.transform, false);
        Canvas canvas= CanvasObj.AddComponent<Canvas>();//���Canvas���
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler canvasscaler = CanvasObj.AddComponent<CanvasScaler>();//���CanvasScaler���
        canvasscaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasscaler.referenceResolution = new Vector2(Screen.width,Screen.height);
        canvasscaler.matchWidthOrHeight = Screen.width > Screen.height ? 0 : 1;
        GraphicRaycaster canvasgraphicRaycaster = CanvasObj.AddComponent<GraphicRaycaster>();//���GraphicRaycaster���

        GameObject EventSystemObj= new GameObject("EventSystem");
        EventSystemObj.transform.SetParent(this.transform, false);
        EventSystem eventSystem = EventSystemObj.AddComponent<EventSystem>();
        StandaloneInputModule standaloneInputModule = EventSystemObj.AddComponent<StandaloneInputModule>();
    }

    private void ParseUIPanelTypeJson()//������������·����json�ļ�������������·�����ֵ���
    {
        panelPathDic = new Dictionary<UIPanelType, string>();
        TextAsset textAsset = ResourcesManager.Instance.Load<TextAsset>("UIPanelType");//�����õ��˼�����Դ��������ͬ�������Ǻ���ͨ����Դ�������𲻴�

        List<UIPanelInfo> uiPanelInfos = JsonMapper.ToObject<List<UIPanelInfo>>(textAsset.text);
        foreach (UIPanelInfo info in uiPanelInfos)
        {
            panelPathDic.Add(info.UIPanelType, info.Path);
        }
    }

    private BasePanel GetPanel(UIPanelType uiPanelType)//����������ȡ���
    {
        if(panelDict==null)
        {
            panelDict=new Dictionary<UIPanelType, BasePanel>();
        }
        BasePanel panel; 
        panelDict.TryGetValue(uiPanelType, out panel);//����������ֵ����չ����
        
        if(panel == null)
        {
            string path;
            panelPathDic.TryGetValue(uiPanelType, out path);
            if(path == null)
            {
                throw new Exception($"û�ж�Ӧ���¼���{uiPanelType}");
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
    /// �����ջ����ҳ����ʾ�ڽ�����
    /// </summary>
    /// <param name="uiPanelType">ҳ����</param>
    public void PushPanel(UIPanelType uiPanelType)
    {
        if(panelDict==null)
        {
            panelStack = new Stack<BasePanel>();
        }
        //�ж�ջ�����Ƿ���ҳ��
        if(panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();//��ΪҪ���µ�ջ����˾ͽ�ջ��Ԫ����ͣ
        }
        BasePanel panel=GetPanel(uiPanelType);//����������ȡ���
        panelStack.Push(panel);
        panel.OnEnter();//����ջ��ҳ�����OnEnter����
    }

    /// <summary>
    /// ҳ���ջ����ջ����ҳ���ջ���Ƴ�
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
        if(panelStack.Count > 0 )//�����ջ��ҳ���ջ����ҳ�棬����Ҫ�ָ���ҳ��
        {
            BasePanel topPanel2=panelStack.Peek();
            topPanel2.OnResume();
        }
    }
}
