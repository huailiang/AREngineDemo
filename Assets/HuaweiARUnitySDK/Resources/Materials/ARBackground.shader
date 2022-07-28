Shader "HuaweiAR/ARBackground"
{
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
	
    SubShader
    {
         Tags { "RenderType"="Opaque" "LightMode"="ForwardBase"}

        Pass
        {
            ZWrite Off

            GLSLPROGRAM
            #include "UnityCG.glslinc"
            uniform vec4 _MainTex_ST;

            #ifdef SHADER_API_GLES3
            #pragma only_renderers gles3
            #extension GL_OES_EGL_image_external_essl3 : require
            #endif

            #ifdef VERTEX
            out vec2 textureCoord;
            void main()
            {
                 gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
                textureCoord = TRANSFORM_TEX_ST(gl_MultiTexCoord0, _MainTex_ST).yx;
                textureCoord.xy = 1.0 - textureCoord.xy;
            }
            #endif

            #ifdef FRAGMENT
            in vec2 textureCoord;
            
             #ifdef SHADER_API_GLES3
            uniform samplerExternalOES _MainTex;
            #else
            uniform sampler2D _MainTex;
            #endif

            void main()
            {
                vec4 color = texture2D(_MainTex, textureCoord);
                gl_FragColor = color;
            }

            #endif

            ENDGLSL
        }
    }

}
