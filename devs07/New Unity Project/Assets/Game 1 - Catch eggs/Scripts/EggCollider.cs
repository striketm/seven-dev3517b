using UnityEngine;
using System.Collections;

public class EggCollider : MonoBehaviour {

    PlayerScript myPlayerScript;

    void Awake()
    {
        myPlayerScript = transform.parent.GetComponent<PlayerScript>();
    }

	void OnTriggerEnter(Collider theCollision)
    {
        //In this game we don't need to check *what* we hit; it must be the eggs
        GameObject collisionGO = theCollision.gameObject;
        Destroy(collisionGO);

        myPlayerScript.theScore++;
    }
}
