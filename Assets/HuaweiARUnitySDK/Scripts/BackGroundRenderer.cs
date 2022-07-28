namespace HuaweiARUnitySDK
{
    using HuaweiARUnitySDK;
    using UnityEngine;
    using UnityEngine.Rendering;

    /**
     * \if english 
     * @brief Renders the device's camera as a background to the attached Unity camera component.
     * \else
     * @brief 将相机预览渲染到unity的相机背景上。
     * \endif
     */
    public class BackGroundRenderer : MonoBehaviour
    {
        private Camera m_Camera;
        private CommandBuffer m_VideoCommandBuffer;
        private bool bCommandBufferInitialized = false;
        public Material BackGroundMaterial = null;

        void Awake()
        {
            m_Camera = GetComponent<Camera>();
            InitializeCommandBuffer();
        }

        void OnDestroy()
        {
            if (!bCommandBufferInitialized)
            {
                return;
            }
            m_Camera.RemoveCommandBuffer(CameraEvent.BeforeForwardOpaque, m_VideoCommandBuffer);
            m_Camera.RemoveCommandBuffer(CameraEvent.BeforeGBuffer, m_VideoCommandBuffer);
            bCommandBufferInitialized = false;
        }

        void InitializeCommandBuffer()
        {
            m_Camera.clearFlags = CameraClearFlags.Depth;
            m_Camera.depth = -1;
            m_VideoCommandBuffer = new CommandBuffer();
            m_VideoCommandBuffer.Blit(BackGroundMaterial.mainTexture, BuiltinRenderTextureType.CurrentActive,
                BackGroundMaterial);
            m_Camera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, m_VideoCommandBuffer);
            m_Camera.AddCommandBuffer(CameraEvent.BeforeGBuffer, m_VideoCommandBuffer);
            bCommandBufferInitialized = true;
        }

    }
}