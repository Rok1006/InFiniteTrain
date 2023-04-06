Shader "Custom/A10WaterColorEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_WobbTex ("WobbleTex", 2D) = "grey" {}
		_WobbScale ("WobbleTex Scale", Float) = 1
		_WobbPower ("Wobbing Power", Float) = 1
		_EdgeSize ("Edge Size", Float) = 1
		_EdgePower ("Edge Power", Float) = 1
		_PaperTex ("Paper", 2D) = "grey" {}
		_PaperScale ("Paper Scale", Float) = 1
		_PaperPower ("Paper Power", Float) = 1
        _color ("color", Color) = (0.4, 0.1, 0.9)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off 
        ZWrite Off 
        ZTest Always

        CGINCLUDE	
        #include "UnityCG.cginc"
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            
            float4 ColorMod(float4 c, float d) {
				return c - (c - c * c) * (d - 1);
			}

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

        ENDCG
        
//Wobble Effect
        Pass {
			CGPROGRAM
			#pragma vertex vert_wobb
			#pragma fragment frag
            //#include "UnityCG.cginc"

            struct v2f_wobb 
			{
                float2 uv : TEXCOORD0;
				float2 uv_Wobb : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _WobbTex;
			float _WobbScale;
			float _WobbPower;
            float _color;

			v2f_wobb vert_wobb(appdata v) {
				float aspect = _ScreenParams.x / _ScreenParams.y;

				v2f_wobb o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
                // v.uv += sin(v.uv.x + float2(0, _Time.y*0.3))*0.1;
                // v.uv += sin(v.uv.y + float2(_Time.y, 0.3))*0.1;
				o.uv_Wobb = v.uv * float2(aspect, 1) * _WobbScale *sin(_Time.x/10);
				return o;
			}
			
			float4 frag(v2f_wobb i) : SV_Target {
                float2 uv = i.uv;
                float3 w = float3(0.299, 0.587, 0.144);
                float grayscale = dot(tex2D(_MainTex, uv), w);
                grayscale = floor(grayscale * 10) / 10;

                float warpStrength = 0.2;  //warping the random pattern at the back to simulate moving sand
                //uv += sin(uv.y + float2(time.y, 1.5))*warpStrength;
                // i.uv_Wobb += sin(uv.x + float2(0, _Time.y*0.3))*warpStrength;
                // i.uv_Wobb += sin(uv.y + float2(_Time.y, 0.3))*warpStrength;
				float2 wobb = dot(tex2D(_WobbTex, i.uv_Wobb).wy * 2 - 1, w);;
                wobb = floor(wobb * 10) / 10;
                // float g = dot(wobb, w);
                // g = floor(g * 10) / 10;
                float2 wobber = i.uv + wobb * _WobbPower;
				float4 source = tex2D(_MainTex, wobber);
                //source = lerp(source, grayscale, i.uv_Wobb.x );
                
				return source;
			}
			ENDCG
		}
//EDGE of stroke
        Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float _EdgeSize;
			float _EdgePower;

			float4 frag(v2f i) : SV_Target {
				float2 offset = _MainTex_TexelSize.xy * _EdgeSize;
				float4 src_l = tex2D(_MainTex, i.uv + float2(-offset.x, 0));
				float4 src_r = tex2D(_MainTex, i.uv + float2(+offset.x, 0));
				float4 src_b = tex2D(_MainTex, i.uv + float2(0, -offset.y));
				float4 src_t = tex2D(_MainTex, i.uv + float2(0, +offset.y));
				float4 source = tex2D(_MainTex, i.uv);

				float4 grad = abs(src_r - src_l) + abs(src_b - src_t);
				float intensity = saturate(0.333 * (grad.x + grad.y + grad.z));
				float v = _EdgePower * intensity + 1;
                float output = source - (source - source * source) * (v - 1);
				return ColorMod(source, v);
                //return output;
			}
			ENDCG
		}
        Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

            sampler2D _PaperTex;
			float _PaperScale;
			float _PaperPower;

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_Paper : TEXCOORD1;
            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_Paper : TEXCOORD1;
            };

            Interpolators vert (MeshData v)
            {
                float aspect = _ScreenParams.x / _ScreenParams.y;
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.uv_Paper = v.uv * float2(aspect, 1) * _PaperScale;
                return o;
            }
            float4 frag (Interpolators i) : SV_Target
            {
                float4 src = tex2D(_MainTex, i.uv);
				float4 paper = tex2D(_PaperTex, i.uv_Paper).x;

				float d = _PaperPower * (paper - 0.5) + 1;
				return ColorMod(src, d);
            }
            ENDCG
        }
        //End----

    }
}
    
