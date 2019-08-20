using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground_check : MonoBehaviour {

    private Player player;
	private Animator anim;

    private void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
		anim = player.GetComponent<Animator> ();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
		anim.SetBool("grounded", true);
		player.grounded = true;
		player.prepare_jump = false;
		anim.SetBool ("prepare_jump", false);
		anim.SetBool ("jumping", false);
		player.jumping = false;
	
    }

    void OnTriggerStay2D(Collider2D col)
    {
		anim.SetBool("grounded", true);
		player.grounded = true;

    }

    void OnTriggerExit2D(Collider2D col)
    {
		anim.SetBool("grounded", false);
		player.grounded = false;
		player.prepare_jump = false;
		anim.SetBool ("prepare_jump", false);
		player.jumping = false;

    }
}
