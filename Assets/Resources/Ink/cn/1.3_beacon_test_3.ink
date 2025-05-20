-> beacon_test_3

=== beacon_test_3 ===

$DISABLE_ALL_ACTIONS #auto:true
$SLEEP:1 #auto:true

VAR isBeaconOn = true
$READ_VARIABLE:isBeaconOn #auto:true

“TF4674 于甚高频——” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do
{isBeaconOn: “开着。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_2 #simulated_voice: sfx_hakumei_do}
{isBeaconOn == false: “关着。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_2 #simulated_voice: sfx_hakumei_do}
“……你能不能等我说完。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speechless_2 #simulated_voice: sfx_phos_do
“你每次都报这么一大长串不累吗？反正这个频段这片海域上就你我两人吧？” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_1 #simulated_voice: sfx_hakumei_do
“……遵守《无线电操作守则》对确保通讯完整与信息安全有着重要意义。” #type:RightAvatarText #speaker:磷光 #avatar:phos_sigh #simulated_voice: sfx_phos_do
“好好好大安全家。那你起码把呼号省省？直接叫我薄明不就好了。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_speechless_1 #simulated_voice: sfx_hakumei_do
“……既然你已经连续答对三次了我看也没有继续验证的必要了。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speechless_2 #simulated_voice: sfx_phos_do
“欸——再来一次！最后一次！” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_shocked_2 #simulated_voice: sfx_hakumei_do
“我每次都要爬上爬下很累的好不好。” #type:RightAvatarText #speaker:磷光 #avatar:phos_angry_2 #simulated_voice: sfx_phos_do
“哎呀最后一次嘛！就当锻炼身体了。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_sad_2 #simulated_voice: sfx_hakumei_do
“真受不了你，最后一次啊。” #type:RightAvatarText #speaker:磷光 #avatar:phos_sigh #simulated_voice: sfx_phos_do

$ENABLE_ALL_ACTIONS

-> END
