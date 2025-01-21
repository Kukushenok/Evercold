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
        [SerializeField] private Shader _blindUpdateShader;
        [SerializeField] private Shader _blindApplyShader;
        private SunBlindnessPostProcessRenderPass _pass; 
        private Material _blindUpdateMaterial;
        private Material _blindApplyMaterial;
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (renderingData.cameraData.cameraType == CameraType.Game)
                renderer.EnqueuePass(_pass);
        }

        public override void Create()
        {
            _blindUpdateMaterial = CoreUtils.CreateEngineMaterial(_blindUpdateShader);
            _blindApplyMaterial = CoreUtils.CreateEngineMaterial(_blindApplyShader);
            _pass = new SunBlindnessPostProcessRenderPass(_blindUpdateMaterial, _blindApplyMaterial);

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CoreUtils.Destroy(_blindUpdateMaterial);
            }
        }
        public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
        {
            base.SetupRenderPasses(renderer, renderingData);
        }
    }
    [System.Serializable]
    public class SunBlindnessPostProcessRenderPass : ScriptableRenderPass
    {
        public readonly Material BlindApplyMaterial;
        public readonly Material BlindUpdateMaterial;
        //private RenderTextureDescriptor _descriptor;
        private RTHandle _camColorHandle;
        private RenderTextureDescriptor _blindDescriptor;
        private RTHandle _blindDescriptorStep = null;
        private RTHandle _lightBlinderTexture = null;
        public SunBlindnessPostProcessRenderPass(Material blindUpdateMaterial, Material blindApplyMaterial)
        {
            _blindDescriptor = new RenderTextureDescriptor(Screen.width,
        Screen.height, RenderTextureFormat.BGR101010_XR, 0);
            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            BlindApplyMaterial = blindApplyMaterial;
            BlindUpdateMaterial = blindUpdateMaterial;
            _lightBlinderTexture = null;
            ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Depth);
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            // get the source target from rendering data every frame
            _camColorHandle = renderingData.cameraData.renderer.cameraColorTargetHandle;
        }
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            //Set the red tint texture size to be the same as the camera target size.
            _blindDescriptor.width = cameraTextureDescriptor.width;
            _blindDescriptor.height = cameraTextureDescriptor.height;

            //Check if the descriptor has changed, and reallocate the RTHandle if necessary.
        }
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (renderingData.cameraData.isPreviewCamera) return;
            if (_camColorHandle == null) return;
            RenderingUtils.ReAllocateIfNeeded(ref _lightBlinderTexture, _blindDescriptor, name: "_BlindTexture");
            RenderingUtils.ReAllocateIfNeeded(ref _blindDescriptorStep, _blindDescriptor, name: "_BlindTextureStep");
            //RenderingUtils.ReAllocateIfNeeded(ref _lightBlinderTexture, _blindDescriptor, name: "_BlindTexture");
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
            //Debug.Log(_lightBlinderTexture);
            BlindUpdateMaterial.SetTexture("_BlindTexture", _lightBlinderTexture);
            BlindUpdateMaterial.SetTexture("_CameraTexture", _camColorHandle);
            BlindApplyMaterial.SetTexture("_BlindTexture", _lightBlinderTexture);

            //cmd.SetGlobalTexture("_LagBackTexture", _lagBackHandle);
            //cmd.SetGlobalTexture("_CameraColorTexture", _camColorHandle);
            Blit(cmd, _lightBlinderTexture, _blindDescriptorStep, BlindUpdateMaterial);
            Blit(cmd, _camColorHandle, _camColorHandle, BlindApplyMaterial);
            Blit(cmd, _blindDescriptorStep, _lightBlinderTexture);
        }
    }
}
