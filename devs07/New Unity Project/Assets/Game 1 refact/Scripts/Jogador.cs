using UnityEngine;
using System.Collections;

/// <summary>
/// Anexado ao jogador (junto com colisor)
/// </summary>
public class Jogador : MonoBehaviour
{
    
    
    void Update () 
    {
        float movimento = Input.GetAxis("Horizontal") * Time.deltaTime * 3; 
        
        transform.position += new Vector3(movimento, 0, 0);

        if (transform.position.x <= -2.5f || transform.position.x >= 2.5f)
        {
            float posicaoXLateral = Mathf.Clamp(transform.position.x, -2.5f, 2.5f);
            transform.position = new Vector3(posicaoXLateral, transform.position.y, transform.position.z);
        }
	}
           
    
}
