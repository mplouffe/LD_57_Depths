Shader "Unlit/FlowingWater"
{
    Properties
    {
        _MainTex("Water Texture", 2D) = "white" {}
        _FlowMap("Flow Map", 2D) = "white" {}
        _FlowStrength("Flow Strength", Float) = 0.1
        _Speed("Speed", Float) = 1.0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                sampler2D _FlowMap;
                float _FlowStrength;
                float _Speed;
                float4 _MainTex_ST;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float2 uvFlow : TEXCOORD1;
                    float4 vertex : SV_POSITION;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.uvFlow = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Read the flow direction from the flow map
                    float2 flow = tex2D(_FlowMap, i.uvFlow).rg * 2 - 1;

                    // Offset the main UV based on flow direction, time, strength, and speed
                    float2 offset = flow * _FlowStrength * _Time.y * _Speed;
                    float2 uv = i.uv + offset;

                    fixed4 col = tex2D(_MainTex, uv);
                    return col;
                }
                ENDCG
            }
        }
}