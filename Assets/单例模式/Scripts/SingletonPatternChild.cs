using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPatternChild :SingletonPatternBase<SingletonPatternChild>
{
    private SingletonPatternChild() { }//���캯��˽�л�
    public int money = 666;//��Ǯ
    public int diamond = 888;//שʯ
}
