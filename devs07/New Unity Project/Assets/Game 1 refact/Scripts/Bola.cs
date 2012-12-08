using UnityEngine;
using System.Collections;

/// <summary>
/// Anexado �s bolas, para fazer elas ca�rem
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
