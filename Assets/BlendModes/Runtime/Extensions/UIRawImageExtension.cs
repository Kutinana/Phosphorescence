// Copyright 2014-2024 Elringus (Artyom Sovetnikov). All Rights Reserved.

using UnityEngine.UI;

namespace BlendModes
{
    [ExtendedComponent(typeof(RawImage))]
    public class UIRawImageExtension : MaskableGraphicExtension<RawImage>
    {
        private static ShaderProperty[] props;

        public override string[] GetSupportedShaderFamilies () => new[] {
            "UIDefault",
            "UIHsbc"
        };

        public override ShaderProperty[] GetDefaultShaderProperties () => props ??= new[] {
            new ShaderProperty("_Hue", ShaderPropertyType.Float, 0),
            new ShaderProperty("_Saturation", ShaderPropertyType.Float, 0),
            new ShaderProperty("_Brightness", ShaderPropertyType.Float, 0),
            new ShaderProperty("_Contrast", ShaderPropertyType.Float, 0)
        };
    }
}
