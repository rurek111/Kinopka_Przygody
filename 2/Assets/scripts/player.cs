﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class player : MonoBehaviour {
    public float maxSpeed = 20f;
	public float maxSpeed_walk = 10f;
	public float maxSpeed_run = 20f;
	public float speed = 400f;
	public float speed_walk = 200f;
	public float speed_run = 400f;
	public float jump_power = 150f;

	public bool grounded;
	public bool direction_normal;
	public bool prefer_run;

	private Rigidbody2D rb2D;
    private Animator anim;
	private game_master gm;
	private float player_scale = 0.13739f; 

	//Stats
	public int currentHP;
	public int maxHP = 5;
	public int state;


	// Use this for initialization
	void Start () {
		rb2D = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
		gm = GameObject.FindGameObjectWithTag ("game_master").GetComponent<game_master> ();

		currentHP = maxHP;
		state = 1;
		anim.SetBool("direction_normal", true);
		anim.SetBool("prefer_run", true);
			}

	// Update is called once per frame
	void Update () {
        anim.SetBool("grounded", grounded);
        anim.SetFloat("speed", Mathf.Abs(rb2D.velocity.x));

        if (Input.GetAxis("Horizontal") < -0.1f)
        {

			transform.localScale = new Vector3(-player_scale, player_scale, player_scale);
			anim.SetBool("direction_normal", false);
        }

        else if (Input.GetAxis("Horizontal") > 0.1f)
        {
			transform.localScale = new Vector3(player_scale, player_scale, player_scale);
			anim.SetBool("direction_normal", true);
        }

        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            rb2D.AddForce(Vector2.up * jump_power);
        }

		if (currentHP > maxHP) {
			currentHP = maxHP;
		}

		if (currentHP <= 0) {
			Die ();
		}

		if (Input.GetKeyDown ("left ctrl"))
		{
			if (prefer_run == true) {
				anim.SetBool ("prefer_run", false);
				maxSpeed = maxSpeed_walk;
				speed = speed_walk;

			} 
			else if (prefer_run == false) {
				anim.SetBool ("prefer_run", true);
				prefer_run = !prefer_run;
				maxSpeed = maxSpeed_run;
				speed = speed_run;

			}

		}

		update_state ();
    }

	void FixedUpdate () {
		float h = Input.GetAxis("Horizontal");
		rb2D.AddForce ((Vector2.right * speed) * h);
        if (rb2D.velocity.x > maxSpeed)
        {
            rb2D.velocity = new Vector2(maxSpeed, rb2D.velocity.y);
        }
        if(rb2D.velocity.x < -maxSpeed)
        {
            rb2D.velocity = new Vector2(-maxSpeed, rb2D.velocity.y);
        }

    }

	void Die () {
		//restart
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);

	}



		
		

	public void Damage(int dmg){
		
		if (dmg > currentHP) {

			currentHP = 0;

		}
		else {
			currentHP -= dmg;
		}	
	}

	public void Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir){

		float timer = 0;
		while (knockDur > timer) {
			timer += Time.deltaTime;
			rb2D.AddForce (new Vector3 (knockbackDir.x * -100, knockbackDir.y * knockbackPwr, transform.position.z));
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag ("burak")) {
			Destroy (col.gameObject);
			gm.points += 1;
		}
	}

	void update_state(){

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			state = 1;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			state = 2;
		}

	}

}	



