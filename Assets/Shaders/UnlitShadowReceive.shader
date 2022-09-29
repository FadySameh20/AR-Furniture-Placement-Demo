// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UnlitShadows/UnlitShadowReceive" {  // Shader fo the AR plane to cast shadow on it
Properties{ _Color("Main Color", Color) = (1,1,1,1) 
			_MainTex("Base (RGB)", 2D) = "white" {}	
			_Cutoff("Cutout", Range(0,1)) = 0.5 
			//_AmbientIntensity("Ambient Intensity", Range(0,1)) = 1 
			_ShadowTransparency("Shadow Transparency", Range(0,1)) = 0.5
		}
SubShader{
	    //Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        //ZWrite Off
        //Blend SrcAlpha OneMinusSrcAlpha
        //Cull back 
	Pass{ Alphatest Greater[_Cutoff] SetTexture[_MainTex] }	
	//Pass{ Blend DstColor Zero Tags{ "LightMode" = "ForwardBase" }
	Pass{ Blend SrcAlpha OneMinusSrcAlpha Tags{ "LightMode" = "ForwardBase" "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	//Pass{ Blend  DstColor Zero  Tags{ "LightMode" = "ForwardBase" "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#pragma multi_compile_fwdbase
		#include "AutoLight.cginc"
		struct v2f {
			float4 pos : SV_POSITION; LIGHTING_COORDS(0,1) 
		};

		float _ShadowTransparency;
		float _AmbientIntensity;

		v2f vert(appdata_base v) 
		{
			v2f o; 
			o.pos = UnityObjectToClipPos(v.vertex);
			TRANSFER_VERTEX_TO_FRAGMENT(o);
			return o; 
		}

		fixed4 frag(v2f i) : COLOR
		{
			float attenuation = LIGHT_ATTENUATION(i);
			float4 finalColor = attenuation;
			finalColor.a = (1-finalColor.r) * _ShadowTransparency;
			return finalColor;
			//return attenuation;

		} 
		ENDCG 
	} 
} Fallback "Transparent/Cutout/VertexLit" } 