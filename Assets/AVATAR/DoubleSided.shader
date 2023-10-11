Shader "Custom/DoubleSided"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_NormalStrength("Normal Strength", Range(0,2)) = 1.0
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 200
			cull off

			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _NormalMap;

			struct Input
			{
				float2 uv_MainTex;
				float2 uv_NormalMap;
			};

			half _Glossiness;
			half _Metallic;
			half _NormalStrength;
			fixed4 _Color;

			UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;

				// Modify the normal map strength
				fixed3 normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
				normal = fixed3(normal.xy * _NormalStrength, normal.z);
				o.Normal = normalize(normal);

				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}
