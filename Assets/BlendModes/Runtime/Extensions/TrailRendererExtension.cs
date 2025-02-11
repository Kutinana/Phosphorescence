// Copyright 2014-2024 Elringus (Artyom Sovetnikov). All Rights Reserved.

using UnityEngine;

namespace BlendModes
{
    [ExtendedComponent(typeof(TrailRenderer))]
    public class TrailRendererExtension : RendererExtension<TrailRenderer>
    {
        private static ShaderProperty[] props;

        public override string[] GetSupportedShaderFamilies () => new[] {
            "ParticlesAdditive"
        };

        public override ShaderProperty[] GetDefaultShaderProperties () => props ??= new[] {
            new ShaderProperty("_MainTex", ShaderPropertyType.Texture, Texture2D.whiteTexture),
            new ShaderProperty("_TintColor", ShaderPropertyType.Color, Color.white),
            new ShaderProperty("_InvFade", ShaderPropertyType.Float, 1f)
        };

        protected override string GetDefaultShaderName ()
        {
            return "Particles/Standard Unlit";
        }
    }
}
