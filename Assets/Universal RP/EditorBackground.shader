Shader "HuaweiAR/EditorBackground"
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

            #ifdef VERTEX

            out vec2 textureCoord;
            
            void main()
            {
                gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
                textureCoord = TRANSFORM_TEX_ST(gl_MultiTexCoord0, _MainTex_ST);
            }
            #endif

            #ifdef FRAGMENT
            in vec2 textureCoord;
            uniform sampler2D _MainTex;

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
