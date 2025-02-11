// Copyright 2014-2024 Elringus (Artyom Sovetnikov). All Rights Reserved.

#if BLENDMODES_TILEMAP

using UnityEngine.Tilemaps;

namespace BlendModes
{
    [ExtendedComponent(typeof(TilemapRenderer))]
    public class TilemapRendererExtension : RendererExtension<TilemapRenderer>
    {
        private static ShaderProperty[] props;

        public override string[] GetSupportedShaderFamilies () => new[] {
            "SpritesDefault",
            "SpritesHsbc"
        };

        public override ShaderProperty[] GetDefaultShaderProperties () => props ??= new[] {
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

#endif
