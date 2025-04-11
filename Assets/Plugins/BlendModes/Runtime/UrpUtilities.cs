// Copyright 2014-2024 Elringus (Artyom Sovetnikov). All Rights Reserved.

namespace BlendModes
{
    public static class UrpUtilities
    {
        public static bool UrpEnabled =>
            #if BLENDMODES_URP
            UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline is UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset;
        #else
            false;
        #endif
        public static bool FeatureEnabled =>
            #if BLENDMODES_URP
            IsBlendFeatureEnabled();
        #else
            false;
        #endif

        private static bool IsBlendFeatureEnabled ()
        {
            #if !BLENDMODES_URP
            return false;
            #else
            if (!UrpEnabled) return false;
            var urp = (UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset)UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline;
            foreach (var data in urp.rendererDataList)
            foreach (var feat in data.rendererFeatures)
                if (feat is RenderBlendModeEffect)
                    return true;
            return false;
            #endif
        }
    }
}
