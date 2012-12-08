using UnityEngine;
using System.Collections;

/// <summary>
/// Anexado às bolas, para fazer elas caírem
/// </summary>
public class Bola : MonoBehaviour
{    
	void Update () 
    {
        float velocidadeQueda = 2 * Time.deltaTime;

        transform.position -= new Vector3(0, velocidadeQueda, 0);

        if (transform.position.y < -1 || transform.position.y >= 20)
        {
            Destroy(gameObject);
        }
	}
}
