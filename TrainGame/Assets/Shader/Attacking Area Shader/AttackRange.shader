Shader "Custom/AttackRange"
{
    Properties
    {
        _baseColor ("Base Color", Color) = (0.2, 0.1, 0.5, 1)
    }
    SubShader
    {
        Tags{"Queue"="Transparent" "IgnoreProjector"="True"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            #define PI 3.14159265359

            float4 _baseColor;

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float3 color : COLOR;
            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float2 worldUV : TEXCOORD1;
            };

            float2x2 rotate2D (float angle) {
                return float2x2 (
                    cos(angle), -sin(angle),
                    sin(angle),  cos(angle)
                );
            }

            Interpolators vert (MeshData v)
            {
                Interpolators o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                
                return float4(_baseColor);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
