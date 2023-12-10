using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPatternChild :SingletonPatternBase<SingletonPatternChild>
{
    private SingletonPatternChild() { }//构造函数私有化
    public int money = 666;//金钱
    public int diamond = 888;//砖石
}
