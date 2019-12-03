Shader "ESDM/Sprites/Outline"
{
    Properties
    {
        [PerRendererData]_MainTex ("Texture", 2D) = "white" {} 
        _Color ("Color", Color) = (1, 1, 1, 1)
        _OutlineColor("Outline Color", Color) = (1,1,1,1)       
        [MaterialToggle]_Outline ("Outline", Float) = 0 
        _OutlineSize ("Outline", Range(1,5)) = 1
        
        [MaterialToggle]_OutlineAnimated ("Outline Animated", Float) = 0
        _OutlineSpeed ("Outline Animation Speed", Range(1,10)) = 1
        
    }
    SubShader
    {
        Tags { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
        }
        
        Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		
        Pass
        {
            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members _SinTime)
#pragma exclude_renderers d3d11
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            float4 _MainTex_ST;           
            fixed4 _Color;
            float4 _MainTex_TexelSize;
            fixed4 _OutlineColor;
            float _Outline;           
            int _OutlineSize;
            
            float _OutlineAnimated;
            int _OutlineSpeed;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color: COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color: COLOR;                
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                col *= _Color;
                col *= i.color;
                               
                if(_Outline==0.0) {
                    return col;
                }
                                    
                fixed leftPixel =   tex2D(_MainTex, i.uv + float2(-_MainTex_TexelSize.x * _OutlineSize, 0)).a;
                fixed upPixel =     tex2D(_MainTex, i.uv + float2(0, _MainTex_TexelSize.y * _OutlineSize)).a;
                fixed rightPixel =  tex2D(_MainTex, i.uv + float2(_MainTex_TexelSize.x * _OutlineSize, 0)).a;
                fixed bottomPixel = tex2D(_MainTex, i.uv + float2(0, -_MainTex_TexelSize.y * _OutlineSize)).a;
 
                // fixed outline = (1 - leftPixel * upPixel * rightPixel * bottomPixel) * col.a;
                fixed outline = max(max(leftPixel, upPixel), max(rightPixel, bottomPixel)) - col.a;                 
                _OutlineColor *= sin(_Time.y * 2.0 * _OutlineAnimated * _OutlineSpeed) * 0.5 + 0.5;                                                                                          
                return lerp(col, _OutlineColor, outline);
            }
            ENDCG
        }
    }
}
