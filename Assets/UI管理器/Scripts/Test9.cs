using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test9 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InputKeysManager.Instance.SetActive(true);
        EventCenterManager.Instance.AddListener<KeyCode>(EventDefine.GetkeyDown,test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void test(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.W:
                UIManager.Instance.PushPanel(UIPanelType.UIPanelOne);
                break;
            case KeyCode.E:
                UIManager.Instance.PushPanel(UIPanelType.UIPanelTwo);
                break;
            case KeyCode.R:
                UIManager.Instance.PushPanel(UIPanelType.UIPanelThree);
                break;
        }
    }
    private void OnDestroy()
    {
        EventCenterManager.Instance.RemoveListener<KeyCode>(EventDefine.GetkeyDown, test);
    }
}
