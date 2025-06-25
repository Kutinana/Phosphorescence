$DISABLE_ALL_ACTIONS #auto:true
$SLEEP:1 #auto:true

-> failed_calling

=== failed_calling ===

- #type:FullScreenOptions #opacity:0.8
  + [“呼叫「薄明」”] “「磷光」于甚高频频道 16 呼叫「薄明」。完毕。” #type:RightAvatarText #speaker:磷光 #avatar:phos_speaking #simulated_voice: sfx_phos_do
    $SLEEP:2
    （无线电静默） #type:FullScreenText #opacity:0.8
    -> failed_calling
  * （离开无线电台）
-

$ENABLE_ALL_ACTIONS

-> END
