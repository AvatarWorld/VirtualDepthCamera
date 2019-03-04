// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/DepthView"
{
	SubShader{
	ZTest Always
	Cull Off
	ZWrite Off
	Fog{ Mode Off }
	Tags{ "RenderType" = "Opaque" }

	Pass {
	  CGPROGRAM
	  #pragma vertex vert
	  #pragma fragment frag
	  #include "UnityCG.cginc"
	  #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 
	  #pragma target 3.0

	  UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);

	  struct v2f {
	float4 pos : SV_POSITION;
	float2 uv : TEXCOORD0;
	  };

	  v2f vert(appdata_img v) {
	v2f o = (v2f)0;
	o.pos = UnityObjectToClipPos(v.vertex);
	o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
	return o;
	  }

	  float frag(v2f i) : SV_TARGET{
		return Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv).x);
	  }
	  ENDCG
	}
	}
}