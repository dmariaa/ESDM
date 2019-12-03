Shader "ESDM/Item Slot" {
    Properties {
        _MainTex ("Font Texture", 2D) = "white" {}
        _Color ("Text Color", Color) = (1,1,1,1)
        [MaterialToggle]_Flashing ("Color Flashing", float) = 0.0
        _FlashingSpeed ("Flashing speed", Range(1, 10)) = 2
    }

    SubShader {

        Tags {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }
        Lighting Off Cull Off ZTest Always ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            uniform float4 _MainTex_ST;
            uniform fixed4 _Color;
            uniform float _Flashing;
            uniform float _FlashingSpeed;

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color * _Color;
                o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = i.color;
                col.a *= tex2D(_MainTex, i.texcoord).a;    
                if(_Flashing) 
                {
                    col.rgb *= sin(_Time.y * 2.0 * _FlashingSpeed) * 0.5 + 0.5;
                    return col;
                }           
                                              
                return col;
            }
            ENDCG
        }
    }
}