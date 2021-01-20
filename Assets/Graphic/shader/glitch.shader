Shader "Unlit/glitch"
{

	Properties{
		_NowTime("NowTime", float) = 0.0
		_Duration("Duration", float) = 100000000

	  [Header(Glitch1)]
	  _GlitchSize1("Glitch Size",float) = 2.0
	  _Amplitude1("Amplitude", float) = 5


	  [Header(Glitch2)]
	  _GlitchSize2("Glitch Size", float) = 3.0
	  _Amplitude2("Amplitude", float) = 0.5


	  [Header(Glitch3)]
	  _GlitchSize3("Glitch Size", float) = 3.0
	  _Amplitude3("Amplitude", float) = 0.4


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

				 float _NowTime, _Duration;//C#から受け取る
				 float _WholeFrequency;
				float _GlitchSize1;
				float _GlitchSize2;
				float _GlitchSize3;
				float _Amplitude1;
				float _Amplitude2;
				float _Amplitude3;

				  fixed2 random2(fixed2 st);
				float perlinNoise(fixed2 st);
				//uv.yを受け取る
				float glitch1(float y);
				   float glitch2(float y);
			   float amplitudeStrength1(float2 uv);
				float amplitudeStrength2();

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


			   //関数定義
			 //-1~+1
			   fixed2 random2(fixed2 st)
			   {
				   st = fixed2(dot(st, fixed2(127.1, 311.7)),
					   dot(st, fixed2(269.5, 183.3)));
				   return -1.0 + 2.0 * frac(sin(st) * 43758.5453123);
			   }

			   float perlinNoise(fixed2 st)
			   {
				   fixed2 p = floor(st);
				   fixed2 f = frac(st);
				   fixed2 u = f * f * (3.0 - 2.0 * f);

				   float v00 = random2(p + fixed2(0, 0));
				   float v10 = random2(p + fixed2(1, 0));
				   float v01 = random2(p + fixed2(0, 1));
				   float v11 = random2(p + fixed2(1, 1));

				   return lerp(lerp(dot(v00, f - fixed2(0, 0)), dot(v10, f - fixed2(1, 0)), u.x),
					   lerp(dot(v01, f - fixed2(0, 1)), dot(v11, f - fixed2(1, 1)), u.x),
					   u.y) + 0.5f;
			   }

			   float glitch1(float y)
			   {
				   float time = _Time.y + 1000;
				   float amp = 2.0;
				   float syuki = 1.0;
				   float frequency1 = 13;
				   float frequency2 = 7;
				   return amp * sin(3 * (y + time * frequency1)) *
					   cos(7 * (y + time * frequency2)) *
					   sin(time);
			   }

			   float glitch2(float y)
			   {
				   y *= 3;
				   float t = _NowTime * 10;
				   float s1 = sin(9.5 * y + t * 1.5);
				   float c1 = cos(3.4 * y + t);
				   float base1 = sin(3 * y);
				   float base2 = sin(17 * y);
				   return base1 + base2 + s1 * c1;
			   }

			   float amplitudeStrength1(float2 uv)
			   {
				   float y = uv.y;
				   return sin(y * 300 + _Time.y * 2) *
					   cos(y * 170 + _Time.y) *
					   perlinNoise(uv * 20 + _Time.x);
			   }

			   float amplitudeStrength2()
			   {
				   float value;
				   float threshold = 0.99;
				   value = sin(_Time.y) * sin(_Time.y);
				   return step(threshold, value);
			   }

			   // PIXEL SHADER
			   fixed4 PixShader(pixel_t input) : SV_Target
			   {
				   UNITY_SETUP_INSTANCE_ID(input);

				   float2 uv = input.texcoord0;
				   float value1 = glitch1(uv.y);
				   float value2_1 = glitch2(uv.y);
				   float value2_2 = glitch2(uv.y * 2.8 + (_Time.y % 0.2) * 100); //0.1秒毎にオフセット量が変わってグリッチが素早く変化する
				   float threshold1 = _GlitchSize1;
				   float threshold2 = _GlitchSize2;
				   float glitch3Ypos = (random2((float2)_NowTime) * 0.5 + 0.5) * (1.0 - _GlitchSize3);//0~(1.0 - _GlitchSize3)

				   float glitch1Flag = step(abs(value1), threshold1);//グリッチする領域は1、しない領域は0。
				   float glitch2_1Flag = step(abs(value2_1), threshold2);//グリッチする領域は1、しない領域は0。
				   float glitch2_2Flag = step(abs(value2_2), threshold2 / 20.0);//グリッチする領域は1、しない領域は0。
				   float glitch3Flag = step(glitch3Ypos, uv.y) * step(uv.y, glitch3Ypos + _GlitchSize3);

				   int isPlaying1 = (_NowTime + (_Duration / 6.0 * 3.0) + _Duration > _Time.y) ? 1 : 0;
				   int isPlaying2_1 = (_NowTime + _Duration > _Time.y) ? 1 : 0;
				   int isPlaying2_2 = (_NowTime + _Duration > _Time.y) ? 1 : 0;
				   int isPlaying3 = (_NowTime + _Duration > _Time.y) ? 1 : 0;

				   // isPlaying1 = 1;
				   // isPlaying2_1 = 1;
				   // isPlaying2_2 = 1;
				   // isPlaying3 = 1;


				   //各グリッチ処理のグリッチのずれ量(amplitude)を格納
				   float x1 = uv.x;
				   float x2_1 = uv.x;
				   float x2_2 = uv.x;
				   float x3 = uv.x;
				   //グリッチ処理1
				  // x1 = isPlaying1 * glitch1Flag * _Amplitude1 * amplitudeStrength1(uv);
				   //グリッチ処理2
				   x2_1 = isPlaying2_1 * glitch2_1Flag * _Amplitude2 * sign(value2_1/10);
				   //グリッチ処理2.5(2のプロパティに依存した改造版)
				   x2_2 = isPlaying2_2 * glitch2_2Flag * _Amplitude2 * sign(value2_2/10);
				   //グリッチ処理3s
				   x3 = isPlaying3 * glitch3Flag * _Amplitude3;
				   uv.x += (1 - glitch3Flag * 2) * (x1 + x2_1 * 2 + x2_2 * 2) + glitch3Flag * (x3 + x2_2 * 0.4);

				   half d = tex2D(_MainTex, input.texcoord0 + float2(uv.x / 700 ,1.0)).a * input.param.x;
				   half4 c = input.faceColor * saturate(d - input.param.w);

				   clip(c.a - 0.001);
				   return c;
			   }
				   ENDCG
	   }
	 }

		 CustomEditor "TMPro.EditorUtilities.TMP_SDFShaderGUI"
}