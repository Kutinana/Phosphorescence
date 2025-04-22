
-> prologue_after_switch_on

=== prologue_after_switch_on ===

$DISABLE_INTERACTION #auto:true

（咔嗒） # type:FullScreenText #opacity:0.5

“MAYDAY RELAY, MAYDAY RELAY, MAYDAY RELAY。这里是 TF4674，灯塔「磷光」。”  # type:RightAvatarText #speaker:磷光
“抄收科研船「薄明」的 MAYDAY 信号，呼号 X92HKM，方位北纬 66°31'41"、西经 17°58'55"。” # type:RightAvatarText #speaker:磷光
“UTC 时间 1907。完毕。” # type:RightAvatarText #speaker:磷光

(咔嗒)  # type: FullScreenText
“呼……” # type: RightAvatarText #speaker:磷光
“请一定要，平安无事啊……” # type: RightAvatarText #speaker:磷光

$SLEEP:3 #skippable:false

“……喂？” # type: LeftAvatarText #speaker:未知信号
“……你好？” # type:LeftAvatarText #speaker:未知信号

- #type:FullScreenOptions #opacity:0.5
  * “你好。” #type: RightAvatarText #speaker:磷光
  * “我听得到。” #type: RightAvatarText #speaker:磷光
-

“啊。” #type:LeftAvatarText #speaker:未知信号
“好真实的幻听。” #type:LeftAvatarText #speaker:未知信号
“我想大概不是幻听。” #type: RightAvatarText #speaker:磷光
“诶，不是吗？” #type:LeftAvatarText #speaker:未知信号
“不是。幻听会给你编出这么详细的数据吗。” #type: RightAvatarText #speaker:磷光
“有道理，那你是谁？” #type:LeftAvatarText #speaker:未知信号

- #type:FullScreenOptions #opacity:0.5
  * “我是灯塔「磷光」[”]，呼号 TF4674。我已经转发你的求救信号，海岸警卫队应该很快就会联系你。” #type: RightAvatarText #speaker:磷光
-

“请保持于 VHF 频道 16 监听，确保补充定位保障开启，等待救援即可。” #type: RightAvatarText #speaker:磷光
“磷光。” #type:LeftAvatarText #speaker:未知信号
“什么事？”#type: RightAvatarText #speaker:磷光
“啊，没什么……你是灯塔？” #type:LeftAvatarText #speaker:未知信号
“是的。” #type: RightAvatarText #speaker:磷光
“原来现在还有灯塔！现在航标灯开着吗？我想去看看！” #type:LeftAvatarText #speaker:未知信号
“啊，抱歉。我刚刚睡着了，都不知道已经到晚上了。我现在就去开灯。”#type: RightAvatarText #speaker:磷光
“没关系！等你打开了航标灯，我应该也正好走到甲板上了！” #type:LeftAvatarText #speaker:未知信号
“感谢你的提醒，X92HKM。” #type: RightAvatarText #speaker:磷光
“以及，虽然救援还没到……但还请允许我提前道一声——” #type: RightAvatarText #speaker:磷光
“欢迎回家。” #type: RightAvatarText #speaker:磷光

-> END