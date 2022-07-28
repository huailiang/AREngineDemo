namespace HuaweiARUnitySDK
{
    using UnityEngine;
    using UnityEngine.Rendering;
    
    public class BackGroundRenderer : MonoBehaviour
    {
        private Camera m_Camera;
        private CommandBuffer m_VideoCommandBuffer;
        public Material m_backGroundMaterial;

        void Awake()
        {
            m_Camera = GetComponent<Camera>();
            InitializeCommandBuffer();
        }

        void OnDestroy()
        {
            m_Camera.RemoveCommandBuffer(CameraEvent.BeforeForwardOpaque, m_VideoCommandBuffer);
            m_Camera.RemoveCommandBuffer(CameraEvent.BeforeGBuffer, m_VideoCommandBuffer);
        }

        void InitializeCommandBuffer()
        {
            m_Camera.clearFlags = CameraClearFlags.Depth;
            m_Camera.depth = -1;
            m_VideoCommandBuffer = new CommandBuffer();
            m_VideoCommandBuffer.Blit(m_backGroundMaterial.mainTexture, BuiltinRenderTextureType.CurrentActive,
                m_backGroundMaterial);
            m_Camera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, m_VideoCommandBuffer);
            m_Camera.AddCommandBuffer(CameraEvent.BeforeGBuffer, m_VideoCommandBuffer);
        }

    }
}