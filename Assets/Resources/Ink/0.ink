
-> prologue

=== prologue ===

“……请求救援。完毕。”  # Speaker: hakumei_shadow
“Pan Pan。 任何单位，任何单位，任何单位。这里是 X05HKM 科研船‘薄明’，位于北纬 66 度 31 分 25 秒, 西经 18 度 6 分 12 秒。我船失去动力，正在飘行。请求救援。完毕。”  # Speaker: hakumei_shadow
“Pan Pan。 任何单位……”  # Speaker: hakumei_shadow

-
  * “……嗯？”  # Speaker: phos_sleeping
-

“啊啊……我睡着了？”  # Speaker: phos_wake_up
“这是……有人在求援？”  # Speaker: phos_questioning
“得快点打开发报机……”  # Speaker: phos_idle

-
  * [（打开发报机）] （咔嗒）
    ** “X05HKM，这里是 TF0900[……”] 灯塔‘磷光’。请切换至 VHF 频道 17 继续通讯。完毕。”  # Speaker: phos_idle
    
“……我船失去动力，正在飘……”  # Speaker: hakumei_shadow

-
  * “原来是在自动发报……？”  # Speaker: phos_idle

-
  * [（切换至频道 17）]
  （无线电静默）
    ** [（保持等待）]
    （无线电静默）
    “啊！对不起！我忘记把频道换过来了！”  # Speaker: hakumei_shadow
      *** “没关系[。”]，很高兴与你建立联系，‘薄明’。”  # Speaker: phos_idle
        -> prologue_after_connection
    
    ** [（切换回频道 16）]
      -> prologue_in_channel_16
    
  * [（保持频道 16）]
  （突然静默）
    ->prologue_in_channel_16

= prologue_in_channel_16
“TF0900！这里是 ‘薄明’！太好了……终于有人……”  # Speaker: hakumei_shadow
“啊，我是不是应该换到频道 17？对不起，我太激动了……”  # Speaker: hakumei_shadow

-
  * “没关系，就保持这样吧。”  # Speaker: phos_smile
    -> prologue_after_connection
  * “是的，我们在频道 17 继续通话吧。”  # Speaker: phos_idle
    ** [（切换到频道 17）]
      -> prologue_after_connection
  
= prologue_after_connection
“谢谢！很高兴认识你，‘磷光’！”  # Speaker: hakumei_shadow

-
  * “能介绍一下你船情况吗？”  # Speaker: phos_idle
-

“啊，好的！”
“这里是远洋科研考察船 ‘薄明’，于 25 天前完成了在挪威斯瓦尔巴特群岛北极科考站的考察活动并起锚。”  # Speaker: hakumei_shadow
“我们原计划停泊在扬马延获取补给，并在那之后前往柯克沃尔。但在约七天前遭遇暴风。”
“大部分物资都因此而遗失。”  # Speaker: hakumei_shadow

-
  * “那还真不容易……”
-

“更糟糕的是，我们在 155 个小时前触礁，大部船舱进水。
“勉强维持了失去动力开始飘行。”  # Speaker: hakumei_shadow
“……你是第一个回应我们的求救信号的人。”  # Speaker: hakumei_shadow

-
  * “辛苦了，一定很不容易吧。[”]居然连续 155 个小时都没有收到回复吗？”  # Speaker: phos_surprised
-

“是的。不过在这个时代也算常见吧。”  # Speaker: hakumei_shadow

-
  * “这个时代……？”  # Speaker: phos_questioning
-

“毕竟‘大涨潮’之后……欸？”  # Speaker: hakumei_shadow
“不好意思，容我确认一下。你方呼号是 TF0900？”  # Speaker: hakumei_shadow
 
-
  * “是的[。”]，有什么问题吗？”  # Speaker: phos_idle
-
  
“可是，TF 呼号头不是在 2059 年就被废弃了吗？”  # Speaker: hakumei_shadow

-
  * “2059 年？”  # Speaker: phos_questioning
  * “被废弃？”  # Speaker: phos_questioning
-

“是的，2059 年开始实施的《全球无线电呼号简化方案》。你没听说过？”  # Speaker: hakumei_shadow

-
  * [“可今年不是 1988 年吗？”]“与其说是有没有听说过……今年不是 1988 年吗？”  # Speaker: phos_idle
-
  
“你在说什么，今年不是 2088……”  # Speaker: hakumei_shadow
“嘶……”  # Speaker: hakumei_shadow
“对不起对不起我可能在做梦我这就醒过来。”  # Speaker: hakumei_shadow

-
  * “你冷静点……”  # Speaker: phos_speechless
    ** “其实我也觉得很奇怪[。”]，我记得为了防止数字 0 和字母 O 混淆，任何呼号头都不允许带有 0。”  # Speaker: phos_thinking
      *** “而你方的呼号是 X05F1822……”  # Speaker: phos_thinking
-

“我没有在开玩笑！我们真的遇到了危险！”  # Speaker: hakumei_shadow
“啊……你觉得我在骗你对吧！”  # Speaker: hakumei_shadow

-
  * “现在不是探讨谁在说谎的时候。[”]我收到了来自你方的 Pan-pan，援助你就是我的职责。”  # Speaker: phos_serious
-
“至于真相，可以等你方安全后再谈。”  # Speaker: phos_serious

“谢谢……可是我要怎么才能让你信任我呢？”  # Spekaer: hakumei_shadow

-
  * “恰恰相反[。”]，我需要让你信任我，这样我的援助才是有意义的。”  # Speaker: phos_serious
-

“我有一个简单的方法自证。我方在你船约南偏西 33 度 1.1 海里处。请立即派遣一名通讯员到甲板上观测该方向。” # Speaker: phos_serious
“我将使用白昼信号灯以莫尔斯电码打出你船的呼号，以证明我方的存在。当你方收到信号后，请立即与我回报。这个方案如何？” # Speaker: phos_serious

“你很有自信嘛。好喔，我这就去甲板。”  # Speaker: hakumei_shadow

-
  * [（离开无线电台）]
-

-> END