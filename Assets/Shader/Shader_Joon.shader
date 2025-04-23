Shader "Custom/Shader_Joon"
{
    Properties
    {
        _Color ("Main Color", Color) = (0.2, 0.8, 1, 0.4)
        _RimColor ("Rim Color", Color) = (1, 1, 1, 1)
        _RimPower ("Rim Power", Range(1, 10)) = 3.0
        _Wobble ("Distortion Strength", Range(0, 0.05)) = 0.02
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Back

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl" // for Unity pipeline

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 normalWS : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            float4 _Color;
            float4 _RimColor;
            float _RimPower;
            float _Wobble; // I heard that is kind of effect that is added based on time and the object’s world position
            // but I just added from https://assetstore.unity.com/packages/vfx/shaders/wobble-effects-shaders-built-in-renderer-105863?srsltid=AfmBOoqtI3NOmdK2GEbDOZT3Kd1E7ABDUo4WvYKIO7c3Jdqq7j0jci3G 
            // I took the code apart to understand it, and used it.

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                float3 worldPos = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.positionHCS = TransformWorldToHClip(worldPos);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.worldPos = worldPos;
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                float3 camDir = normalize(GetCameraPositionWS() - IN.worldPos);
                float rim = 1.0 - saturate(dot(camDir, normalize(IN.normalWS)));
                rim = pow(rim, _RimPower);

                rim += sin(_Time.y * 6.0 + IN.worldPos.x * 12.0) * _Wobble;

                float3 finalColor = _Color.rgb + _RimColor.rgb * rim;
                return float4(finalColor, _Color.a * rim);
            }

            ENDHLSL // end of HLSL
        }
    }
}
