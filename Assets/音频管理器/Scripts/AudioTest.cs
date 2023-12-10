using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    AudioClip BGM_1;
    AudioClip BGM_2;
    AudioClip sound;
    public GameObject TARGET;

    private void Awake()
    {
        BGM_1 = Resources.Load<AudioClip>("Audio/BGM/BGM_1");
        BGM_2= Resources.Load<AudioClip>("Audio/BGM/BGM_2");
        sound= Resources.Load<AudioClip>("Audio/Sound/Sound_2");
    }
    void Start()
    {
        EventCenterManager.Instance.AddListener<KeyCode>(EventDefine.GetkeyDown, PlayAudio);
        InputKeysManager.Instance.SetActive(true);
    }

    public void PlayAudio(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.W:
                Debug.Log("���ű�������1");
                AudioManager.Instance.PlayBGM(BGM_1);
                break;
            case KeyCode.E:
                Debug.Log("���ű�������2");
                AudioManager.Instance.PlayBGM(BGM_2);
                break;
            case KeyCode.R:
                Debug.Log("��ͣBGM");
                AudioManager.Instance.PauseBGM();
                break;
            case KeyCode.T:
                Debug.Log("�ָ�BGM");
                AudioManager.Instance.ResumeBGM();
                break;
            case KeyCode.Y:
                Debug.Log("ֹͣBGM");
                AudioManager.Instance.StopBGM();
                break;
            case KeyCode.U:
                Debug.Log("������Ч");
                AudioManager.Instance.PlaySound(sound);
                break;
            case KeyCode.I:
                Debug.Log("����3D��Ч");
                AudioManager.Instance.PlaySound(sound, TARGET);
                break;
        }
    }

    private void OnDestroy()
    {
        EventCenterManager.Instance.RemoveListener<KeyCode>(EventDefine.GetkeyDown, PlayAudio);
    }
}
