Shader "Custom/OutlineShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,0,1)
        _OutlineThickness ("Outline Thickness", Range(0.0, 10.0)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _OutlineColor;
            float _OutlineThickness;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float alpha = tex2D(_MainTex, uv).a;

                // Outline sampling
                float outline = 0.0;
                float thickness = _OutlineThickness / 100.0;

                outline += tex2D(_MainTex, uv + float2(thickness, 0)).a;
                outline += tex2D(_MainTex, uv + float2(-thickness, 0)).a;
                outline += tex2D(_MainTex, uv + float2(0, thickness)).a;
                outline += tex2D(_MainTex, uv + float2(0, -thickness)).a;

                if (alpha > 0.1)
                {
                    return tex2D(_MainTex, uv);
                }
                else if (outline > 0.1)
                {
                    return _OutlineColor;
                }
                else
                {
                    return fixed4(0,0,0,0);
                }
            }
            ENDCG
        }
    }
}
