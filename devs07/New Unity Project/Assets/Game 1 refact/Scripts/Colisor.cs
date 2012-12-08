using UnityEngine;
using System.Collections;

/// <summary>
/// Anexado ao jogador, para fazer colidir com as bolas
/// </summary>
public class Colisor : MonoBehaviour 
{
    //Jogador jogador;

    /// <summary>
    /// Deve ser pública para ser visível de Colisor
    /// </summary>
    public int pontos;

    void Awake()
    {
        pontos = 10;
        //jogador = transform.parent.GetComponent<Jogador>();
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("fuu!!!");

        if (collider.gameObject.tag == "bola")
        {
            Debug.Log("foi");

            GameObject gameObject = collider.gameObject;

            Destroy(gameObject);

            pontos++;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("fooooooooooooo!!!");
        //nada...

        if (collision.gameObject.tag == "bola")
        {
            Debug.Log("foi");

            GameObject gameObject = collision.gameObject;

            Destroy(gameObject);

            pontos++;
        }
    }

    void OnGUI()
    {
        GUILayout.Label("Pontos: " + pontos);
    }    
}
