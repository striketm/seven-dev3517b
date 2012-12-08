using UnityEngine;
using System.Collections;

/// <summary>
/// Anexado ao gerenciador na cena, que faz as bolas caírem
/// </summary>
public class Gerenciador : MonoBehaviour
{
    public Transform BolaPrefab;

    float tempoAparecerProximo = 0.0f;
 
    float taxaAparecimento = 1.5f;
 	
	void Update ()
    {
        if (tempoAparecerProximo < Time.time)
        {
            Criar();
            
            tempoAparecerProximo = Time.time + taxaAparecimento;

            taxaAparecimento *= 0.98f;

            taxaAparecimento = Mathf.Clamp(taxaAparecimento, 0.3f, 99f);
        }
	}

    void Criar()
    {
        float posicaoXLateral = Random.Range(-2.0f, 2.0f);

        Vector3 posicaoXCriacao = transform.position + new Vector3(posicaoXLateral, 10, 0);

        Instantiate(BolaPrefab, posicaoXCriacao, Quaternion.identity);
    }
}