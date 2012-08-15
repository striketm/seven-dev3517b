
sampler samplerState;


float alpha = 0.0;


float4 Pixel_Shader( float2 texCoord: TEXCOORD0 ) : COLOR
{

    float4 color = tex2D( samplerState, texCoord );
	
	color.r = color.r - alpha;
	color.g = color.g - alpha;
	color.b = color.b - alpha;
	
	return color;
	
}

technique RenderScene
{
    pass p0
    {
        PixelShader = compile ps_2_0 Pixel_Shader();
    }
}

