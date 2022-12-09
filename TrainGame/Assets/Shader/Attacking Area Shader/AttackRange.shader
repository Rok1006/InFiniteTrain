Shader "Custom/AttackRange"
{
    Properties 
	{
		_ProjectionTex ("Projection Texture", 2D) = "" {}
		_MainColor ("Main Color", Color) = (1,1,1,1)
		[HDR] _EmissionColor ("Emission Color", Color) = (0,0,0,1)
		_Emission ("Emission", Range(0, 1)) = 0.5
		_EdgeColor ("Edge Color", Color) = (1,1,1,1)
		_RotateAngle ("Rotate Angle", Range(0, 360)) = 0
		_MinRange ("Min Range", Range(0, 0.25)) = 0
		_AttackAngle ("Attack Angle", Range(0, 360)) = 60

		_Power ("Power", float) = 5
		_Strength ("Strength", float) = 1

		_ScanSpeed ("Scan Speed", float) = 0.5
		_ScanCount ("Scan Count", float) = 3
		_ScanStrength ("Scan Strength", float) = 1

		[Toggle(ENABLE_CIRCLE)] _isCircle ("is circle", float) = 0
		[Toggle(Line_FromCenter)] _startFromCenter ("start from center?", float) = 1
		[Toggle(Line_UsingAbsValue)] _isUsingAbs ("using abs value?", float) = 0
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	#define PI 3.14159265359
	
	struct MeshData 
	{
		float4 vertex : POSITION;
	};

	struct Interpolators 
	{
		float4 pos : SV_POSITION;
		float4 uvShadow : TEXCOORD1;
	};
	
	float4x4 unity_Projector;
				
	float4 _MainColor;
	float4 _EmissionColor;
	float _Emission;
	float4 _EdgeColor;
	sampler2D _ProjectionTex;
	float _RotateAngle;
	float _MinRange;
	float _AttackAngle;
	float _Power;
	float _Strength;
	float _ScanSpeed;
	float _ScanCount;
	float _ScanStrength;

	float _isCircle;
	float _startFromCenter;
	float _isUsingAbs;

	Interpolators vert1 (MeshData v)
	{
		Interpolators o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uvShadow = mul(unity_Projector, v.vertex);
		return o;
	}

	float2 Rotate(float2 samplePosition, float rotation)
	{
		float angle = rotation / 180 * PI;
		float sine, cosine;
		sincos(angle, sine, cosine);
		return float2(cosine * samplePosition.x + sine * samplePosition.y, 
					cosine * samplePosition.y - sine * samplePosition.x);
	}

	float2x2 rotate2D (float angle) {
		return float2x2 (
			cos(angle), -sin(angle),
			sin(angle),  cos(angle)
		);
	}

	float2x2 scale2D (float2 scale) {
		return 	float2x2(
			scale.x, 0,
			0, scale.y 
		);
	}

	float getRange (float length2, float2 uv) {		
		if (length2 < _MinRange)
		{
			return 0;
		}
		else
		{
			float angle = atan2(uv.y, uv.x) / PI * 180;
			return 1 - step(smoothstep(_AttackAngle * 0.5, 0, angle) - smoothstep(0, -_AttackAngle * 0.5, angle), 0);
		}
	}

	float getOutterCircle (float length, float range, float4 uvShadow) {
		float fullMask = tex2Dproj (_ProjectionTex, UNITY_PROJ_COORD(uvShadow)).r;
		const float BORDER = 0.001;
		if (uvShadow.x / uvShadow.w < BORDER
		|| uvShadow.x / uvShadow.w > 1 - BORDER  
		|| uvShadow.y / uvShadow.w < BORDER
		|| uvShadow.y / uvShadow.w > 1 - BORDER)
		{
			fullMask = 0;
		}

		float alpha = pow(length, _Power) * fullMask * _Strength;
		return alpha;
	}

	float getShape(float4 uvShadow) {
		float fullMask = tex2Dproj (_ProjectionTex, UNITY_PROJ_COORD(uvShadow)).r;
		const float BORDER = 0.001;
		if (uvShadow.x / uvShadow.w < BORDER
		|| uvShadow.x / uvShadow.w > 1 - BORDER  
		|| uvShadow.y / uvShadow.w < BORDER
		|| uvShadow.y / uvShadow.w > 1 - BORDER)
		{
			fullMask = 0;
		}
		return fullMask;
	}

	float4 frag1 (Interpolators i) : SV_Target
	{	
		float2 uv = (i.uvShadow.xy / i.uvShadow.w) - 0.5f;
		uv = mul(uv, rotate2D(_RotateAngle));
		float len = length(uv);
		float len2 = uv.x * uv.x + uv.y * uv.y;
		float3 color = 0;

		if (_isCircle) {
			float range = getRange(len2, uv);
			float alpha = getOutterCircle(len, range, i.uvShadow);
			
			float centerWave = 0;
			if(alpha > 0)
			{
				float dis = len + _Time.y * _ScanSpeed;
				dis *= _ScanCount;
				float wave = dis - floor(dis);
				wave = pow(wave, _Power) * _ScanStrength;
				alpha = clamp(alpha + wave, 0, _Strength);
			}
			
			color = _MainColor + (_EmissionColor * _Emission);
			return float4(color, alpha * range);
		} else {
			float range = getRange(len2, uv);
			float alpha = getOutterCircle(len, range, i.uvShadow);
			
			float centerWave = 0;
			if(alpha > 0)
			{
				float dis = 0;
				if (!_startFromCenter)
					dis = (uv.y) + _Time.y * _ScanSpeed;
				else {
					if (!_isUsingAbs)
						dis = len + _Time.y * _ScanSpeed;
					else
						dis = (abs(uv.x) + abs(uv.y)) + _Time.y * _ScanSpeed;
				}

				dis *= _ScanCount;
				float wave = dis - floor(dis);

				if (_startFromCenter)
					wave = pow(wave, _Power) * _ScanStrength;
				
				if (!_startFromCenter)
					alpha = clamp(alpha + wave, 0.3, _Strength);
				else
					alpha = clamp(alpha + wave, 0, _Strength);
			}
			
			color = _MainColor + (_EmissionColor * _Emission);
			return float4(color, alpha * range);
		}
		
		return float4(color, 1.0);
	}

	float4 frag2 (Interpolators i) : SV_Target
	{
		float2 uv = (i.uvShadow.xy / i.uvShadow.w) - 0.5;
		uv = mul(uv, rotate2D(_RotateAngle));
		float len = length(uv);
		float len2 = uv.x * uv.x + uv.y * uv.y;
		float3 color = 0;

		float2 scale = 0.5;
		float4 scaledUVShadow1 = float4(mul((i.uvShadow.xy / i.uvShadow.w) - 0.01, scale2D(1.02)), i.uvShadow.zw);
		float4 scaledUVShadow2 = float4(mul((i.uvShadow.xy / i.uvShadow.w), scale2D(1)), i.uvShadow.zw);
		float range = getRange(len2, uv);
		float alpha = getShape(scaledUVShadow2).r - getShape(scaledUVShadow1).r;

		color = _EdgeColor + (_EmissionColor * _Emission);
		return float4(color, alpha * range);
	}
	ENDCG

	Subshader 
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent"}
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Offset -1, -1
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert1
			#pragma fragment frag1
			
			ENDCG
		}

		Pass {
			CGPROGRAM
			#pragma vertex vert1
			#pragma fragment frag2
			
			ENDCG
		}
	}
}
