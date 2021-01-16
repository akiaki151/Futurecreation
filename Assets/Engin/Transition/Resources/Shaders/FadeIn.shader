Shader "Unlit/FadeIn"
{
	Properties
	{
		_Color("Tint", Color) = (1,1,1,1)
		_Alpha("Time", Range(0, 1)) = 0
		_MainTex("Texture", 2D) = "white" {}
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}
		
		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Fog{ Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"
		
		struct Input
		{
			float4 vertex   : POSITION;
			float2 texcoord : TEXCOORD0;
		};
		
		struct v_2D
		{
			float4 vertex   : SV_POSITION;
			half2 texcoord  : TEXCOORD0;
		};
		
		fixed4 _Color;
		fixed _Alpha;
		sampler2D _MainTex;

		v_2D vert(Input IN)
		{
				v_2D OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
	#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw - 1.0) * float2(-1,1);
	#endif
				return OUT;
			}
			
			fixed4 frag(v_2D IN) : SV_Target
			{
				//_Color = tex2D(_MainTex, IN.texcoord);
				half alpha = tex2D(_MainTex, IN.texcoord).a;
				alpha = saturate(alpha + (_Alpha * 2 - 1));
				return fixed4(_Color.r, _Color.g, _Color.b, alpha);
			}

			ENDCG
		}
	}

	FallBack "Default"
}