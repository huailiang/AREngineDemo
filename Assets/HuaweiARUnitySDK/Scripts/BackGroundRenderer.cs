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
        private static float[] QUAD_TEXCOORDS = {0f, 1f, 0f, 0f, 1f, 1f, 1f, 0f};
        private float[] transformedUVCoords = QUAD_TEXCOORDS;
        public Material BackGroundMaterial = null;

        void Awake()
        {
            m_Camera = GetComponent<Camera>();
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

        void Update()
        {
            if (!ARFrame.TextureIsAvailable())
            {
                return;
            }
            if (BackGroundMaterial != null)
            {
                const string backroundTex = "_MainTex";
                const string leftTopBottom = "_UvLeftTopBottom";
                const string rightTopBottom = "_UvRightTopBottom";

                BackGroundMaterial.SetTexture(backroundTex, ARFrame.CameraTexture);

                if (ARFrame.IsDisplayGeometryChanged())
                {
                    transformedUVCoords = ARFrame.GetTransformDisplayUvCoords(QUAD_TEXCOORDS);
                }

                BackGroundMaterial.SetVector(leftTopBottom, new Vector4(transformedUVCoords[0], transformedUVCoords[1],
                    transformedUVCoords[2], transformedUVCoords[3]));
                BackGroundMaterial.SetVector(rightTopBottom, new Vector4(transformedUVCoords[4], transformedUVCoords[5],
                    transformedUVCoords[6], transformedUVCoords[7]));
                m_Camera.projectionMatrix =
                    HuaweiARUnitySDK.ARSession.GetProjectionMatrix(m_Camera.nearClipPlane, m_Camera.farClipPlane);
                if (!bCommandBufferInitialized)
                {
                    InitializeCommandBuffer();
                }
            }
        }
    }
}