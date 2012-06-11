uniform float4x4 xWorld;
uniform float4x4 xViewProjection;
uniform float4x4 xLightViewProjection;
uniform float4 xColor;
uniform float3 xLightPos;
uniform float3 xLightDir;
uniform float3 xCamPos;
uniform float3 xMousePos;
uniform float  xMouseRadio;

void VS_Basico(in float4 inPos : POSITION, 
	in float4 inColor: COLOR0, 
	out float4 outPos: POSITION, 
	out float4 outColor:COLOR0
	)
{
	float4 tmp = mul (inPos, xWorld);
	outPos = mul (tmp, xViewProjection);
	outColor = inColor;	
}

float4 PS_Basico(in float4 inColor:COLOR) :COLOR
{
	return inColor;
}



technique Lines
{
	pass Pass0
    {   
    	VertexShader = compile vs_2_0 VS_Basico();
        PixelShader  = compile ps_2_0 PS_Basico();
        FILLMODE = SOLID;
        CULLMODE = NONE;        
    }  
}
