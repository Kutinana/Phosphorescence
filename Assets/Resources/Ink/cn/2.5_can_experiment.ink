-> can_experiment

=== can_experiment ===

$DISABLE_ALL_ACTIONS #auto:true
$SLEEP:1 #auto:true

VAR IsCanBoxSet = false
VAR HaveTriedCanExperiment = false
$READ_INFO:IsCanBoxSet #auto:true
$READ_INFO:HaveTriedCanExperiment #auto:true

{IsCanBoxSet == false && HaveTriedCanExperiment: -> second_time_not_set}

“「磷光」于甚高频频道 16 呼叫「薄明」。完毕。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do
“薄明收到来自磷光的呼叫。放好了？” #type: LeftAvatarText #speaker:薄明 #avatar:radio_happy #simulated_voice: sfx_hakumei_re

{IsCanBoxSet: “是的。但这真的有用吗？” #type:RightAvatarText #speaker:磷光 #avatar:phos_confused_0.5 #simulated_voice: sfx_phos_re}
{IsCanBoxSet == false: -> first_time_not_set}

“有没有用也得试了才知道嘛。” #type: LeftAvatarText #speaker:薄明 #avatar:radio_smile #simulated_voice: sfx_hakumei_do
“也对。但你打算怎么过来呢？「薄明号」不是失去动力了？” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do
“那肯定不是开着「薄明号」过来。我刚刚已经把锚链放了下去，这样她就不会乱跑了。” #type: LeftAvatarText #speaker:薄明 #avatar:radio_default_1 #simulated_voice: sfx_hakumei_do
“然后我打算乘救生艇过来。” #type: LeftAvatarText #speaker:薄明 #avatar:radio_smile #simulated_voice: sfx_hakumei_re
“开着救生艇到灯笼房吗……听上去像是在天上飞。” #type:RightAvatarText #speaker:磷光 #avatar:phos_thinking_3 #simulated_voice: sfx_phos_do
“这么说还挺酷的。” #type: LeftAvatarText #speaker:薄明 #avatar:radio_happy #simulated_voice: sfx_hakumei_re
“不管怎么想都觉得海平面在灯笼房的高度实在是很不可思议。” #type:RightAvatarText #speaker:磷光 #avatar:phos_look_upwards #simulated_voice: sfx_phos_do
“可惜这就是现实啊。我准备出发啦。灯笼房没锁门吧？” #type: LeftAvatarText #speaker:薄明 #avatar:radio_default_1 #simulated_voice: sfx_hakumei_do
“没锁。我把罐头放地上了。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do
“那就祝我一路顺风！” #type: LeftAvatarText #speaker:薄明 #avatar:radio_happy #simulated_voice: sfx_hakumei_re
“那就祝你一路顺风。” #type:RightAvatarText #speaker:磷光 #avatar:phos_smile #simulated_voice: sfx_phos_re

$SET_INFO:FinishedCanExperiment,true
$RECORD_CURRENT_TIME_TO:CanBoxSetAt
$EVENT:hakumei_departed_to_reach_can
$ENABLE_ALL_ACTIONS

-> DONE

=== first_time_not_set ===

“没有。” #type:RightAvatarText #speaker:磷光 #avatar:phos_look_upwards #simulated_voice: sfx_phos_do
“没有为什么不去啦！” #type: LeftAvatarText #speaker:薄明 #avatar:radio_proud #simulated_voice: sfx_hakumei_do

- #type:FullScreenOptions #opacity:0.8
  * [> “这就去。”]“这就去。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do
  * [> “想听听你的声音。”]“想听听你的声音。” #type:RightAvatarText #speaker:磷光 #avatar:phos_smile #simulated_voice: sfx_phos_re
    “……………………你…………” #type: LeftAvatarText #speaker:薄明 #avatar:radio_shocked_3 #simulated_voice: sfx_hakumei_mi #speed:0.2
    “快！去！” #type: LeftAvatarText #speaker:薄明 #avatar:radio_serious #simulated_voice: sfx_hakumei_mi
-

$SET_INFO:HaveTriedCanExperiment,true
$ENABLE_ALL_ACTIONS

-> DONE

=== second_time_not_set ===

“再逗她估计要被骂了，还是先去把罐头放好吧。” #type:RightAvatarText #speaker:磷光 #avatar:phos_smile #simulated_voice: sfx_phos_do

$ENABLE_ALL_ACTIONS

-> DONE

-> END
