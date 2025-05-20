
-> prologue

=== prologue ===

$SLEEP:2
$DISABLE_ALL_ACTIONS
$MUSIC:sfx_radio_noise_01

“……请求救援。完毕。”  #type:LeftAvatarText #speaker:未知信号 #background_pic:bg_pic_pureblack #skippable:false #simulated_voice: sfx_talk_high_pitch_01 #avatar:shadow_hakumei #avatar_opacity:0.05
“MAYDAY, MAYDAY, MAYDAY。 任何单位，任何单位，任何单位。” #type: LeftAvatarText #speaker:未知信号 #background_pic:bg_pic_pureblack #avatar:shadow_hakumei #avatar_opacity:0.05
“这里是科研船「薄明」，呼号 X92HKM。位于北纬 66°35'26"、西经 17°56'16"。” #type:LeftAvatarText #speaker:未知信号 #background_pic:bg_pic_pureblack #avatar:shadow_hakumei #avatar_opacity:0.05
“引擎失去动力，正以三节向西南方向飘行。无伤亡报告。UTC 时间 1905。请求救援。完毕。” # type:LeftAvatarText #speaker:未知信号 #background_pic:bg_pic_pureblack #avatar:shadow_hakumei #avatar_opacity:0.05
“MAYDAY, MAYDAY, MAYDAY。 任何单位……”  # type:LeftAvatarText #speaker:未知信号 #background_pic:bg_pic_pureblack #avatar:shadow_hakumei #avatar_opacity:0.05

$STOP_MUSIC
$EVENT:WakeUpFromRadio
$SLEEP:3

“……” #type: RightAvatarText #speaker:磷光 #avatar:phos_sleeping
“……咦？”  #type: RightAvatarText #speaker:磷光 #avatar:phos_wake_up #simulated_voice:sfx_phos_do
“这是什么信号……”  # type: RightAvatarText #speaker:磷光 #avatar:phos_speechless #simulated_voice:sfx_phos_do
“啊，求援？”  # type: RightAvatarText #speaker:磷光 #avatar:phos_surprised #simulated_voice:sfx_phos_re
“得快点打开……”  # type: RightAvatarText #speaker:磷光 #avatar:phos_default #simulated_voice:sfx_phos_do #auto:true

“……真希望哪天你能想起我啊。” #type: FullScreenText #background_pic:bg_pic_pureblack

“……” #type: RightAvatarText #speaker:磷光 #avatar:phos_default
“…………薄明？” #type: RightAvatarText #speaker:磷光 #avatar:phos_confused_0.5 #simulated_voice:sfx_phos_do #skippable:false #sleep:3 #auto:true

$THANK_PAGE

-> DONE