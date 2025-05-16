-> chapter_2

=== chapter_2 ===

$DISABLE_ALL_ACTIONS #auto:true
$SLEEP:1 #auto:true

VAR isBeaconOn = true
$READ_VARIABLE:isBeaconOn #auto:true

“TF4674 于甚高频频道 16 呼叫 X92HKM。问现在的灯光？完毕。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do

$SLEEP:1.5

“咦？” #type:RightAvatarText #speaker:磷光 #avatar:phos_confused_0.5 #simulated_voice: sfx_phos_re
“TF4674 于甚高频频道 16 呼叫 X92HKM。收到请回答。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do

$SLEEP:1.5

“……总不至于是生气了吧。” #type:RightAvatarText #speaker:磷光 #avatar:phos_awkward #simulated_voice: sfx_phos_do
“……磷光于甚高频频道16呼叫薄明。完毕。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do

{isBeaconOn: “薄明收到！开着的！” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_happy #simulated_voice: sfx_hakumei_do}
{isBeaconOn == false: “薄明收到！关着的！” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_happy #simulated_voice: sfx_hakumei_do}

“你啊……能不能不要在这种时候开玩笑。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speechless_2 #simulated_voice: sfx_phos_do
“这不是乖乖叫我薄明就没事了嘛！” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_serious #simulated_voice: sfx_hakumei_do
“但这不是船的名字吗？” #type:RightAvatarText #speaker:磷光 #avatar:phos_confused_0.5 #simulated_voice: sfx_phos_re
“不是喔，是我的。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_smile #simulated_voice: sfx_hakumei_re
“或者更准确地说，我和船都叫薄明。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_happy #simulated_voice: sfx_hakumei_do
“那还挺巧的。” #type:RightAvatarText #speaker:磷光 #avatar:phos_smile #simulated_voice: sfx_phos_do
“哪里巧了啊！一听就知道是我爹犯懒吧！” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_angry #simulated_voice: sfx_hakumei_re
“我说我和你挺巧的。” #type:RightAvatarText #speaker:磷光 #avatar:phos_happy_1 #simulated_voice: sfx_phos_re
“我的名字也和这个灯塔一样，叫磷光。” #type:RightAvatarText #speaker:磷光 #avatar:phos_happy_2 #simulated_voice: sfx_phos_re
“欸？” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_shocked_1 #simulated_voice: sfx_hakumei_mi
“没开玩笑，我真的叫磷光。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do
“大概是这个灯塔对于我父亲来说很重要吧。” #type:RightAvatarText #speaker:磷光 #avatar:phos_happy_2 #simulated_voice: sfx_phos_re
“我小时候不喜欢这个名字，觉得它太复杂太难写。不过现在还蛮喜欢的。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do
“原来如此……” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_happy #simulated_voice: sfx_hakumei_la
“那为了庆祝我们这难得的巧合，叫我薄明吧？” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_sad_2 #simulated_voice: sfx_hakumei_re
“我考虑一下。” #type:RightAvatarText #speaker:磷光 #avatar:phos_look_upwards #simulated_voice: sfx_phos_do
“喂！” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_serious #simulated_voice: sfx_hakumei_mi
“好了回归正题。你又答对了。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_re
“是吗……” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_2 #simulated_voice: sfx_hakumei_la
“那看来我看到的光，就来自一百年前，灯塔「磷光」发出的光。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_1 #simulated_voice: sfx_hakumei_do
“很谨慎的结论啊，还不能断言看到的就是「磷光」吗？” #type:RightAvatarText #speaker:磷光 #avatar:phos_confused_0.5 #simulated_voice: sfx_phos_re
“是啊。不如说很可能不是吧。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_1 #simulated_voice: sfx_hakumei_la
“为什么？” #type:RightAvatarText #speaker:磷光 #avatar:phos_confused_flip_1 #simulated_voice: sfx_phos_re
“因为我所见的海平面几乎完全没过了整个灯塔。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_1 #simulated_voice: sfx_hakumei_re
“如果这是你所在的灯塔，就不能解释为什么你丝毫不受「大涨潮」影响。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_2 #simulated_voice: sfx_hakumei_do
“可如果你看到的不是我的灯塔，那为什么能看到对应的光？” #type:RightAvatarText #speaker:磷光 #avatar:phos_confused_0.5 #simulated_voice: sfx_phos_do
“问得好。因此我打算再做个实验。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_smile #simulated_voice: sfx_hakumei_do
“需要我做什么吗？” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_re
“需要你给我寄个快递。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_happy #simulated_voice: sfx_hakumei_re
“啊？” #type:RightAvatarText #speaker:磷光 #avatar:phos_confused_flip_2 #simulated_voice: sfx_phos_mi
“你不是之前还说要给我提供物资嘛？还是说那只是安慰我骗人的？” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_speechless_2 #simulated_voice: sfx_hakumei_do
“物资倒好说，但你不是刚刚才说看到的不是我的灯塔？” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_re
“既然我能收到你的信号，也能看到你的光，那为什么不能收到来自你的物资？” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_2 #simulated_voice: sfx_hakumei_re
“听上去确实值得一试。” #type:RightAvatarText #speaker:磷光 #avatar:phos_thinking_1 #simulated_voice: sfx_phos_do
“可是，具体要怎么做呢？” #type:RightAvatarText #speaker:磷光 #avatar:phos_thinking_2 #simulated_voice: sfx_phos_re
“随便找点什么放灯笼房里就行。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_1 #simulated_voice: sfx_hakumei_la
“随便啊……你有什么需要的吗？” #type:RightAvatarText #speaker:磷光 #avatar:phos_look_upwards #simulated_voice: sfx_phos_do
“我想尝尝看一百年前的吃的！” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_happy #simulated_voice: sfx_hakumei_mi
“吃的应该是随时间不断改良的吧？我这儿可没什么好东西啊。” #type:RightAvatarText #speaker:磷光 #avatar:phos_awkward #simulated_voice: sfx_phos_do
“这就叫体验嘛！如果什么都没有体验过就提前下结论可不行啊。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_smile #simulated_voice: sfx_hakumei_do
“也对。不过提前说好，我只有罐头。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_re
“罐头也没关系喔。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_1 #simulated_voice: sfx_hakumei_do
“要什么味的？” #type:RightAvatarText #speaker:磷光 #avatar:phos_look_upwards #simulated_voice: sfx_phos_re
“有什么味的？” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_default_2 #simulated_voice: sfx_hakumei_re
“有牛肉罐头、金枪鱼罐头和菠菜罐头。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do
“不要菠菜罐头。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_speechless_2 #simulated_voice: sfx_hakumei_la
“十罐菠菜罐头，明白了。” #type:RightAvatarText #speaker:磷光 #avatar:phos_happy_1 #simulated_voice: sfx_phos_do
“喂！” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_serious #simulated_voice: sfx_hakumei_mi
“挑食可不好啊。每种给你各拿一罐吧。” #type:RightAvatarText #speaker:磷光 #avatar:phos_happy_2 #simulated_voice: sfx_phos_do
“……那你放好之后和我说喔。我先去做下取快递的准备。” #type: LeftAvatarText #speaker:X92HKM #avatar:radio_speechless_3 #simulated_voice: sfx_hakumei_do

-> END
