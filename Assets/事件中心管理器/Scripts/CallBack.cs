/// <summary>
/// 不同参数对应的委托类型
/// 这里预设了含有五个参数的委托，一般来说已经够用了
/// </summary>
public delegate void CallBack();
public delegate void CallBack<T>(T arg);
public delegate void CallBack<T,X>(T arg1, X arg2);
public delegate void CallBack<T,X,Y>(T arg1, X arg2,Y arg3);
public delegate void CallBack<T, X, Y, Z>(T arg1, X arg2, Y arg3, Z arg4);
public delegate void CallBack<T, X, Y, Z, W>(T arg1, X arg2, Y arg3, Z arg4, W arg5);
