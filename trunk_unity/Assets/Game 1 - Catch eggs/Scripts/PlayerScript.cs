using UnityEngine;
using System.Collections;

/// <summary>
/// Este script deve ser anexado ao jogador.
/// </summary>
public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// A pontuação do jogador
    /// </summary>
    public int score = 0;
	
	/// <summary>
	/// Atualização.
	/// </summary>
	void Update ()
	{
		//lê o eixo horizontal
        float moveInput = Input.GetAxis("Horizontal") * Time.deltaTime * 3; 
		//move o jogador segundo o valor lido no eixo X
        transform.position += new Vector3(moveInput, 0, 0);
		//evita que o jogador saia dos limites definidos
        if (transform.position.x <= -2.5f || transform.position.x >= 2.5f)
        {
			//testa a posição, corta e fixa nela
            float xPos = Mathf.Clamp(transform.position.x, -2.5f, 2.5f);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }
	}
	/// <summary>
	/// Chama o evento de GUI.
	/// </summary>
    void OnGUI()
    {
        //escreve a pontuação na tela
        GUILayout.Label("Pontos: " + score);
    }    
}
