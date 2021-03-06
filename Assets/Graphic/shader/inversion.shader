﻿Shader "Unlit/inversion"
{
	Properties{
	  _FaceColor("Face Color", Color) = (1,1,1,1)
	  _FaceDilate("Face Dilate", Range(-1,1)) = 0

	  _OutlineColor("Outline Color", Color) = (0,0,0,1)
	  _OutlineWidth("Outline Thickness", Range(0,1)) = 0
	  _OutlineSoftness("Outline Softness", Range(0,1)) = 0

	  _UnderlayColor("Border Color", Color) = (0,0,0,.5)
	  _UnderlayOffsetX("Border OffsetX", Range(-1,1)) = 0
	  _UnderlayOffsetY("Border OffsetY", Range(-1,1)) = 0
	  _UnderlayDilate("Border Dilate", Range(-1,1)) = 0
	  _UnderlaySoftness("Border Softness", Range(0,1)) = 0

	  _WeightNormal("Weight Normal", float) = 0
	  _WeightBold("Weight Bold", float) = .5

	  _ShaderFlags("Flags", float) = 0
	  _ScaleRatioA("Scale RatioA", float) = 1
	  _ScaleRatioB("Scale RatioB", float) = 1
	  _ScaleRatioC("Scale RatioC", float) = 1

	  _MainTex("Font Atlas", 2D) = "white" {}
	  _TextureWidth("Texture Width", float) = 512
	  _TextureHeight("Texture Height", float) = 512
	  _GradientScale("Gradient Scale", float) = 5
	  _ScaleX("Scale X", float) = 1
	  _ScaleY("Scale Y", float) = 1
	  _PerspectiveFilter("Perspective Correction", Range(0, 1)) = 0.875

	  _VertexOffsetX("Vertex OffsetX", float) = 0
	  _VertexOffsetY("Vertex OffsetY", float) = 0

	  _ClipRect("Clip Rect", vector) = (-32767, -32767, 32767, 32767)
	  _MaskSoftnessX("Mask SoftnessX", float) = 0
	  _MaskSoftnessY("Mask SoftnessY", float) = 0

	  _StencilComp("Stencil Comparison", Float) = 8
	  _Stencil("Stencil ID", Float) = 0
	  _StencilOp("Stencil Operation", Float) = 0
	  _StencilWriteMask("Stencil Write Mask", Float) = 255
	  _StencilReadMask("Stencil Read Mask", Float) = 255

	  _ColorMask("Color Mask", Float) = 15
	}

		SubShader{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
			}


			Stencil
			{
				Ref[_Stencil]
				Comp[_StencilComp]
				Pass[_StencilOp]
				ReadMask[_StencilReadMask]
				WriteMask[_StencilWriteMask]
			}

			Cull[_CullMode]
			ZWrite Off
			Lighting Off
			Fog { Mode Off }
			ZTest[unity_GUIZTestMode]
			Blend One OneMinusSrcAlpha
			ColorMask[_ColorMask]

			Pass {
				CGPROGRAM
				#pragma vertex VertShader
				#pragma fragment PixShader
				#pragma shader_feature __ OUTLINE_ON
				#pragma shader_feature __ UNDERLAY_ON UNDERLAY_INNER

				#pragma multi_compile __ UNITY_UI_CLIP_RECT
				#pragma multi_compile __ UNITY_UI_ALPHACLIP

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"
				#include "Assets/TextMesh Pro/Shaders/TMPro_Properties.cginc"

				#define PI 3.14159265358979

				struct vertex_t {
					float4	vertex			: POSITION;
					float3	normal			: NORMAL;
					fixed4	color : COLOR;
					float2	texcoord0		: TEXCOORD0;
					float2	texcoord1		: TEXCOORD1;
				};

				struct pixel_t {
					float4	vertex			: SV_POSITION;
					fixed4	faceColor : COLOR;
					fixed4	outlineColor : COLOR1;
					float4	texcoord0		: TEXCOORD0;			// Texture UV, Mask UV
					half4	param			: TEXCOORD1;			// Scale(x), BiasIn(y), BiasOut(z), Bias(w)
					half4	mask			: TEXCOORD2;			// Position in clip space(xy), Softness(zw)
					half4  texcoord2		: TEXCOORD5;
				};

				pixel_t VertShader(vertex_t input)
				{
					pixel_t output;

					UNITY_INITIALIZE_OUTPUT(pixel_t, output);
					UNITY_SETUP_INSTANCE_ID(input);
					UNITY_TRANSFER_INSTANCE_ID(input, output);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

					float bold = step(input.texcoord1.y, 0);

					float4 vert = input.vertex;
					vert.x += _VertexOffsetX;
					vert.y += _VertexOffsetY;
					float4 vPosition = UnityObjectToClipPos(vert);

					float2 pixelSize = vPosition.w;
					pixelSize /= float2(_ScaleX, _ScaleY) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));

					float scale = rsqrt(dot(pixelSize, pixelSize));
					scale *= abs(input.texcoord1.y) * _GradientScale * (_Sharpness + 1);
					if (UNITY_MATRIX_P[3][3] == 0) scale = lerp(abs(scale) * (1 - _PerspectiveFilter), scale, abs(dot(UnityObjectToWorldNormal(input.normal.xyz), normalize(WorldSpaceViewDir(vert)))));

					float weight = lerp(_WeightNormal, _WeightBold, bold) / 4.0;
					weight = (weight + _FaceDilate) * _ScaleRatioA * 0.5;

					float layerScale = scale;

					scale /= 1 + (_OutlineSoftness * _ScaleRatioA * scale);
					float bias = (0.5 - weight) * scale - 0.5;
					float outline = _OutlineWidth * _ScaleRatioA * 0.5 * scale;

					float opacity = input.color.a;

					fixed4 faceColor = fixed4(input.color.rgb, opacity) * _FaceColor;
					faceColor.rgb *= faceColor.a;

					fixed4 outlineColor = _OutlineColor;
					outlineColor.a *= opacity;
					outlineColor.rgb *= outlineColor.a;
					outlineColor = lerp(faceColor, outlineColor, sqrt(min(1.0, (outline * 2))));

					// Generate UV for the Masking Texture
					float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
					float2 maskUV = (vert.xy - clampedRect.xy) / (clampedRect.zw - clampedRect.xy);

					// Populate structure for pixel shader
					output.vertex = vPosition;
					output.faceColor = faceColor;
					output.outlineColor = outlineColor;
					output.texcoord0 = float4(input.texcoord0.x, input.texcoord0.y, maskUV.x, maskUV.y);
					output.param = half4(scale, bias - outline, bias + outline, bias);
					output.mask = half4(vert.xy * 2 - clampedRect.xy - clampedRect.zw, 0.25 / (0.25 * half2(_MaskSoftnessX, _MaskSoftnessY) + pixelSize.xy));

					return output;
				}


				// PIXEL SHADER
				fixed4 PixShader(pixel_t input) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID(input);

					half d = tex2D(_MainTex, input.texcoord0.xy).a * input.param.x;
					half4 c = input.faceColor * saturate(-d + input.param.w);
					//c.rg = input.mask.xy;
					/*if (c.r >= 0.9&&c.r <= 1.0)
					{
						c.r = 1.0;
						c.g = 0.0f;
						c.b = 0.0;
					}

					if (c.r >= -10.&&c.r <=- 5.0)
					{
						c.r = 1.0;
						c.g = 0.0f;
						c.b = 0.0;
					}*/
					clip(c.a - 0.001);
					return c;
				}
					ENDCG
		}
	  }

		  CustomEditor "TMPro.EditorUtilities.TMP_SDFShaderGUI"
}