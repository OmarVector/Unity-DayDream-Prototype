// Copyright 2016 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

Shader "GoogleVR/Unlit/Texture Overlay" {
  Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _EmissiveTex("Glow Map",2D) = "white" {}
     [HDR]_ColorEmissive ("Tint", Color) = (1,1,1,1)
  }

  SubShader {
    Tags { "Queue"="Overlay+100" "RenderType"="Opaque" }
    LOD 100

    Pass {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma target 2.0
      #pragma multi_compile_fog

      #include "UnityCG.cginc"

      struct appdata_t {
        float4 vertex : POSITION;
        float2 texcoord : TEXCOORD0;
      };

      struct v2f {
        float4 vertex : SV_POSITION;
        half2 texcoord : TEXCOORD0;
        UNITY_FOG_COORDS(1)
      };

      sampler2D _MainTex;
      sampler2D _EmissiveTex;
     
      float4 _MainTex_ST;      
      float4 _ColorEmissive;

      v2f vert (appdata_t v) {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex );
        
        UNITY_TRANSFER_FOG(o,o.vertex);
        return o;
      }

      fixed4 frag (v2f i) : SV_Target {
        fixed4 col = tex2D(_MainTex, i.texcoord);
        fixed4 col2 = tex2D(_EmissiveTex, i.texcoord) * _ColorEmissive;
        
        fixed4 col3 = col + col2;
        UNITY_APPLY_FOG(i.fogCoord, col3);
        UNITY_OPAQUE_ALPHA(col3.a);
        return col3;
      }
      ENDCG
    }
  }
}
