-> beacon_test_2

=== beacon_test_2 ===

$DISABLE_ALL_ACTIONS #auto:true
$SLEEP:1 #auto:true

VAR isBeaconOn = true
$SET_VARIABLE:isBeaconOn #auto:true

“TF4674 于甚高频频道 16 呼叫 X92HKM。问现在的灯光？完毕。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do
{isBeaconOn: “开着的。完毕。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_2 #simulated_voice: sfx_hakumei_do}
{isBeaconOn == false: “关着的。完毕。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_2 #simulated_voice: sfx_hakumei_do}
“正确。请等待下一次连线。完毕。” #type:RightAvatarText #speaker:磷光 #avatar:phos_smile #simulated_voice: sfx_phos_do

-> END
