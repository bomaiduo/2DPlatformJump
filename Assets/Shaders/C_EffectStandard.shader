Shader "effect/C_EffectStandard" {
	 Properties {
	 	//Diffuse第一层贴图
	 	[Enum(TwoSide,0,Off,2)] _TwoSide ("2-Side",float) = 2
		[KeywordEnum(OFF, ON)] _FogMode ("FogMode", float) = 1
		[HideInInspector] _ZWrite ("__zw", Float) = 1.0
		_AlphaCutout("_AlphaCutout",Range(0,1))=0
        _Diffuse ("第一层贴图", 2D) = "white" {}
        //是否可旋转
        [Toggle]_DiffuseRo("旋转",Float)=0
        _DiffuseAng ("旋转角度", float ) = 0
        [HDR]_Color ("颜色", Color) = (1,1,1,1)
        _Brightness("亮度(0-10)", Range(0,10)) = 1
        _Uspeed ("Uspeed", float ) = 0
        _Vspeed ("Vspeed", float ) = 0
        //DiffuseMask第一层贴图遮罩
        [Foldout] _DiffuseMaskShown ("", Float) = 1 
        [Toggle]_DiffuseMask("第一层遮罩",Float)=0
        _DiffuseMaskTex ("遮罩贴图", 2D) = "white" {}
        //是否可旋转
        [Toggle]_DiffuseMaskRo("旋转",Float)=0
        _DiffuseMaskAng ("旋转角度", float ) = 0
        _USpeed_diffusem ("USpeed", float ) = 0
        _VSpeed_diffusem ("VSpeed", float ) = 0
        //第二层贴图
        [Foldout] _SecondLayerShown ("", Float) = 1 
        [Toggle]_SecondLayerBlock("第二层",Float)=0
        _SecondLayerTex ("第二层贴图", 2D) = "white" {}
		_SecondLayerColor ("颜色", Color) = (1,1,1,1)
        _SecondLayerBrightness ("亮度(0-10)", Range(0,10)) = 1
        //是否可旋转
        [Toggle]_SecondLayerRo("旋转",Float)=0
        _SecondLayerAng ("旋转角度", float ) = 0
        _Uspeed_second ("Uspeed", float ) = 0
        _Vspeed_second ("Vspeed", float ) = 0
        //第二层贴图遮罩
        [Foldout] _SecondLayerMaskShown ("", Float) = 1 
        [Toggle]_SecondLayerMask("第二层遮罩",Float)=0
        _SecondLayerMaskTex ("遮罩贴图", 2D) = "white" {}
        //是否可旋转
        [Toggle]_SecondLayerMaskRo("旋转",Float)=0
        _SecondLayerMaskAng ("旋转角度", float ) = 0
        _Uspeed_secondm ("USpeed", float ) = 0
        _Vspeed_secondm ("VSpeed", float ) = 0
        //溶解贴图
        [Foldout] _DissolveShown ("", Float) = 1 
        [Toggle]_DissolveBlock("溶解",Float)=0              
        _DissolveTex ("溶解贴图", 2D) = "white" {}
        _DissolveColor("溶解勾边颜色", Color) = (1,1,1,1)
		_Dissolve("溶解度", float) = 0
		_DissolveSize ("溶解勾边大小", Range(0.0, 1.0)) = 0.15
		_DissolveBrightness("溶解勾边亮度(0-10)", Range(0,10)) = 1
		//溶解贴图遮罩
		[Foldout] _DissolveMaskShown ("", Float) = 1 
		[Toggle]_DissolveMask("溶解遮罩",Float)=0
		_DissolveMaskTex ("遮罩贴图", 2D) = "white" {}
		//扭曲贴图
		[Foldout] _DistortShown ("", Float) = 1 
		[Toggle]_DistortBlock("扭曲",Float)=0
		_DistortTex("扭曲贴图",2D )="white" {}
		//_DistortColor("Distort Color", Color) = (1,1,1,1)
		_ForceX  ("强度 X(0 1)", float) = 0.1
		_ForceY  ("强度 Y(0 1)", float) = 0.1
		_USpeed_distort  ("USpeed", float) = 0
		_VSpeed_distort  ("VSpeed", float) = 0
		//扭曲贴图遮罩
		[Foldout] _DistortMaskShown ("", Float) = 1 
		[Toggle]_DistortMask("扭曲遮罩",Float)=0
		_DistortMaskTex ("遮罩贴图", 2D) = "white" {}


        

		[HideInInspector]_BlendSet("__mode", Float) = 1
		[HideInInspector]_SrcRGBMode("__src_rgb",Float) = 5
		[HideInInspector]_SrcAlphaMode("__src_alpha",Float) = 5
		[HideInInspector]_DestRGBMode("__dst_rgb",Float) = 10
		[HideInInspector]_DestAlphaMode("__dst_alpha",Float) = 10
		[HideInInspector]_cpfv("cpfv", Int) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
			"ArtType"="Effect"
        }
        
        Pass {
            Lighting Off
			Blend[_SrcRGBMode][_DestRGBMode],[_SrcAlphaMode][_DestAlphaMode]
			Cull [_TwoSide]
            ZWrite [_ZWrite]
			Stencil {
                Ref 2
                Comp [_cpfv]
            }           
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature _FOGMODE_ON _FOGMODE_OFF
			#pragma shader_feature FOG_LINEAR
			#pragma shader_feature _ALPHATEST_ON
			#pragma fragmentoption ARB_precision_hint_fastest

   //         #ifdef _FOGMODE_OFF
			//#undef FOG_LINEAR
			//#endif

			#pragma  shader_feature  _DIFFUSERO_ON
			#pragma  shader_feature  _DIFFUSEMASK_ON
			#pragma  shader_feature  _DIFFUSEMASKRO_ON
			#pragma  shader_feature  _SECONDLAYERBLOCK_ON
			#pragma  shader_feature  _SECONDLAYERRO_ON
			#pragma  shader_feature  _SECONDLAYERMASK_ON
			#pragma  shader_feature  _SECONDLAYERMASKRO_ON
			#pragma  shader_feature  _DISSOLVEBLOCK_ON
			#pragma  shader_feature  _DISSOLVEMASK_ON
			#pragma  shader_feature  _DISTORTBLOCK_ON 
			#pragma  shader_feature  _DISTORTMASK_ON 

            #include "UnityCG.cginc"
			//#define FORCE_PIXEL_FOG
			//#include "../Common/ShaderFixedCommon.cginc"

            uniform sampler2D _Diffuse;
			uniform half4 _Diffuse_ST;
			uniform half4 _Color;

			uniform half _DiffuseAng;
			uniform half _Brightness;
			uniform half _Uspeed;
            uniform half _Vspeed;
           
            #if _ALPHATEST_ON
            uniform half _AlphaCutout;
            #endif
            #if _DIFFUSEMASK_ON
            uniform sampler2D _DiffuseMaskTex;
			uniform half4 _DiffuseMaskTex_ST;
			uniform half _DiffuseMaskAng;
            uniform half _USpeed_diffusem;
            uniform half _VSpeed_diffusem;
            #endif

            #if _DISSOLVEBLOCK_ON
            uniform sampler2D _DissolveTex;
			uniform half4 _DissolveTex_ST;
			uniform half4 _DissolveColor;
			uniform half _Dissolve;
			uniform half _DissolveSize;
			uniform half _DissolveBrightness;
			#endif

			#if _DISSOLVEMASK_ON
			uniform sampler2D _DissolveMaskTex;
			#endif
			
			#if _SECONDLAYERBLOCK_ON
            uniform sampler2D _SecondLayerTex; 
			uniform half4 _SecondLayerTex_ST;
            uniform half4 _SecondLayerColor;
            uniform half _SecondLayerAng;
			uniform half _SecondLayerBrightness;
			uniform half _Uspeed_second;
            uniform half _Vspeed_second;
            #endif

            #if _SECONDLAYERMASK_ON
            uniform sampler2D _SecondLayerMaskTex;
			uniform half4 _SecondLayerMaskTex_ST;
			uniform half _SecondLayerMaskAng;
            uniform half _Uspeed_secondm;
            uniform half _Vspeed_secondm;
            #endif

            #if _DISTORTBLOCK_ON 
            uniform sampler2D _DistortTex;
            uniform half4 _DistortTex_ST;
            uniform half _ForceX;
            uniform half _ForceY;
            uniform half _USpeed_distort;
            uniform half _VSpeed_distort;
            #endif
            
            #if _DISTORTMASK_ON
            uniform sampler2D _DistortMaskTex;
            #endif

            struct VertexInput {
                float4 vertex : POSITION;
                half2 texcoord0 : TEXCOORD0;
				half4 vertexColor : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            struct VertexOutput {
                float4 vertex : SV_POSITION;
				float2 uv0 : TEXCOORD0;
				#if _DIFFUSEMASK_ON
				float2 uv1 :TEXCOORD1;
				#endif
				#if _SECONDLAYERBLOCK_ON
				float2 uv2 :TEXCOORD2;
				#endif
				#if _SECONDLAYERBLOCK_ON && _SECONDLAYERMASK_ON
				float2 uv3:TEXCOORD3;
				#endif
				#if _DISSOLVEBLOCK_ON
				half2 uv4:TEXCOORD4;
				#endif
				#if _DISTORTBLOCK_ON
				float2 uv5:TEXCOORD5;
				#endif
				half4 vertexColor : COLOR;

				UNITY_FOG_COORDS(6)
				UNITY_VERTEX_OUTPUT_STEREO
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.vertexColor = v.vertexColor;
				//uv计算
				#if _DIFFUSERO_ON
				half DiffuseAng_cos;
				half DiffuseAng_sin;
				sincos(_DiffuseAng*0.0174, DiffuseAng_sin, DiffuseAng_cos);
				half2 panner0=mul(v.texcoord0- half2(0.5,0.5),half2x2( DiffuseAng_cos, -DiffuseAng_sin, DiffuseAng_sin, DiffuseAng_cos))+ half2(0.5,0.5)+_Time.y*float2(_Uspeed, _Vspeed);
				#else
				float2 panner0=v.texcoord0+_Time.y*float2(_Uspeed, _Vspeed);
				#endif
				o.uv0 = TRANSFORM_TEX(panner0, _Diffuse);

				#if _DIFFUSEMASK_ON
				#if _DIFFUSEMASKRO_ON
				half DiffuseMaskAng_cos;
				half DiffuseMaskAng_sin;
				sincos(0.0174*_DiffuseMaskAng, DiffuseMaskAng_sin, DiffuseMaskAng_cos);
				//uv旋转前将旋转中心移到uv中心
				half2 panner1=mul(v.texcoord0- half2(0.5,0.5),half2x2( DiffuseMaskAng_cos, -DiffuseMaskAng_sin, DiffuseMaskAng_sin, DiffuseMaskAng_cos))+ half2(0.5,0.5)+_Time.y*float2(_USpeed_diffusem, _VSpeed_diffusem);
				#else
				float2 panner1=v.texcoord0+_Time.y*float2(_USpeed_diffusem, _VSpeed_diffusem);
				#endif
				o.uv1 = TRANSFORM_TEX(panner1, _DiffuseMaskTex);
				#endif
				
				#if _SECONDLAYERBLOCK_ON 
				#if _SECONDLAYERRO_ON
				half FlowAng_cos;
				half FlowAng_sin;
				sincos(_SecondLayerAng*0.0174, FlowAng_sin, FlowAng_cos);
				half2 panner2= mul(v.texcoord0- half2(0.5,0.5),half2x2( FlowAng_cos, -FlowAng_sin, FlowAng_sin, FlowAng_cos))+ half2(0.5,0.5)+_Time.y*float2(_Uspeed_second, _Vspeed_second);
				#else
				float2 panner2=v.texcoord0+_Time.y*float2(_Uspeed_second, _Vspeed_second);
				#endif
				o.uv2 = TRANSFORM_TEX(panner2, _SecondLayerTex);
				#endif

				#if _SECONDLAYERBLOCK_ON && _SECONDLAYERMASK_ON
				#if _SECONDLAYERMASKRO_ON
				half FlowMaskAng_cos;
				half FlowMaskAng_sin;
				sincos(_SecondLayerMaskAng*0.0174, FlowMaskAng_sin, FlowMaskAng_cos);
				half2 panner3=mul(v.texcoord0- half2(0.5,0.5),half2x2( FlowMaskAng_cos, -FlowMaskAng_sin, FlowMaskAng_sin, FlowMaskAng_cos))+ half2(0.5,0.5)+_Time.y*float2(_Uspeed_secondm, _Vspeed_secondm);
				#else
				float2 panner3=v.texcoord0+_Time.y*float2(_Uspeed_secondm, _Vspeed_secondm);
				#endif
				o.uv3 = TRANSFORM_TEX(panner3, _SecondLayerMaskTex);
				#endif
				//#if _FLOWMAPBLOCK_ON
				//float3 flowDir = tex2D(_FlowMap, IN.texcoord) * 2.0f - 1.0f;
				//flowDir *= _FlowSpeed;
				//float panner6 = frac(_Time[1] * 0.5f + 0.5f);
				//float panner7 = frac(_Time[1] * 0.5f + 1.0f);
				//float flowLerp = abs((0.5f - panner6) / 0.5f);

				#if _DISSOLVEBLOCK_ON
				//half2 panner4=v.texcoord0+_Time.y*float2(_USpeed_distort, _Vspeed_distort);
				o.uv4= TRANSFORM_TEX(v.texcoord0, _DissolveTex);
				#endif

				#if _DISTORTBLOCK_ON
				float2 panner5=v.texcoord0+_Time.y*float2(_USpeed_distort, _VSpeed_distort);
				o.uv5= TRANSFORM_TEX(panner5, _DistortTex);
				#endif

				UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            half4 frag(VertexOutput i) : COLOR {  
            	
				#if _DISTORTBLOCK_ON 
				half4 _Distort_var = tex2D(_DistortTex,i.uv5);
				#if _DISTORTMASK_ON
				half4 _DistortMaskTex_var = tex2D(_DistortMaskTex,i.uv5);
				i.uv0+=float2(_Distort_var.r*_ForceX*_DistortMaskTex_var.a,_Distort_var.r*_ForceY*_DistortMaskTex_var.a);
				#else
				i.uv0+=float2(_Distort_var.r*_ForceX*_Distort_var.a,_Distort_var.r*_ForceY*_Distort_var.a);
				#endif
				#endif
				//颜色计算
				half4 _Diffuse_var = tex2D(_Diffuse,i.uv0);	
				half4 col=i.vertexColor* _Diffuse_var * _Color * _Brightness;
				
				#if _ALPHATEST_ON
				clip(col.a-_AlphaCutout);
				#endif

				#if _DIFFUSEMASK_ON
				half4 _DiffuseMaskTex_var = tex2D(_DiffuseMaskTex,i.uv1);
				col=half4((col.rgb),(col.a*_DiffuseMaskTex_var.a));
				#endif

				#if _SECONDLAYERBLOCK_ON
				half4 _SecondLayerTex_var= tex2D(_SecondLayerTex,i.uv2);
				#if _SECONDLAYERMASK_ON
				half4 _SecondLayerMaskTex_var= tex2D(_SecondLayerMaskTex,i.uv3);
				col=lerp(col,col+i.vertexColor*_SecondLayerTex_var*_SecondLayerColor * _SecondLayerBrightness,_SecondLayerTex_var.a*_SecondLayerMaskTex_var.a);
				#else
				col=lerp(col,col+i.vertexColor*_SecondLayerTex_var*_SecondLayerColor * _SecondLayerBrightness,_SecondLayerTex_var.a);
				#endif
				#endif

				#if _DISSOLVEBLOCK_ON

				half4 _Dissolve_var = tex2D(_DissolveTex,i.uv4);
				
				half dissolve = _Dissolve_var.r - _Dissolve;
				
				col.rgb=lerp(col.rgb,col.rgb*_DissolveColor.rgb*_DissolveBrightness,sign(_DissolveSize-dissolve)*0.5+0.5);
				col.a=lerp(col.a,col.a*_DissolveColor.a,clamp((_DissolveSize-dissolve)*10,0,1 ));
   				//if(dissolve < _DissolveSize)
   				//col=col*_DissolveColor*_DissolveBrightness;
   			    clip(dissolve);
   			    #endif

   			    #if _DISSOLVEBLOCK_ON && _DISSOLVEMASK_ON
   			    half4 _DissolveMaskTex_var = tex2D(_DissolveMaskTex,i.uv4);
   			    dissolve = _Dissolve_var - _Dissolve+(1-_DissolveMaskTex_var.a);
   				if(dissolve < _DissolveSize)
   				col=col*_DissolveColor*_DissolveBrightness;
   			    clip(dissolve);   
   			    #endif

				UNITY_APPLY_FOG(i.fogCoord,col);
                return col;
            }
            ENDHLSL
        }
    }
    
    //FallBack "Legacy Shaders/Transparent/Diffuse"
	FallBack "Hidden/Universal Render Pipeline/FallbackError"
	CustomEditor "CustomEffectMatGUI"
}
