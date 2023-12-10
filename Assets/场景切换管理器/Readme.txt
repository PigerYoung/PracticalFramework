Editor为编辑器扩展的文件夹

切换场景管理器
 功能：
    同步(异步未完善):1.切换到当前场景
           2.切换到上一个场景
           3.切换到下一个场景
   异步加载
         注意事项：1.AsyncOperation相关的代码应写在一个协同程序中
	         2.AsyncOperation对象报错了异步加载的一些信息，如加载进度等
	         3.AsyncOperation对象.allowSceneActivation=true/false表示异步加载完毕后是否立即激活该场景
	         4.AsyncOperation对象.progress表示异步加载进度，范围在0-1
		注意：当allowSceneActivation为false时，这个参数就会卡在0.9，知道变为true后，这参数才会变为1
	         5.AsyncOperation对象.isDone表示异步加载是否完成，只有当progress为1时才能返回true，但这样就会切换到新场景，因此很难观测到该值
   	         6.在异步加载的时候，可以看到协同方法中参数有委托（带参数），应该还要有个参数(这个参数就是委托的参数的(因为该委托需要一个参数))，一般来说想要调用该方法
	            就直接传入一个参数的方法给委托，还要再传入委托中的参数即可调用；但是这里并没有写传给委托的参数，因此这里委托的参数可以直接用asyncOperation.progress
	            传入，因此在外部调用的时候直接传方法给委托即可

LoadSceneManager为场景切换管理器--Framework
SceneTest是测试场景切换的脚本，用编辑器中我的菜单/***切换，记得需要运行运行后才能切换，否则报错