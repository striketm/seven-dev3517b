//nao tinha...

float4x4 World;
float4x4 View;
float4x4 Projection;

// Texture
sampler TextureSampler : register(s0);
sampler NormalSampler  : register(s1);

// Extern parameters
float offsetX;
float offsetY;

//SHADER!

struct VertexShaderInput
{
    float4 Position : POSITION0;

    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    // TODO: add your vertex shader code here.

    return output;
}

//SHADER! end

// Main method PIXEL!
float4 main(float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
//float4 main(VertexShaderOutput input) : COLOR0
{
	float2 NormalTexCoord = texCoord;
	NormalTexCoord.x += offsetX;
	if (NormalTexCoord.x > 1.0f)
		NormalTexCoord.x -= 1.0f;
	NormalTexCoord.y += offsetY;
	if (NormalTexCoord.y > 1.0f)
		NormalTexCoord.y -= 1.0f;

	float2 RealCoord = (float3(texCoord, 0) + tex2D(NormalSampler, NormalTexCoord).xyz / 100.0f).xy;
    return tex2D(TextureSampler, RealCoord) * color;
}


// Techniques //Test
technique Technique1
{
    pass Pass1
    {
	VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 main();
    }
}
