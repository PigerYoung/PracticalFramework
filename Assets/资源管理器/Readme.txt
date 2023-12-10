资源加载的方案：
1.Inspector窗口拖拽
    使用方法：在脚本中用public声明变量，然后在inspector窗口中把要加载的资源拖拽给该脚本的public变量
    不建议在大型项目使用，在公司项目也不要用
    如果独立开发可以使用
    不支持热更新

2.Resources
    使用方法：用Resources.Load，Resources.LoadAsync，Resources.LoadAll加载资源
    可以在商用项目，公司项目中使用，但是Resources文件夹存放资源有限，大概只能存放2G资源，谨慎使用
    不支持热更
    同步：做一件事情，完成之后再做下一件事情。事情必须做完一件在做另一件
    异步：做一件事情，开始做的时候，可以不等事情做完就开始做下一件事情，多个事情可以一起做
    同步和异步最大的区别就在于：同步会阻塞主线程，异步不会阻塞主线程
    只要通过Resources加载的资源，如果不释放就会一直储存在内存中，需要自己手动释放

3.AssetBundle
    用AssetBundle.LoadFromXXX等方法来加载资源。
    商业项目常用的资源加载方案，推荐使用这种方式来加载
    效率比Resources加载高，占用内存小，有限发布后所占用空间也小
    支持热更新

4.Addressables(可寻址资源系统)
    可以理解为高级的AssetBundle，资源管理由Unity内部自动完成。
    目前还在发展中，可能会有bug，主流商业游戏还是使用AssetBundle做资源加载
    支持热更新

5.AssetDatabase
    用AssetDatabase.LoadAssetPath方法来加载资源
    仅限于编辑器模式，主要用于在编辑器模式下用代码更改本地文件
    不支持热更新

用ResourcesManager资源管理器专门负责资源的加载功能（使用的异步加载），避免代码的重复性
test脚本用来测试Resources的加载方法
ResourcesManager为资源管理器，用该管理器专门负责Resources的资源加载--Framework