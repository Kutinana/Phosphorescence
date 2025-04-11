// Copyright 2014-2024 Elringus (Artyom Sovetnikov). All Rights Reserved.

using UnityEngine.UI;

namespace BlendModes
{
    [ExtendedComponent(typeof(Text))]
    public class UITextExtension : MaskableGraphicExtension<Text>
    {
        public override string[] GetSupportedShaderFamilies () => new[] {
            "UIDefaultFont"
        };
    }
}
