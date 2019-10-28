using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	private Player player;
    public int damage = 1;
    public float duration = 0.03f, power = 700;
	void Start(){
	
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();

    }

	void OnTriggerEnter2D(Collider2D col){
	
		if (col.CompareTag ("Player"))
        {
		
			player.Damage (damage);

			Knockback (duration, power, player.transform.position);
		}
	
	}


    public void Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
    {
        float timer = 0;
        {
            Rigidbody2D rb2D = player.GetComponent<Rigidbody2D>();
            timer += Time.deltaTime;
            rb2D.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockbackPwr, transform.position.z));
        }
    }
}
