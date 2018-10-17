﻿Shader "Kaima/Shape/RisingSun" 
{
	Properties
	{
		_Num("Num", Float) = 2
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "Assets/_Libs/Tools.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			float _Num;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed3 col = 0;
				float2 pos = i.uv - 0.5; //[-0.5, 0.5], make (0,0) in the center

				float a = atan2(pos.y, pos.x) * _Num;

				float f = cos(a);
				float cir = Circle(float2(0.5, 0.5), 0.2, i.uv);

				fixed3 circleCol = cir * fixed3(1,0,0);
				fixed3 lineCol = (1 - cir) * ((1 - f) + f * fixed3(0.9,0.9,0));
				col = circleCol + lineCol;
				return fixed4(col, 1);
			}
			ENDCG
		}
	}
}
