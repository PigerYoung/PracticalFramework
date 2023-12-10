using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region ���ص�����Դ
        if (Input.GetKeyDown(KeyCode.Q))
        {
           GameObject go= Resources.Load("Prefabs/Capsule") as GameObject;
            Instantiate(go);
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            GameObject go=Resources.Load("Prefabs/Capsule",typeof(GameObject)) as GameObject;//���أ����ݷ�������ͼ��أ�Ϊ�˷�ֹͬ�����ǲ�ͬ���͵���Դ�������������Ƶ��װ�������������
            Instantiate(go);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject go = Resources.Load<GameObject>("Prefabs/Capsule");//ʹ�÷��ͣ������и�ȷ�����ǲ����ȸ��£���Ϊlua�в�֧�ַ���
            Instantiate(go);
        }
        #endregion

        #region ���ض����Դ
        if(Input.GetKeyDown(KeyCode.A))
        {
            Object[] gos = Resources.LoadAll("Prefabs");//���غ��Ϸ�һ��
            for(int i = 0; i < gos.Length; i++) 
            {
                Instantiate(gos[i] as GameObject);
            }
        }
        #endregion

        #region �첽����
        if (Input.GetKeyDown(KeyCode.Z))//�첽�����첽����Ҫ���Э��ʹ��
        {
            StartCoroutine(LoadAsyncCoroutine());
            
        }
        if(Input.GetKeyDown(KeyCode.X))//ʹ����Դ����������
        {
            ResourcesManager.Instance.LoadAsnc<GameObject>("Prefabs/Capsule", TestFunction);
        }
        #endregion
    }
    IEnumerator LoadAsyncCoroutine()
    {
        //��ʼ�첽����
       ResourceRequest resourceRequest=  Resources.LoadAsync<GameObject>("Prefabs/Capsule");
        //�ȴ���Դ�������
       yield return resourceRequest;//����Ϳ���ִ�����̺߳������߼��ȣ�������ȫ����ִ����
        //��Դ������Ϻ�ִ�е��߼�
        Instantiate(resourceRequest.asset);//ʵ�����ɹ��첽���ص���Դ
    }
    public void TestFunction(GameObject gameObject)//���������ʹ�ü��ص���Դ�ĺ���
    {
        Instantiate(gameObject);
        Debug.Log("���Ǽ�����Դ��Ļص��������ɹ�ʵ�����˼��ص���Դ");
    }
}
