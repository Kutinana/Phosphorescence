// Copyright 2014-2024 Elringus (Artyom Sovetnikov). All Rights Reserved.

using UnityEngine;

namespace BlendModes
{
    [ExtendedComponent(typeof(SkinnedMeshRenderer))]
    public class SkinnedMeshRendererExtension : RendererExtension<SkinnedMeshRenderer>
    {
        private static ShaderProperty[] props;

        public override string[] GetSupportedShaderFamilies () => new[] {
            "UnlitTransparent",
            "DiffuseTransparent"
        };

        public override ShaderProperty[] GetDefaultShaderProperties () => props ??= new[] {
            new ShaderProperty("_MainTex", ShaderPropertyType.Texture, Texture2D.whiteTexture),
            new ShaderProperty("_MainTex_ST", ShaderPropertyType.Vector, new Vector4(1, 1, 0, 0)),
            new ShaderProperty("_Color", ShaderPropertyType.Color, Color.white)
        };

        protected override string GetDefaultShaderName ()
        {
            return "Legacy Shaders/Diffuse";
        }
    }
}
