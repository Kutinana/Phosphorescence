
-> prologue_after_switch_on

=== prologue_after_switch_on ===

$DISABLE_ALL_ACTIONS #auto:true
$SFX:sfx_radio_switch_01

（咔嗒） # type:FullScreenText #opacity:0.8

“MAYDAY RELAY, MAYDAY RELAY, MAYDAY RELAY。这里是 TF4674，灯塔「磷光」。”  # type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice:sfx_phos_do
“抄收科研船「薄明」的 MAYDAY 信号，呼号 X92HKM，方位北纬 66°31'41"、西经 17°58'55"。” # type:RightAvatarText #speaker:磷光 #avatar:phos_look_upwards #simulated_voice:sfx_phos_do
“UTC 时间 1907。完毕。” # type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice:sfx_phos_do

$SFX:sfx_radio_switch_02
(咔嗒)  # type: FullScreenText #opacity:0.8

“呼……” # type: RightAvatarText #speaker:磷光 #avatar:phos_sigh
“请一定要，平安无事啊……” # type: RightAvatarText #speaker:磷光 #avatar:phos_default

$SLEEP:3 #skippable:false

“……喂？” # type: LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_re #avatar:radio_default_2
“……你好？” # type:LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_re #avatar:radio_smile

- #type:FullScreenOptions #opacity:0.8
  * [> “你好。”]“你好。” #type: RightAvatarText #speaker:磷光 #avatar:phos_default #simulated_voice: sfx_phos_re
  * [> “我听得到。”]“我听得到。” #type: RightAvatarText #speaker:磷光 #avatar:phos_default #simulated_voice: sfx_phos_do
-

“啊。” #type:LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_do #avatar:radio_default_2
“好真实的幻听。” #type:LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_do #avatar:radio_default_1
“我想大概不是幻听。” #type: RightAvatarText #speaker:磷光 #avatar:phos_awkward #simulated_voice: sfx_phos_do
“诶，不是吗？” #type:LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_re #avatar:radio_shocked_3
“不是。幻听会给你编出这么详细的数据吗。” #type: RightAvatarText #speaker:磷光 #avatar:phos_smile #simulated_voice: sfx_phos_do
“有道理，那你是谁？” #type:LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_do #avatar:radio_default_2

- #type:FullScreenOptions #opacity:0.8
  * [> “我是灯塔「磷光」”]“我是灯塔「磷光」，呼号 TF4674。我已经转发你的求救信号，海岸警卫队应该很快就会联系你。” #type: RightAvatarText #speaker:磷光 #avatar:phos_smile #simulated_voice: sfx_phos_do
-

“请保持于 VHF 频道 16 监听，确保补充定位保障开启，等待救援即可。” #type: RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_re
“磷光。” #type:LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_do #avatar:radio_smile
“什么事？” #type: RightAvatarText #speaker:磷光 #avatar:phos_confused_flip_1 #simulated_voice: sfx_phos_re
“有趣的名字。” #type:LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_do #avatar:radio_happy
“……谢谢？” #type: RightAvatarText #speaker:磷光 #avatar:phos_awkward #simulated_voice: sfx_phos_re
“你是灯塔来着？” #type:LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_re #avatar:radio_default_2
“是的。” #type: RightAvatarText #speaker:磷光 #avatar:phos_default #simulated_voice: sfx_phos_do
“原来这个时代还有灯塔欸。” #type:LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_mi #avatar:radio_default_1
“经常有人这么说。” #type: RightAvatarText #speaker:磷光 #avatar:phos_confident_2 #simulated_voice: sfx_phos_do
“这么说，你也有航标灯吗？” #type:LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_re #avatar:radio_default_2
“是啊，没有航标灯怎么叫灯塔呢。” #type: RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_re
“欸，那现在开着吗？我想去看看！” #type:LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_mi #avatar:radio_smile
“这么说起来……应该没开。” #type: RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do
“欸——” #type:LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_la #avatar:radio_shocked_1
“我刚刚睡着了，都不知道已经到晚上了。我现在就去开灯。” #type: RightAvatarText #speaker:磷光 #avatar:phos_happy_1 #simulated_voice: sfx_phos_re
“好喔。等你打开了航标灯，我应该也正好走到甲板上了！” #type:LeftAvatarText #speaker:未知信号 #simulated_voice: sfx_hakumei_do #avatar:radio_happy
“感谢你的提醒，X92HKM。” #type: RightAvatarText #speaker:磷光 #avatar:phos_happy_2 #simulated_voice: sfx_phos_do
“以及，虽然救援还没到……但还请允许我提前道一声——” #type: RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_re
“欢迎回家。” #type: RightAvatarText #speaker:磷光 #avatar:phos_smile #simulated_voice: sfx_phos_do

$ENABLE_ALL_ACTIONS

-> END