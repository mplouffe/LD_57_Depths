Shader "Custom/URP_WaterFlow2D"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Flowmap (Normal Map)", 2D) = "bump" {}
        _Magnitude("Distortion Magnitude", Range(0,1)) = 0.05
    }

        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            Blend SrcAlpha OneMinusSrcAlpha
            Pass
            {
                Name "WaterFlowURP"
                HLSLPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float4 positionCS : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };

                TEXTURE2D(_CameraOpaqueTexture);    // <-- Built-in in URP
                SAMPLER(sampler_CameraOpaqueTexture);

                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);

                float4 _MainTex_ST;
                half4 _Color;
                float _Magnitude;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.positionCS = TransformObjectToHClip(v.vertex.xyz);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                half4 frag(v2f i) : SV_Target
                {
                    // Sample the flowmap (normal map)
                    float2 flow = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv).rg;
                    flow = (flow * 2.0 - 1.0) * _Magnitude; // remap from [0,1] -> [-1,1] and multiply by strength

                    // Calculate new UVs for background
                    float2 screenUV = i.positionCS.xy / i.positionCS.w;
                    screenUV = screenUV * 0.5 + 0.5; // NDC to 0-1

                    screenUV += flow;

                    // Sample screen texture
                    half4 background = SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, screenUV);

                    // Apply color tint
                    return background * _Color;
                }

                ENDHLSL
            }
        }
}
