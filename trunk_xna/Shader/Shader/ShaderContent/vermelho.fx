float4x4 World;
float4x4 View;
float4x4 Projection;

// TODO: add effect parameters here.


/*

 ESSE AQUI � UM SHADER B�SICO QUE PODE SER FACILMENTE CRIADO
 CLICANDO COM O BOT�O DIREITO DO MOUSE SOBRE O "CONTENT"  E EM SEGUIDA SELECIONAR "ADD / NEW ITEM"
 
 FEITO ISSO SER� ABERTA UMA CAIXA DE DI�LOGO E EM SEGUIDA SELECIONE A OP��O "EFFECT FILE" E DEPOIS D� UM
 NOME PARA ELE E CLIQUE EM "ADD".

 POR PADR�O, ELE J� CRIA UM SHADER QUE COLOCA QUALQUER PRIMITIVA NA COR VERMELHA. PARA CONFIRMAR ISSO BASTA IR
 NO M�TODO "PixelShaderFunction". L� TEMOS AS INSTRU��ES QUE FAZEM ISSO.

*/



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

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.

    return float4(1, 0, 0, 1);
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
