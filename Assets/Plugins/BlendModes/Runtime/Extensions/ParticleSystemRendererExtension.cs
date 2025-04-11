// Copyright 2014-2024 Elringus (Artyom Sovetnikov). All Rights Reserved.

#if BLENDMODES_PARTICLES

using UnityEngine;

namespace BlendModes
{
    [ExtendedComponent(typeof(ParticleSystemRenderer))]
    public class ParticleSystemRendererExtension : RendererExtension<ParticleSystemRenderer>
    {
        private static ShaderProperty[] props;

        public override string[] GetSupportedShaderFamilies () => new[] {
            "ParticlesAdditive",
            "ParticlesHsbc"
        };

        public override ShaderProperty[] GetDefaultShaderProperties () => props ??= new[] {
            new ShaderProperty("_MainTex", ShaderPropertyType.Texture, Texture2D.whiteTexture),
            new ShaderProperty("_TintColor", ShaderPropertyType.Color, Color.white),
            new ShaderProperty("_InvFade", ShaderPropertyType.Float, 1f),
            new ShaderProperty("_Hue", ShaderPropertyType.Float, 0),
            new ShaderProperty("_Saturation", ShaderPropertyType.Float, 0),
            new ShaderProperty("_Brightness", ShaderPropertyType.Float, 0),
            new ShaderProperty("_Contrast", ShaderPropertyType.Float, 0)
        };

        protected override string GetDefaultShaderName ()
        {
            return "Particles/Standard Unlit";
        }
    }
}

#endif
