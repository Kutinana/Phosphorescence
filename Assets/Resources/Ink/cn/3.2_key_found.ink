-> key_found

=== key_found ===

$DISABLE_ALL_ACTIONS #auto:true
$SLEEP:1 #auto:true

“……” #type:RightAvatarText #speaker:磷光 #avatar:phos_confused_scared_1
“有变化吗？” #type: LeftAvatarText #speaker:薄明 #avatar:holo_seriously_thinking #simulated_voice: sfx_hakumei_re
“……有。放在地上的银色钥匙？” #type:RightAvatarText #speaker:磷光 #avatar:phos_confused_0.5 #simulated_voice: sfx_phos_re
“啊？真有啊？我随口乱说的。” #type: LeftAvatarText #speaker:薄明 #avatar:holo_surprised_2 #simulated_voice: sfx_hakumei_do
“……这是什么原理呢。” #type:RightAvatarText #speaker:磷光 #avatar:phos_thinking_3 #simulated_voice: sfx_phos_do
“灯笼房刷新了？” #type: LeftAvatarText #speaker:薄明 #avatar:holo_confused #simulated_voice: sfx_hakumei_re
“开玩笑的吧。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speechless #simulated_voice: sfx_phos_do
“还真不一定是玩笑。如果把灯笼房视作一个处于叠加态下的时空泡，你我在进入时空泡时势必要令其塌缩至确定的状态，对这个时空泡内施加的影响也因此局限在了自己的时空。但一旦我们均退出了时空泡，它又重新回归叠加态，此前的影响便得以同步到另一个时空……” #type: LeftAvatarText #speaker:薄明 #avatar:holo_seriously_thinking #simulated_voice: sfx_hakumei_do
“停停停，我听不懂。能不能说中文？” #type:RightAvatarText #speaker:磷光 #avatar:phos_confused_flip_1 #simulated_voice: sfx_phos_re
“啊，抱歉。就当我在说梦话吧。快去试试万能钥匙？它开 1982 年的锁理应是小菜菜菜—” #type: LeftAvatarText #speaker:薄明 #avatar:holo_happy_1 #simulated_voice: sfx_hakumei_re #auto:true
“菜——” #type: LeftAvatarText #avatar:holo_happy_1 #avatar_opacity:0.1 #simulated_voice: sfx_hakumei_re #auto:true
“菜——” #type: LeftAvatarText #avatar:holo_happy_1 #avatar_opacity:0.01 #simulated_voice: sfx_hakumei_re #auto:true

$SLEEP:1.5 #auto:true

“……喂？” #type:RightAvatarText #speaker:磷光 #avatar:phos_confused_0.5 #simulated_voice: sfx_phos_re
“……薄明？” #type:RightAvatarText #speaker:磷光 #avatar:phos_confused_flip_1 #simulated_voice: sfx_phos_re
“又是开玩笑吗？” #type:RightAvatarText #speaker:磷光 #avatar:phos_confident_2 #simulated_voice: sfx_phos_do
“薄明？” #type:RightAvatarText #speaker:磷光 #avatar:phos_confused_flip_2 #simulated_voice: sfx_phos_re

$EVENT:beacon_stop #auto:true
$SLEEP:2 #auto:true

“该死，怎么偏偏在这个时候停电……！” #type:RightAvatarText #speaker:磷光 #avatar:phos_surprised #simulated_voice: sfx_phos_do

$EVENT:diesel_runs_out #auto:true
$ENABLE_ALL_ACTIONS

-> DONE
