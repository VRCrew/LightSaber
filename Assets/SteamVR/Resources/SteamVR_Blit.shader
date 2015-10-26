﻿Shader "Custom/SteamVR_Blit" {
	Properties { _MainTex ("Base (RGB)", 2D) = "white" {} }

	CGINCLUDE

	#include "UnityCG.cginc"

	sampler2D _MainTex;

	struct v2f {
		float4 pos : SV_POSITION;
		float2 tex : TEXCOORD0;
	};

	v2f vert(appdata_base v) {
		v2f o;
		o.pos = v.vertex;
		o.tex = v.texcoord;
		return o;
	}

	float4 frag(v2f i) : COLOR {
		return tex2D(_MainTex, i.tex);
	}

	float4 frag_hdr(v2f i) : COLOR {
		return pow(tex2D(_MainTex, i.tex), 1.0 / 2.2);
	}

	ENDCG

	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode Off }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode Off }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag_hdr
			ENDCG
		}
	}
}
