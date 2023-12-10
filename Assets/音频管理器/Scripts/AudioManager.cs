using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonPatternMonoBase<AudioManager>
{
    //各个声道的AudioSource,因为音效可能会存在很多同时播放的情况，如果只用一个AudioSource播放音效的话肯定不够，因此就可以在soundController音效对象下每播放一条音效就创建一个对象，再添加AudioSource，播放完就销毁(可以用对象池)
    private AudioSource bgmAudioSource;
    private AudioSource bgsAudioSource;
    private AudioSource voiceAudioSource;

    private GameObject bgmController;//背景音乐对象
    private GameObject soundController;//音效
    private GameObject bgsController;//环境
    private GameObject msController;//提示音效
    private GameObject voiceController;//人声

    //2D，3D音效对象池
    private GameObject sound2DPool;
    private GameObject sound3DPool;
    private GameObject ms2DPool;
    private GameObject ms3DPool;

    private AudioManager() { }

    private void Awake()
    {
        Init();
    }

    private GameObject CreateController(string name,Transform parent)//创建不同音频的对象
    {
        GameObject go=new GameObject(name);
        go.transform.SetParent(parent);
        return go;
    }
    private void Init()//初始化音频管理器脚本
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
    private void InitObjectAtPool(string name, Transform parent, bool Is3D = false, int count = 5)//在指定中对象池创建对象
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

    public GameObject FindObject(GameObject pool)//从对象池中找可以使用的对象
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

    #region 背景音乐
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="bgm">背景音乐的clip</param>
    /// <param name="loop">是否循环播放</param>
    public void PlayBGM(AudioClip bgm,bool loop=true)
    {
        if (bgm == null)
        {
            Debug.LogWarning("播放BGM失败，要播放的BGM为空");
            return;
        }
        bgmAudioSource.clip = bgm;
        bgmAudioSource.loop = loop;
        bgmAudioSource.Play();
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBGM()
    {
        bgmAudioSource.Pause();
    }

    /// <summary>
    /// 背景音乐暂停时恢复播放
    /// </summary>
    public void ResumeBGM()
    {
        bgmAudioSource.UnPause();
    }

    /// <summary>
    /// 停止背景音乐(停止之后不能恢复，只能重新播放)
    /// </summary>
    public void StopBGM()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip=null;
    }
    #endregion

    #region 音效
    /// <summary>
    /// 播放指定2D音效
    /// </summary>
    /// <param name="sound">2D音效</param>
    public void PlaySound(AudioClip sound)
    {
        if (sound == null)
        {
            Debug.LogWarning("播放sound失败，要播放的sound为空");
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
            }while(audioSource!=null&&audioSource.time!=0);//time等于0就是播放完
            if(go!=null)
            {
                audioSource.clip= null;
                go.SetActive(false);          
            }
        }
    }
   

    /// <summary>
    /// 播放3D音效
    /// </summary>
    /// <param name="sound">3D音效</param>
    /// <param name="target">播放3D音效的目标</param>
    public void PlaySound(AudioClip sound,GameObject target)
    {
        if (sound == null)
        {
            Debug.LogWarning("播放sound失败，要播放的sound为空");
            return;
        }
        if (target == null)
        {
            Debug.LogWarning("目标为空");
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
            } while (audioSource != null && audioSource.time != 0);//time等于0就是播放完
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

    #region 环境音乐
    /// <summary>
    /// 播放环境音乐
    /// </summary>
    /// <param name="bgm">环境音乐的clip</param>
    /// <param name="loop">是否循环播放</param>
    public void PlayBGS(AudioClip bgs, bool loop = true)
    {
        if (bgsAudioSource == null)
        {
            Debug.LogWarning("播放环境音乐失败，要播放的环境音乐为空");
            return;
        }
        bgsAudioSource.clip = bgs;
        bgsAudioSource.loop = loop;
        bgsAudioSource.Play();
    }

    /// <summary>
    /// 暂停环境音乐
    /// </summary>
    public void PauseBGS()
    {
        bgsAudioSource.Pause();
    }

    /// <summary>
    /// 环境暂停时恢复播放
    /// </summary>
    public void ResumeBGS()
    {
        bgsAudioSource.UnPause();
    }

    /// <summary>
    /// 停止环境音乐(停止之后不能恢复，只能重新播放)
    /// </summary>
    public void StopBGS()
    {
        bgsAudioSource.Stop();
        bgsAudioSource.clip = null;
    }
    #endregion

    #region 提示音效
    /// <summary>
    /// 播放指定2D提示音效
    /// </summary>
    /// <param name="sound">2D音效</param>
    public void PlayMS(AudioClip ms)
    {
        if (ms == null)
        {
            Debug.LogWarning("播放ms失败，要播放的ms为空");
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
            } while (audioSource != null && audioSource.time != 0);//time等于0就是播放完
            if (go != null)
            {
                audioSource.clip = null;
                go.SetActive(false);
            }
        }
    }
   

    /// <summary>
    /// 播放3D提示音效
    /// </summary>
    /// <param name="sound">3D音效</param>
    /// <param name="target">播放3D音效的目标</param>
    public void PlayMS(AudioClip ms, GameObject target)
    {
        if (ms == null)
        {
            Debug.LogWarning("播放ms失败，要播放的ms为空");
            return;
        }
        if(target == null)
        {
            Debug.LogWarning("目标为空");
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
            } while (audioSource != null && audioSource.time != 0);//time等于0就是播放完
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

    #region 角色语音

    /// <summary>
    /// 播放角色语音
    /// </summary>
    /// <param name="voice">要播放的clip</param>
    public void PlayVoice(AudioClip voice)
    {
        voiceAudioSource.clip = voice;
        voiceAudioSource.Play();
    }
    /// <summary>
    /// 停止角色语音
    /// </summary>
    public void Stop()
    {
        voiceAudioSource.Stop();
    }
    #endregion

}
