-> beacon_test_too_fast

=== beacon_test_too_fast ===

$DISABLE_ALL_ACTIONS #auto:true
$SLEEP:1 #auto:true

VAR isBeaconOn = true
$READ_VARIABLE:isBeaconOn #auto:true

“TF4674 于甚高频——” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do #auto:true
“怎么这么快！你是不是没去改！” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_2 #simulated_voice: sfx_hakumei_re
“不，我是在测试你是不是在单纯交替着说唬我。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speechless_2 #simulated_voice: sfx_phos_do
“怎么会！我一直都在好好盯着灯塔的喔，现在也是{isBeaconOn:开|关}着的吧？” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_1 #simulated_voice: sfx_hakumei_do
“……倒是没错。” #type:RightAvatarText #speaker:磷光 #avatar:phos_sigh #simulated_voice: sfx_phos_do
“哎呀，为了增进你我间的信任，别用呼号了，直接叫我薄明吧？我也会叫你磷光的！” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_speechless_1 #simulated_voice: sfx_hakumei_do
“……既然你都说了一直盯着灯塔了我看也没有继续验证的必要了。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speechless_2 #simulated_voice: sfx_phos_do
“欸——再来一次！最后一次！” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_shocked_2 #simulated_voice: sfx_hakumei_do
“我每次都要爬上爬下很累的好不好。” #type:RightAvatarText #speaker:磷光 #avatar:phos_angry_2 #simulated_voice: sfx_phos_do
“哎呀最后一次嘛！就当锻炼身体了。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_sad_2 #simulated_voice: sfx_hakumei_do
“真受不了你，最后一次啊。” #type:RightAvatarText #speaker:磷光 #avatar:phos_sigh #simulated_voice: sfx_phos_do

$ENABLE_ALL_ACTIONS

-> END
