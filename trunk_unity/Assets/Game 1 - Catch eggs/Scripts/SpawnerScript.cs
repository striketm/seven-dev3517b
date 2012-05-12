using UnityEngine;
using System.Collections;

/// <summary>
/// Anexado em algo na tela para chamar os ovos
/// </summary>
public class SpawnerScript : MonoBehaviour
{
	/// <summary>
	/// O que é spawnado
	/// </summary>
    public Transform eggPrefab;
	
	/// <summary>
	/// Quando aparece o próximo
	/// </summary>
    private float nextEggTime = 0.0f;
	
	/// <summary>
	/// Taxa com que aparece
	/// </summary>
    private float spawnRate = 1.5f;
 	
	/// <summary>
	/// Atualiza... IA
	/// </summary>
	void Update () 
	{
		//se está na hora de aparecer
        if (nextEggTime < Time.time)
        {
			//aparece
            SpawnEgg();
			
			//quando vai aparecer um novo
            nextEggTime = Time.time + spawnRate;
			
			//faz aparecer mais rápido
            spawnRate *= 0.98f;
            spawnRate = Mathf.Clamp(spawnRate, 0.3f, 99f);
        }
	}
	
	/// <summary>
	/// Chama uma nova instância de ovo
	/// </summary>
    void SpawnEgg()
    {
		//aparece em um X aleatório
        float addXPos = Random.Range(-1.6f, 1.6f);
		//joga a posicao para esta sorteada
        Vector3 spawnPos = transform.position + new Vector3(addXPos,0,0);
		//instancia um novo prefab naquela posição, sem rotação
        Instantiate(eggPrefab, spawnPos, Quaternion.identity);
    }
}
