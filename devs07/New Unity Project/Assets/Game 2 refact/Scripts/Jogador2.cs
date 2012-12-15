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

        //faz um teste entre a posi��o, naquela dire��o e com aquela dist�ncia...
        noChao = Physics.Raycast(transform.position, -Vector3.up, 1.0f);

        //if (noChao)
        //{
        //    Pular();//pulo autom�tico
        //}

        if (Input.GetButtonDown("Jump") && noChao)
        {
            Pular();//pulo manual
        }
	}

    //void FixedUpdate()
    //{
    //    //faz um teste entre a posi��o, naquela dire��o e com aquela dist�ncia...
    //    //noChao = Physics.Raycast(transform.position, -Vector3.up, 1.0f);

    //    //if (noChao)
    //    //{
    //    //    Pular();//pulo autom�tico
    //    //}
    //}

    void Pular()
    {
        if (!noChao) { return; }//n�o pula se n�o estiver no ch�o

        noChao = false;//se pular n�o est� no ch�o ;-)

        rigidbody.velocity = new Vector3(0, 0, 0);//zera velocidade/movimentos laterais

        //aplica uma for�a para cima, quatro tipos diferentes
        rigidbody.AddForce(new Vector3(0, 700, 0), ForceMode.Force);
    }

    

}
