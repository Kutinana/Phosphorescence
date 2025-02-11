// Copyright 2014-2024 Elringus (Artyom Sovetnikov). All Rights Reserved.

using UnityEngine;

namespace BlendModes
{
    [ExtendedComponent(typeof(SpriteRenderer))]
    public class SpriteRendererExtension : RendererExtension<SpriteRenderer>
    {
        private static ShaderProperty[] props;

        public override string[] GetSupportedShaderFamilies () => new[] {
            "SpritesDefault",
            "SpritesHsbc",
            "SpritesVectorGradient"
        };

        public override ShaderProperty[] GetDefaultShaderProperties () => props ??= new[] {
            new ShaderProperty("_Color", ShaderPropertyType.Color, Color.white),
            new ShaderProperty("_Hue", ShaderPropertyType.Float, 0),
            new ShaderProperty("_Saturation", ShaderPropertyType.Float, 0),
            new ShaderProperty("_Brightness", ShaderPropertyType.Float, 0),
            new ShaderProperty("_Contrast", ShaderPropertyType.Float, 0)
        };

        protected override string GetDefaultShaderName ()
        {
            return "Sprites/Default";
        }
    }
}
