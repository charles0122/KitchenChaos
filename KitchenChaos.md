

# Kitchen Chaos 开发笔记

### 0.开发环境

unity版本: 2022.2.2f1c1

3dURP项目

初始化项目结构

![image-20230205201129407](img/KitchenChaos/image-20230205201129407.png)

减少渲染平台的等级，删除相关urp配置文件

![](img/KitchenChaos/image-20230205201214628.png)



![image-20230205210544728](img/KitchenChaos/image-20230205210544728.png)

![image-20230205210607878](img/KitchenChaos/image-20230205210607878.png)

完成制作后改成msaa 超采样抗锯齿方法

### 使用cinemachine

![image-20230205223043833](img/KitchenChaos/image-20230205223043833.png)

### 使用新输入系统

![image-20230205231726096](img/KitchenChaos/image-20230205231726096.png)

当前效果

![image-20230206125511144](img/KitchenChaos/image-20230206125511144.png)

交互生成物品

![image-20230206131419431](img/KitchenChaos/image-20230206131419431.png)

新增容器柜台

![image-20230206170920435](img/KitchenChaos/image-20230206170920435.png)

![image-20230206170956383](img/KitchenChaos/image-20230206170956383.png)

![image-20230206170619358](img/KitchenChaos/image-20230206170619358.png)

### 切菜柜台、切菜进度及效果

![image-20230208011846313](img/KitchenChaos/image-20230208011846313.png)

### 煎炸柜台、垃圾桶、盘子柜台

使用继承制作 盘子物品

使用结构体完成汉堡原料表

使用接口完成进度条UI展示

![image-20230208212557362](img/KitchenChaos/image-20230208212557362.png)