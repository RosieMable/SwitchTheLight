Shader "Custom/SphericalMask"
{
    Properties 
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_ColorStrength("Color Strength", Range(1,4)) = 1
		_EmissionColor("Emission Color", Color) = (1,1,1,1)
		_EmissionTex("Emission (RGB)", 2D) = "white" {}
		_EmissionStrength("Emission Strength", Range(0,4)) = 1
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" } 
        LOD 200 

        CGPROGRAM 
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex, _EmissionTex; 

        struct Input 
        {
            float2 uv_MainTex; 
			float2 uv_EmissionTex;
			float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color, _EmissionColor;
		half _ColorStrength, _EmissionStrength;

		//Spherical Mask, it is uniform so it can be called from outside the shader
		uniform float4 Mask_Position;
		uniform half Mask_Radius;
		uniform half Mask_Softness;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			//Grayscale
			half grayscale = (c.r + c.g + c.b) * 0.333; //Always use multiplications instead of divisions, since they are faster to calculate
			fixed3 c_g = fixed3(grayscale,grayscale,grayscale);

			//Emission
			fixed4 e = tex2D(_EmissionTex, IN.uv_EmissionTex) * _EmissionColor * _EmissionStrength;



			//Spherical Mask
			half d = distance(Mask_Position, IN.worldPos);
			half sum = saturate((d - Mask_Radius) / -Mask_Softness); //Saturates the color inside of the mask (the color of the objects is shown only inside the circle)
			fixed4 lerpColor = lerp(fixed4(c_g, 1), c * _ColorStrength, sum);
			fixed4 lerpEmission = lerp(fixed4(0, 0, 0, 0), e, sum);


			//Assigning values to the object components
            o.Albedo = lerpColor.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
			o.Emission = lerpEmission.rgb;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
