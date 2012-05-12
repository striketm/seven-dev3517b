using UnityEngine;
using System.Collections;

/// <summary>
/// Script anexo ao jogador para detectar colisão do ovo nele
/// </summary>
public class EggCollider : MonoBehaviour
{
	/// <summary>
	/// Maneira de ter acesso à um outro código.
	/// </summary>
    PlayerScript playerScript;
	
	/// <summary>
	/// Chamado na criação/load do objeto
	/// </summary>
    void Awake()
    {
		//encontra o script do jogador
        playerScript = transform.parent.GetComponent<PlayerScript>();
    }
	
	/// <summary>
	/// Chama o evneto de colisão
	/// </summary>
	/// <param name='collider'>
	/// Em quem colide.
	/// </param>
	void OnTriggerEnter(Collider collider)
    {
        //"separa" o game object
        GameObject collisionGO = collider.gameObject;
		//e destrói
        Destroy(collisionGO);
		//e aumenta os pontos do jogador...
        playerScript.score++;
    }
}
