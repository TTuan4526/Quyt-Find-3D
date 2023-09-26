Shader "Unlit/GlowOutline" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_BumpMap ("Bumpmap", 2D) = "bump" {}
		[HDR] _RimColor ("Rim Color", Vector) = (1,0.9,0.4,0)
		_RimPower ("Rim Power", Range(0.5, 8)) = 1.65
		[HDR] _OutlineColor ("Outline Color", Vector) = (255,213,0,1)
		_OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.05
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}