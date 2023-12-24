// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Class/01分割材质"
{
	Properties
	{
		[HDR]_TexColor("第一层贴图颜色", Color) = (0,0,0,0)
		_Tex01("第一层贴图", 2D) = "white" {}
		_Mask01("第一层遮罩", 2D) = "white" {}
		_MaskRotator("遮罩图旋转", Range( 0 , 1)) = 0
		_FenGeTex("分割图", 2D) = "white" {}
		_Distance("分割强度", Range( -1 , 1)) = 0.98
		_Asix("分割角度修复", Range( 0 , 3)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Custom"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _TexColor;
		uniform sampler2D _Tex01;
		uniform float4 _Tex01_ST;
		uniform sampler2D _FenGeTex;
		uniform float4 _FenGeTex_ST;
		uniform float _Distance;
		uniform float _Asix;
		uniform sampler2D _Mask01;
		uniform float4 _Mask01_ST;
		uniform float _MaskRotator;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_Tex01 = i.uv_texcoord * _Tex01_ST.xy + _Tex01_ST.zw;
			float2 uv_FenGeTex = i.uv_texcoord * _FenGeTex_ST.xy + _FenGeTex_ST.zw;
			float temp_output_47_0 = floor( tex2D( _FenGeTex, uv_FenGeTex ).r );
			float temp_output_41_0 = ( temp_output_47_0 * _Distance );
			float temp_output_76_0 = ( 1.0 - _Asix );
			float2 appendResult43 = (float2(temp_output_41_0 , ( temp_output_41_0 * temp_output_76_0 )));
			float temp_output_60_0 = ( ( 1.0 - temp_output_47_0 ) * _Distance );
			float2 appendResult58 = (float2(temp_output_60_0 , ( temp_output_60_0 * temp_output_76_0 )));
			float2 temp_output_61_0 = ( appendResult43 + ( appendResult58 * -1.0 ) );
			float4 tex2DNode1 = tex2D( _Tex01, ( uv_Tex01 + temp_output_61_0 ) );
			o.Emission = ( _TexColor * tex2DNode1 ).rgb;
			float2 uv_Mask01 = i.uv_texcoord * _Mask01_ST.xy + _Mask01_ST.zw;
			float cos6 = cos( ( ( _MaskRotator * UNITY_PI ) * 2.0 ) );
			float sin6 = sin( ( ( _MaskRotator * UNITY_PI ) * 2.0 ) );
			float2 rotator6 = mul( uv_Mask01 - float2( 0.5,0.5 ) , float2x2( cos6 , -sin6 , sin6 , cos6 )) + float2( 0.5,0.5 );
			o.Alpha = ( tex2DNode1.a * tex2D( _Mask01, ( rotator6 + temp_output_61_0 ) ).r );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18800
2816.604;72.45284;479.8113;593.9434;726.438;631.4524;1.549592;True;False
Node;AmplifyShaderEditor.SamplerNode;39;-2511.717,639.9547;Inherit;True;Property;_FenGeTex;分割图;5;0;Create;False;0;0;0;False;0;False;-1;0e33ce47bcb2bfa48b0472047bf74395;42eadad5f5986c54fbca6c85ca50eed4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FloorOpNode;47;-2204.213,666.2563;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;42;-2053.535,886.7675;Inherit;False;Property;_Distance;分割强度;6;0;Create;False;0;0;0;False;0;False;0.98;0.88;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;74;-1738.065,935.2177;Inherit;False;Property;_Asix;分割角度修复;7;0;Create;False;0;0;0;False;0;False;0;1.46;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;57;-2020.892,1056.648;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;-1722.009,1070.463;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;76;-1442.026,948.8229;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-1440.633,461.2463;Inherit;False;Property;_MaskRotator;遮罩图旋转;4;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-1741.159,670.7557;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-1278.423,1144.127;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-1241.652,778.1481;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;58;-1032.526,1059.363;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;63;-945.8991,1292.963;Inherit;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;0;False;0;False;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;9;-1129.4,466.0022;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;43;-1032.095,675.6152;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-1206.247,272.6218;Inherit;False;0;3;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-943.1577,453.7185;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;62;-796.8991,1092.963;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;61;-776.565,729.1761;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;6;-917.6523,276.7704;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;37;-916.3568,-131.0576;Inherit;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;69;-651.9856,-116.9923;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;44;-711.1461,277.5231;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;3;-419.0806,257.4156;Inherit;True;Property;_Mask01;第一层遮罩;3;0;Create;False;0;0;0;False;0;False;-1;None;49d8cbdd60a819d409d56c2449e7de9d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-467.2399,-159.1076;Inherit;True;Property;_Tex01;第一层贴图;2;0;Create;False;0;0;0;False;0;False;-1;98450da720ebbb24c882071e7643e8e3;98450da720ebbb24c882071e7643e8e3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;82;-346.9171,-383.3932;Inherit;False;Property;_TexColor;第一层贴图颜色;1;1;[HDR];Create;False;0;0;0;False;0;False;0,0,0,0;1,0.07903177,0.07903177,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;-8.453272,-164.3059;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-68.6322,241.4748;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;429.3766,-35.13236;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Class/01分割材质;False;False;False;False;True;True;True;True;True;True;True;True;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Custom;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;47;0;39;1
WireConnection;57;0;47;0
WireConnection;60;0;57;0
WireConnection;60;1;42;0
WireConnection;76;0;74;0
WireConnection;41;0;47;0
WireConnection;41;1;42;0
WireConnection;75;0;60;0
WireConnection;75;1;76;0
WireConnection;73;0;41;0
WireConnection;73;1;76;0
WireConnection;58;0;60;0
WireConnection;58;1;75;0
WireConnection;9;0;8;0
WireConnection;43;0;41;0
WireConnection;43;1;73;0
WireConnection;20;0;9;0
WireConnection;62;0;58;0
WireConnection;62;1;63;0
WireConnection;61;0;43;0
WireConnection;61;1;62;0
WireConnection;6;0;7;0
WireConnection;6;2;20;0
WireConnection;69;0;37;0
WireConnection;69;1;61;0
WireConnection;44;0;6;0
WireConnection;44;1;61;0
WireConnection;3;1;44;0
WireConnection;1;1;69;0
WireConnection;81;0;82;0
WireConnection;81;1;1;0
WireConnection;46;0;1;4
WireConnection;46;1;3;1
WireConnection;0;2;81;0
WireConnection;0;9;46;0
ASEEND*/
//CHKSM=ABCF02047F92A530EFAB728733C5F73897BEF2AB