Shader "MSK/Crop/BlendOff/CropAlpha" {
	Properties{
		_MainTex("MainTex", 2D) = "white" {}
		_Top("Top",  range(0.0, 1.0)) = 0.0
		_Bottom("Bottom",  range(0.0, 1.0)) = 0.0
		_Left("Left",  range(0.0, 1.0)) = 0.0
		_Right("Right",  range(0.0, 1.0)) = 0.0
	}
	CGINCLUDE
	#include "UnityCG.cginc"
	struct VS_OUT {
		half4 position:POSITION;
		fixed2 texcoord0:TEXCOORD0;
	};

	sampler2D _MainTex;
	fixed4 _MainTex_ST;
	fixed _Top;
	fixed _Bottom;
	fixed _Left;
	fixed _Right;

	VS_OUT vert(appdata_base input) {
		VS_OUT o;
		o.position = UnityObjectToClipPos(input.vertex);
		o.texcoord0 = TRANSFORM_TEX(input.texcoord, _MainTex);
		return o;
	}
	fixed4 frag(VS_OUT input) : SV_Target {
		fixed2 t = input.texcoord0;
		fixed4 c = tex2D(_MainTex, input.texcoord0);
		if (t.x < _Left ||  t.x > 1-_Right || t.y < _Bottom ||  t.y > 1-_Top) {
			c.a = 0;
		}
		return c;
	}
	ENDCG

	SubShader {
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
		Lighting Off
		ZWrite Off
		AlphaTest Off
		Blend Off
		
		Pass {
			CGPROGRAM
			  #pragma vertex vert
			  #pragma fragment frag
			ENDCG
		}
	}
	Fallback Off
}