using UnityEngine;
using System.Collections;

/// <summary>
/// Este script deve estar no objeto ovo.
/// </summary>
public class EggScript : MonoBehaviour
{
	/// <summary>
	/// Atualiza o ovo
	/// </summary>
	void Update () 
	{
		//faz ele cair, ele cai mais rápido com o tempo
        float fallSpeed = 2 * Time.deltaTime;
        transform.position -= new Vector3(0, fallSpeed, 0);
		
		//se estiver muito baixo destrói... 
        if (transform.position.y < -1 || transform.position.y >= 20)
        {
            Destroy(gameObject);
        }
	}
}
