using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DebugBackground : MonoBehaviour
{
    private Camera m_Camera;
    public Material m_backGroundMaterial = null;
    private CommandBuffer m_VideoCommandBuffer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GetComponent<Camera>();
        InitializeCommandBuffer();
       
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
