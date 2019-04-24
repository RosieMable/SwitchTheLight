//Dissolve Shader, this shader is supposed to make Meshes dissolve in the pattern of a texture. This is a surface shader.

Shader "Custom/DissolveSHader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        [HDR] _Emission("Emission", Color) = (1,1,1,1)

        //Properties for the dissolve effect
        _DissolveTex ("Dissolve Texture", 2D) = "white" {} //Texture that is going to be the pattern used to dissolve the mesh
      /*  _DissolveAmount("Dissolve Amount", Range(0,1)) = 0.5 *///Slider to indicate how much the mesh should be dissolving


        //Because the isVisible variable slowly approaches zero, we can predict when we’re about to discard a pixel. A common effect is to make that border, just before we discard the pixel, glow.
        [Header(Glow)]
        [HDR] _GlowColor("Glow Color", Color) = (1,1,1,1)
        _GlowRange("Glow Range", Range(0, .5)) = 0.1
        _GlowFallOff("Glow FallOff", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        //Reference to the dissolve texture
        sampler2D _DissolveTex;

        //Reference to the slider
       uniform float _DissolveAmount;
        float3 _GlowColor;
        float _GlowRange;
        float _GlowFallOff;

        struct Input
        {
            float2 uv_MainTex;
            
            //To get the uv coodinates of the dissolve texture
            float uv_DissolveTex;
        };

        fixed4 _Color;
        half3 _Emission;

        void surf (Input IN, inout SurfaceOutput o)
        {
            //Dissolve function
            float dissolve = tex2D(_DissolveTex, IN.uv_MainTex).r;
            dissolve = dissolve * 0.999;
            float MeshVisible = dissolve - _DissolveAmount;
            clip(MeshVisible);
            

            //Glowing edges
            float GlowingEdges = smoothstep(_GlowRange + _GlowFallOff, _GlowRange, MeshVisible);
            float3 glow = GlowingEdges * _GlowColor;

            fixed4 col = tex2D(_MainTex, IN.uv_MainTex);
            col *= _Color;

            o.Albedo = col;
            o.Emission = _Emission + glow;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
