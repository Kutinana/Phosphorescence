-> prologue_after

=== prologue_after ===

-
  * “TF090 于频道 17 呼叫 X05F1822。[”]完毕。”  # Speaker: phos_idle
-

“这里是 X05F1822，收到来自 TF090 的信号。完毕。”  # Speaker: hakumei_shadow
“我看到光了！真的有欸！你真的是灯塔！”  # Speaker: hakumei_shadow

-
  * “为什么要骗你啦……”  # Speaker: phos_idle
-

“地图上真的没有任何标注欸！可能是因为这个灯塔被废弃很久了，地图已经不收录了……但没想到这样的灯塔还有人在维护……”  # Speaker: hakumei_shadow

-
  * “这样的？[”]哪样的？”  # Speaker: phos_questioning
-

“欸，你不知道吗？”  # Speaker: hakumei_shadow
“我只看得到灯喔，只有灯。”  # Speaker: hakumei_shadow

-
  * [“只有灯……”]“如果离得远的话，只看得到灯也很正常吧？”  # Speaker: phos_idle
-

“不是！是只有灯露在海面上！”  # Speaker: hakumei_shadow

-
  * “我不太理解……[”]你的意思是除了灯笼房，其他的部分都在海面以下？”  # Speaker: phos_questioning
-

“是啊！所以你真的好厉害，这都不撤离吗！”  # Speaker: hakumei_shadow

-
  * “……如果真是这样的话我早就死了吧！” # Speaker: phos_questioning
-
  
“也对喔……”  # Speaker: hakumei_shadow
“可是我真的看到只有灯笼房在海面上……感觉一个浪就能淹过去。”  # Speaker: hakumei_shadow

-
  * “这可……真是离奇。[”]我刚刚也到灯笼房里操作信号灯了，但周围就是正常的天空啊。”  # Speaker: phos_idle
-

“嗯……所以你还是不相信我？” # Speaker: hakumei_shadow

-
  * “不，我相信你。[”]问题在于如果真是这样的话，你要如何接收到我的援助？”  # Speaker: phos_serious
-

“我能提供食物与淡水，但你真的能取得到它们吗？我也能替你向其他电台转发求救信号，但 1988 年的我们真的能找到你吗？”  # Speaker: phos_serious
“如果你真的来自 2088 年……我要怎么才能帮到你？”  # Speaker: phos_serious

“……” # Speaker: hakumei_shadow
“……可以的！” # Speaker: hakumei_shadow

-
  * “欸？”  # Speaker: phos_surprised
-

“你能接收到我的信号，还能与我说话……已经帮了我太多了。” # Speaker: hakumei_shadow
“我连续监听了 155 个小时的电台，几乎都要放弃了……但是这时候你回答了，还像这样一直想着要如何帮助我……真的，谢谢你。” # Speaker: hakumei_shadow
“而且，我不是能看到你的光吗！说不定我也能收到来源于你的物资。” # Speaker: hakumei_shadow
“在所有的可能性都穷尽之前……” # Speaker: hakumei_shadow
“我不想放弃。” # Speaker: hakumei_shadow

-
  * “……我明白了。[”]我将尽我所能。”  # Speaker: phos_smile
-

“谢谢你！”  # Speaker: hakumei_shadow

-
  * “等你真正拿到物资之后再谢我也不迟。”  # Speaker: phos_smile
-

“我正好也对你能不能收到物资有些好奇。换句话说，如果物资消失了，本身也能证明你的存在。”  # Speaker: phos_serious
“剩下的问题是，你要怎么到灯笼房来。”  # Speaker: phos_thinking

“啊，这不用担心！‘薄明’上就有救生艇，我会开小艇过来的。”  # Speaker: hakumei_shadow
“虽然这次需要救援的是我啦……”  # Speaker: hakumei_shadow

-
  * “那很好。[”]那么现在对我来说唯一的问题就是你是否真的是亟待救援的科研船了。”
-

“啊！所以你还是不相信我！”
“还是说，只有你用莫尔斯电码打了我的呼号，我没有回应，觉得不公平？”

VAR is_telling_onboard = false

-
  * “没有[。”]，毕竟说要自证的本来也就是我。”
    “嗨呀，真是可靠呢。”
  
  * “有一点。”
    “真小气，是不是因为你也想让‘薄明’打一遍你的呼号啊。”
    ** “是的。”
      “哼哼，想要也不给！我可懒得再跑到甲板上一次啦。”
      ~is_telling_onboard = true
    ** “不……那还是算了。”
      “还算你识相！我可懒得再跑到甲板上一次啦。”
      ~is_telling_onboard = true
-

“总之……还是谢谢你的信任！”
“如果可以的话，能请你提供一些食物与淡水吗？船上的余量可能只够我使用非常短的时间了。”

-
  * “当然，你有多少船员？”
    “啊，他们……”
    “因为船上的物资相当匮乏，他们主动进入了休眠舱……现在保持苏醒状态的只有我一个人。”
    ** “……对不起，你相当孤独吧。”
      “没事啦！这不是联系上你了嘛！”
    ** “休眠舱？[”]啊，是科幻电影里经常出现的那个？”
      “嘛……差不多？”
      “原来你还看科幻电影欸，你最喜欢哪部？”
      *** “现在不是闲聊的时候吧[……”]，等你安全了之后我们再慢慢聊。”
        “好吧好吧。”
-

“那么就麻烦你了！等你放好了记得给我打信号喔，我会给你回一遍‘磷光’的呼号的！”

-
  * “那听上去还挺可怕的。”
-

“欸，为什么？”

-
  * “照刚刚你的描述，我所在的灯塔不是只有灯笼房在海面上了吗？”
-

“是的？”

-
  * “换句话说，你的船和灯笼房在同一个高度[……”]。但对我来说灯笼房当然在空中啊。假设我能看到你的船，那它岂不是在天上飞？”
-

“对喔！那不是很酷吗！”

-
  * “……完全不觉得。”
-

“好冷漠！”

-
  * “……总之我去打包一些物资，先失陪了。”
-

-
  * [（离开无线电台）]
  （前往粮食库）
-

-> END
