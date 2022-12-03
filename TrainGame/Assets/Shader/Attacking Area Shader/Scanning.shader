Shader "Custom/Scanning"
{
    Properties
    {
        _baseColor ("Base Color", Color) = (0.2, 0.1, 0.5, 1)
        _sightRadius ("Sight Radius", Range(0,0.5)) = 0.5
        _darkColor ("Dark Color", Color) = (0, 0, 0)
        _lightColor ("Light Color", Color) = (1, 1, 1)
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
            float _sightRadius;
            float3 _darkColor;
            float3 _lightColor;

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
                float2 uv = i.uv - 0.5;
                uv = mul(rotate2D(_Time.z), uv);
                float len = length(uv);
                int radius = _sightRadius > len;
                float angle = atan2(uv.y, uv.x) / PI * 180.0;
                float sightRange = smoothstep(_sightRadius * 0.5, 0, angle) - smoothstep(0, -_sightRadius * 0.5, angle);
                float sight = sightRange * radius;
                return float4(lerp(_darkColor, _lightColor, saturate(sight)), 1.0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
