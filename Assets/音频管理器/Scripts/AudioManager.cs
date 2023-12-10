using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonPatternMonoBase<AudioManager>
{
    //����������AudioSource,��Ϊ��Ч���ܻ���ںܶ�ͬʱ���ŵ���������ֻ��һ��AudioSource������Ч�Ļ��϶���������˾Ϳ�����soundController��Ч������ÿ����һ����Ч�ʹ���һ�����������AudioSource�������������(�����ö����)
    private AudioSource bgmAudioSource;
    private AudioSource bgsAudioSource;
    private AudioSource voiceAudioSource;

    private GameObject bgmController;//�������ֶ���
    private GameObject soundController;//��Ч
    private GameObject bgsController;//����
    private GameObject msController;//��ʾ��Ч
    private GameObject voiceController;//����

    //2D��3D��Ч�����
    private GameObject sound2DPool;
    private GameObject sound3DPool;
    private GameObject ms2DPool;
    private GameObject ms3DPool;

    private AudioManager() { }

    private void Awake()
    {
        Init();
    }

    private GameObject CreateController(string name,Transform parent)//������ͬ��Ƶ�Ķ���
    {
        GameObject go=new GameObject(name);
        go.transform.SetParent(parent);
        return go;
    }
    private void Init()//��ʼ����Ƶ�������ű�
    {
        bgmController = CreateController("BgmController", transform);
        bgmAudioSource = bgmController.AddComponent<AudioSource>();
        bgmAudioSource.playOnAwake = false;
        bgmAudioSource.loop = true;

        soundController = CreateController("SoundController", transform);
        sound2DPool = CreateController("Sound2DPool", soundController.transform);
        sound3DPool = CreateController("Sound3DPool", soundController.transform);

        bgsController = CreateController("BgsController", transform);
        bgsAudioSource = bgsController.AddComponent<AudioSource>();
        bgsAudioSource.playOnAwake = false;
        bgsAudioSource.loop = true;

        msController = CreateController("MsController", transform);
        ms2DPool = CreateController("Ms2DPool", msController.transform);
        ms3DPool = CreateController("Ms3DPool", msController.transform);

        voiceController = CreateController("VoiceController", transform);
        voiceAudioSource = voiceController.AddComponent<AudioSource>();
        voiceAudioSource.playOnAwake = false;
        voiceAudioSource.loop = false;

        InitObjectAtPool("2DSound", sound2DPool.transform);
        InitObjectAtPool("3DSound", sound3DPool.transform, true);
        InitObjectAtPool("2DMs", ms2DPool.transform);
        InitObjectAtPool("3DMs", ms3DPool.transform, true);
    }
    private void InitObjectAtPool(string name, Transform parent, bool Is3D = false, int count = 5)//��ָ���ж���ش�������
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent);
            AudioSource audioSource = go.AddComponent<AudioSource>();
            if (Is3D)
            {
                audioSource.spatialBlend = 1.0f;
            }
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            go.SetActive(false);
        }
    }

    public GameObject FindObject(GameObject pool)//�Ӷ�������ҿ���ʹ�õĶ���
    {
        foreach (Transform go in pool.transform)
        {
            GameObject item = go.gameObject;
            if (item.activeSelf == false)
            {
                item.SetActive(true);
                return item;
            }
        }
        return null;
    }

    #region ��������
    /// <summary>
    /// ���ű�������
    /// </summary>
    /// <param name="bgm">�������ֵ�clip</param>
    /// <param name="loop">�Ƿ�ѭ������</param>
    public void PlayBGM(AudioClip bgm,bool loop=true)
    {
        if (bgm == null)
        {
            Debug.LogWarning("����BGMʧ�ܣ�Ҫ���ŵ�BGMΪ��");
            return;
        }
        bgmAudioSource.clip = bgm;
        bgmAudioSource.loop = loop;
        bgmAudioSource.Play();
    }

    /// <summary>
    /// ��ͣ��������
    /// </summary>
    public void PauseBGM()
    {
        bgmAudioSource.Pause();
    }

    /// <summary>
    /// ����������ͣʱ�ָ�����
    /// </summary>
    public void ResumeBGM()
    {
        bgmAudioSource.UnPause();
    }

    /// <summary>
    /// ֹͣ��������(ֹ֮ͣ���ָܻ���ֻ�����²���)
    /// </summary>
    public void StopBGM()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip=null;
    }
    #endregion

    #region ��Ч
    /// <summary>
    /// ����ָ��2D��Ч
    /// </summary>
    /// <param name="sound">2D��Ч</param>
    public void PlaySound(AudioClip sound)
    {
        if (sound == null)
        {
            Debug.LogWarning("����soundʧ�ܣ�Ҫ���ŵ�soundΪ��");
            return;
        }

        GameObject go=null;
        AudioSource audioSource;
        go = FindObject(sound2DPool);
        if(go==null)
        {
            InitObjectAtPool("2DSound", sound2DPool.transform);
            go = FindObject(sound2DPool);
        }     
        audioSource = go.GetComponent<AudioSource>();
        audioSource.playOnAwake= false;
        audioSource.clip= sound;
        audioSource.Play();
        StartCoroutine(DestroyWhenOver());

        IEnumerator DestroyWhenOver()
        {
            do
            {
                yield return new WaitForSeconds(1);
            }while(audioSource!=null&&audioSource.time!=0);//time����0���ǲ�����
            if(go!=null)
            {
                audioSource.clip= null;
                go.SetActive(false);          
            }
        }
    }
   

    /// <summary>
    /// ����3D��Ч
    /// </summary>
    /// <param name="sound">3D��Ч</param>
    /// <param name="target">����3D��Ч��Ŀ��</param>
    public void PlaySound(AudioClip sound,GameObject target)
    {
        if (sound == null)
        {
            Debug.LogWarning("����soundʧ�ܣ�Ҫ���ŵ�soundΪ��");
            return;
        }
        if (target == null)
        {
            Debug.LogWarning("Ŀ��Ϊ��");
            return;
        }
        GameObject go = null;
        AudioSource audioSource;
        go = FindObject(sound3DPool);
        if (go == null)
        {
            InitObjectAtPool("3DSound", sound3DPool.transform,true);
            go = FindObject(sound3DPool);
        }      
        go.transform.SetParent( target.transform);
        go.transform.localPosition = Vector3.zero;
        audioSource = go.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = sound;
        audioSource.Play();
        StartCoroutine(DestroyWhenOver());

        IEnumerator DestroyWhenOver()
        {
            do
            {
                yield return new WaitForSeconds(1);
            } while (audioSource != null && audioSource.time != 0);//time����0���ǲ�����
            if (go != null)
            {
                audioSource.clip = null;
                go.transform.SetParent(sound3DPool.transform);
                go.transform.localPosition = Vector3.zero;
                go.SetActive(false);
            }
        }

    }
    #endregion

    #region ��������
    /// <summary>
    /// ���Ż�������
    /// </summary>
    /// <param name="bgm">�������ֵ�clip</param>
    /// <param name="loop">�Ƿ�ѭ������</param>
    public void PlayBGS(AudioClip bgs, bool loop = true)
    {
        if (bgsAudioSource == null)
        {
            Debug.LogWarning("���Ż�������ʧ�ܣ�Ҫ���ŵĻ�������Ϊ��");
            return;
        }
        bgsAudioSource.clip = bgs;
        bgsAudioSource.loop = loop;
        bgsAudioSource.Play();
    }

    /// <summary>
    /// ��ͣ��������
    /// </summary>
    public void PauseBGS()
    {
        bgsAudioSource.Pause();
    }

    /// <summary>
    /// ������ͣʱ�ָ�����
    /// </summary>
    public void ResumeBGS()
    {
        bgsAudioSource.UnPause();
    }

    /// <summary>
    /// ֹͣ��������(ֹ֮ͣ���ָܻ���ֻ�����²���)
    /// </summary>
    public void StopBGS()
    {
        bgsAudioSource.Stop();
        bgsAudioSource.clip = null;
    }
    #endregion

    #region ��ʾ��Ч
    /// <summary>
    /// ����ָ��2D��ʾ��Ч
    /// </summary>
    /// <param name="sound">2D��Ч</param>
    public void PlayMS(AudioClip ms)
    {
        if (ms == null)
        {
            Debug.LogWarning("����msʧ�ܣ�Ҫ���ŵ�msΪ��");
            return;
        }
        GameObject go = null;
        AudioSource audioSource;
        go = FindObject(ms2DPool);
        if (go == null)
        {
            InitObjectAtPool("2DMS", ms2DPool.transform);
            go = FindObject(ms2DPool);
        }      
        audioSource = go.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = ms;
        audioSource.Play();
        StartCoroutine(DestroyWhenOver());

        IEnumerator DestroyWhenOver()
        {
            do
            {
                yield return new WaitForSeconds(1);
            } while (audioSource != null && audioSource.time != 0);//time����0���ǲ�����
            if (go != null)
            {
                audioSource.clip = null;
                go.SetActive(false);
            }
        }
    }
   

    /// <summary>
    /// ����3D��ʾ��Ч
    /// </summary>
    /// <param name="sound">3D��Ч</param>
    /// <param name="target">����3D��Ч��Ŀ��</param>
    public void PlayMS(AudioClip ms, GameObject target)
    {
        if (ms == null)
        {
            Debug.LogWarning("����msʧ�ܣ�Ҫ���ŵ�msΪ��");
            return;
        }
        if(target == null)
        {
            Debug.LogWarning("Ŀ��Ϊ��");
            return;
        }
        GameObject go = null;
        AudioSource audioSource;
        go = FindObject(ms3DPool);
        if (go == null)
        {
            InitObjectAtPool("3DMS", ms3DPool.transform, true);
            go = FindObject(ms3DPool);
        }
        go.transform.SetParent(target.transform);
        go.transform.localPosition = Vector3.zero;
        audioSource = go.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = ms;
        audioSource.Play();
        StartCoroutine(DestroyWhenOver());

        IEnumerator DestroyWhenOver()
        {
            do
            {
                yield return new WaitForSeconds(1);
            } while (audioSource != null && audioSource.time != 0);//time����0���ǲ�����
            if (go != null)
            {
                audioSource.clip = null;
                go.transform.SetParent(ms3DPool.transform);
                go.transform.localPosition = Vector3.zero;
                go.SetActive(false);
            }
        }

    }
    #endregion

    #region ��ɫ����

    /// <summary>
    /// ���Ž�ɫ����
    /// </summary>
    /// <param name="voice">Ҫ���ŵ�clip</param>
    public void PlayVoice(AudioClip voice)
    {
        voiceAudioSource.clip = voice;
        voiceAudioSource.Play();
    }
    /// <summary>
    /// ֹͣ��ɫ����
    /// </summary>
    public void Stop()
    {
        voiceAudioSource.Stop();
    }
    #endregion

}
