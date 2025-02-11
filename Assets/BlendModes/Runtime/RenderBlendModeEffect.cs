// Copyright 2014-2024 Elringus (Artyom Sovetnikov). All Rights Reserved.

// Can't conditionally compile the whole script, as it breaks Unity serialization.

using UnityEngine;
using UnityEngine.Rendering;
#if BLENDMODES_URP
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;
#endif

namespace BlendModes
{
    // https://docs.unity3d.com/6000.0/Documentation/Manual/urp/render-graph

    public class RenderBlendModeEffect
        #if BLENDMODES_URP
        : ScriptableRendererFeature
    #endif
    {
        #if BLENDMODES_URP

        // 1. Grabs current rendered content (scene) to a render texture; all the URP blend shaders
        //    have custom "LightMode" tag, so they are not rendered by default.
        private class BlendModesGrabPass : ScriptableRenderPass
        {
            public class Context : ContextItem
            {
                public TextureHandle Grab;
                public override void Reset () => Grab = TextureHandle.nullHandle;
            }

            private class Data
            {
                internal TextureHandle Scene;
            }

            private const string passId = nameof(BlendModesGrabPass);
            private const string textId = "BlendModesGrab";

            public override void RecordRenderGraph (RenderGraph graph, ContextContainer ctx)
            {
                using var pass = graph.AddRasterRenderPass<Data>(passId, out var data);
                var camera = ctx.Get<UniversalCameraData>();
                var resources = ctx.Get<UniversalResourceData>();
                var desc = camera.cameraTargetDescriptor;
                desc.msaaSamples = 1;
                desc.depthBufferBits = 0;
                var grab = UniversalRenderer.CreateRenderGraphTexture(graph, desc, textId, false);
                pass.UseTexture(data.Scene = resources.activeColorTexture);
                pass.SetRenderAttachment(ctx.GetOrCreate<Context>().Grab = grab, 0);
                pass.SetRenderFunc((Data data, RasterGraphContext raster) => {
                    Blitter.BlitTexture(raster.cmd, data.Scene, new(1, 1, 0, 0), 0, false);
                });
            }
        }

        // 2. Sets the grabbed content to a global texture used by the blend shaders.
        // 3. Draws the blend shaders with the custom "LightMode" tag.
        private class BlendModesDrawPass : ScriptableRenderPass
        {
            private class Data
            {
                internal TextureHandle Grab;
                internal RendererListHandle List;
            }

            private static readonly ShaderTagId tag = new("BlendModeEffect"); // Tags { "LightMode" = "BlendModeEffect" }
            private static readonly int grabId = Shader.PropertyToID("_BLENDMODES_UrpGrabTexture");
            private const string passId = nameof(BlendModesDrawPass);

            public override void RecordRenderGraph (RenderGraph graph, ContextContainer ctx)
            {
                using var pass = graph.AddRasterRenderPass<Data>(passId, out var data);
                var camera = ctx.Get<UniversalCameraData>();
                var light = ctx.Get<UniversalLightData>();
                var render = ctx.Get<UniversalRenderingData>();
                var resources = ctx.Get<UniversalResourceData>();
                var draw = CreateDrawingSettings(tag, render, camera, light, SortingCriteria.CommonTransparent);
                var filter = new FilteringSettings(RenderQueueRange.transparent);
                pass.AllowGlobalStateModification(true);
                pass.UseTexture(data.Grab = ctx.Get<BlendModesGrabPass.Context>().Grab);
                pass.UseRendererList(data.List = graph.CreateRendererList(new RendererListParams(render.cullResults, draw, filter)));
                pass.SetRenderAttachment(resources.cameraColor, 0);
                pass.SetRenderAttachmentDepth(resources.cameraDepth);
                pass.SetRenderFunc((Data data, RasterGraphContext raster) => {
                    raster.cmd.SetGlobalTexture(grabId, data.Grab);
                    raster.cmd.DrawRendererList(data.List);
                });
            }
        }

        private BlendModesGrabPass grab;
        private BlendModesDrawPass draw;

        public override void Create ()
        {
            grab = new();
            grab.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
            draw = new();
            draw.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        }

        public override void AddRenderPasses (ScriptableRenderer renderer, ref RenderingData data)
        {
            renderer.EnqueuePass(grab);
            renderer.EnqueuePass(draw);
        }

        #endif
    }
}
