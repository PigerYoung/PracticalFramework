using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̳г�MonoBehavior�ĵ���ģʽ
/// ���������
/// ����ͳһĬ��Ϊ��������ģʽ
/// </summary>

public class PlayerModel 
{
    //���췽��˽�л�����ֹ�ⲿnew����
    private PlayerModel() { }


    //�����������ṩ���ⲿ���ʣ����Ե�get�������ǻ�ȡ��������������������Ի�������public������д
    private static PlayerModel instance;
    public static PlayerModel Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PlayerModel();
            }
            return instance;
        }
    }

    public int money=666;//��Ǯ
    public int diamond = 888;//שʯ
}
