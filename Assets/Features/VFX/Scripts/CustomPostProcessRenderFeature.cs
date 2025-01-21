using PlasticGui.WorkspaceWindow.QueryViews;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Features.VFX
{

    public class CustomPostProcessRenderFeature : ScriptableRendererFeature, IDisposable
    {
        [SerializeField] private Shader _fShader;
        private CustomPostProcessRenderPass _pass; 
        private Material _material;
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (renderingData.cameraData.cameraType == CameraType.Game)
                renderer.EnqueuePass(_pass);
        }

        public override void Create()
        {
            _material = CoreUtils.CreateEngineMaterial(_fShader);
            _pass = new CustomPostProcessRenderPass(_material);

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CoreUtils.Destroy(_material);
            }
        }
        public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
        {
            base.SetupRenderPasses(renderer, renderingData);
        }
    }
    [System.Serializable]
    public class CustomPostProcessRenderPass : ScriptableRenderPass
    {
        public readonly Material Material;
        //private RenderTextureDescriptor _descriptor;
        private RTHandle _camColorHandle;
        private RTHandle _camDerpthHandle;
        private RenderTextureDescriptor _lagBackDescriptor;
        private RTHandle _lagBackHandle = null;
        private RTHandle _camResultHandle = null;
        public CustomPostProcessRenderPass(Material material)
        {
            _lagBackDescriptor = new RenderTextureDescriptor(Screen.width,
        Screen.height, RenderTextureFormat.Default, 0);
            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            Material = material;
            _lagBackHandle = null;
            ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Depth);
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            // get the source target from rendering data every frame
            _camColorHandle = renderingData.cameraData.renderer.cameraColorTargetHandle;
            _camDerpthHandle = renderingData.cameraData.renderer.cameraDepthTargetHandle;
        }
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            //Set the red tint texture size to be the same as the camera target size.
            _lagBackDescriptor.width = cameraTextureDescriptor.width;
            _lagBackDescriptor.height = cameraTextureDescriptor.height;
            //Check if the descriptor has changed, and reallocate the RTHandle if necessary.
        }
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (renderingData.cameraData.isPreviewCamera) return;
            if (_camColorHandle == null || _camDerpthHandle == null) return;
            RenderingUtils.ReAllocateIfNeeded(ref _lagBackHandle, _lagBackDescriptor, name: "_LagBackTexture");
            RenderingUtils.ReAllocateIfNeeded(ref _camResultHandle, _lagBackDescriptor, name: "_CamResultTexture");
            VolumeStack st = VolumeManager.instance.stack;
            MeatVolumeEffect eff = st.GetComponent<MeatVolumeEffect>();
            CommandBuffer cmd = CommandBufferPool.Get();
            if (eff != null)
            {
                using var scope = new ProfilingScope(cmd, new ProfilingSampler("Please kill me"));
                DoRendering(cmd);
                //RTHandles.Alloc(20, name: "HI");
            }
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            CommandBufferPool.Release(cmd);           
        }
        private void DoRendering(CommandBuffer cmd)
        {
            Debug.Log(_lagBackHandle);
            Material.SetTexture("_LagBackTexture", _lagBackHandle);
            //cmd.SetGlobalTexture("_LagBackTexture", _lagBackHandle);
            //cmd.SetGlobalTexture("_CameraColorTexture", _camColorHandle);
            Blit(cmd, _camColorHandle, _camResultHandle);
            Blit(cmd, _camColorHandle, _camColorHandle, Material);
            Blit(cmd, _camResultHandle, _lagBackHandle);
        }
    }
}
