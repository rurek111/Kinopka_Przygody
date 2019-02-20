using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground_check : MonoBehaviour {

    private player player;
	private Animator anim;

    private void Start()
    {
        player = gameObject.GetComponentInParent<player>();
		anim = player.GetComponent<Animator> ();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        player.grounded = true;
		anim.SetBool("grounded", true);
	
    }

    void OnTriggerStay2D(Collider2D col)
    {
        player.grounded = true;
		anim.SetBool("grounded", true);

    }

    void OnTriggerExit2D(Collider2D col)
    {
        player.grounded = false;
		anim.SetBool("grounded", false);

    }
}
