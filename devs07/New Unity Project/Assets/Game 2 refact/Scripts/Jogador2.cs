using UnityEngine;
using System.Collections;
//jogador
public class Jogador2 : MonoBehaviour
{
    public float velocidade = 5.0f;

    bool noChao;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        //coloca a velocidade x e y pra 0
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0); 

        //movimento do jogador em X
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * velocidade, 0, 0);

        //faz um teste entre a posição, naquela direção e com aquela distância...
        noChao = Physics.Raycast(transform.position, -Vector3.up, 1.0f);

        //if (noChao)
        //{
        //    Pular();//pulo automático
        //}

        if (Input.GetButtonDown("Jump") && noChao)
        {
            Pular();//pulo manual
        }
	}

    //void FixedUpdate()
    //{
    //    //faz um teste entre a posição, naquela direção e com aquela distância...
    //    //noChao = Physics.Raycast(transform.position, -Vector3.up, 1.0f);

    //    //if (noChao)
    //    //{
    //    //    Pular();//pulo automático
    //    //}
    //}

    void Pular()
    {
        if (!noChao) { return; }//não pula se não estiver no chão

        noChao = false;//se pular não está no chão ;-)

        rigidbody.velocity = new Vector3(0, 0, 0);//zera velocidade/movimentos laterais

        //aplica uma força para cima, quatro tipos diferentes
        rigidbody.AddForce(new Vector3(0, 700, 0), ForceMode.Force);
    }

    

}
