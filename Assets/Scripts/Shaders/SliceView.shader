shader "BiodesignLab/SliceView"
{
    Properties
    {
        _VolumeTex("3D Volume Texture", 3D) = "" {}
    }

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler3D _VolumeTex;
            
            float4x4 _LocalToWorld;
            float4x4 _WorldToVolume; 

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float4 uvw = mul(_WorldToVolume, mul(_LocalToWorld, float4(i.uv.x, i.uv.y, 1.0f, 1.0f)));
                return tex3D(_VolumeTex, uvw.xyz);
            }

            ENDCG
        }
    }
}