﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public float maxSpeed = 7f;
	public float maxSpeed_walk = 5f;
	public float maxSpeed_run = 7f;
	public float speed = 7f;
	public float speed_walk = 5f;
	public float speed_run = 7f;
	public float jump_power = 700f;
	public float jump_timer = 0.0f;


	public bool grounded = true;
	public bool prepare_jump = false;
	public bool direction_normal = true;
	public bool prefer_run = true;
	public bool jumping = false;


	private Rigidbody2D rb2D;
    private Animator anim;
	private game_master gm;
	private float player_scale = 0.13739f; 
	private float prep_jump_time = 0.17f;

	public float currentHP, maxHP = 5.0f, currentMana, maxMana = 5.0f;

    public int exp = 0;

    //// public bool dialoguing = false;
    public Journal journal;
    
    public PlayerInventory inventory;



	// Use this for initialization
	void Start () 
	{
		rb2D = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
		gm = GameObject.FindGameObjectWithTag ("game_master").GetComponent<game_master> ();

		transform.localScale = new Vector3(player_scale, player_scale, player_scale);

		currentHP = maxHP;
		currentMana = maxMana;
		anim.SetBool("direction_normal", true);
		anim.SetBool("prefer_run", true);
	}

	// Update is called once per frame
	void Update () 
	{
		Move ();
		HealthCheck ();
    }

	void FixedUpdate () 
	{
		float h = Input.GetAxis("Horizontal");

        rb2D.velocity = new Vector2(h*maxSpeed, rb2D.velocity.y);

		Jump ();
		PressKey ();

        /*
       rb2D.AddForce ((Vector2.right * speed) * h);


       if (rb2D.velocity.x > maxSpeed)
       {
           rb2D.velocity = new Vector2(maxSpeed, rb2D.velocity.y);
       }

       if(rb2D.velocity.x < -maxSpeed)
       {
           rb2D.velocity = new Vector2(-maxSpeed, rb2D.velocity.y);
       }
       */
    }

    void HealthCheck()
	{
		if (currentHP > maxHP) 
		{
			currentHP = maxHP;
		}
		else if (currentHP <= 0)
		{
			Die ();
		}
	}

    void ManaCheck()
    {
        if(currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        if(currentMana<0)
        {
            currentMana = 0f;
        }
    }
    
	void Move()
	{
		anim.SetFloat("speed", Mathf.Abs(rb2D.velocity.x) );

		if ((Input.GetAxis("Horizontal") < -0.1f) && grounded)
		{
			transform.localScale = new Vector3(-player_scale, player_scale, player_scale);
			anim.SetBool("direction_normal", false);
			///rb2D.AddForce (Vector2.left * speed);
		}
		else if ((Input.GetAxis("Horizontal") > 0.1f) && grounded)
		{
			transform.localScale = new Vector3(player_scale, player_scale, player_scale);
			anim.SetBool("direction_normal", true);
			//rb2D.AddForce (Vector2.right * speed) ;
		}


        // Move the character by finding the target velocity
       // Vector3 targetVelocity = new Vector2(speed * 10f, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
       // m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    
    }
    
	void Jump ()
	{
		if (Input.GetButtonDown("Jump") && grounded == true)
		{
			anim.SetBool ("prepare_jump",true);
			prepare_jump = true;
			jump_timer = prep_jump_time;
		}
		if (prepare_jump && grounded)
		{

			if (jump_timer > 0) 
			{
				jump_timer -= Time.deltaTime;
			} 
			else 
			{
				prepare_jump = false;
				anim.SetBool ("prepare_jump",false);
				anim.SetBool ("jumping",true);
				jumping = true;
			}
		}
		if (jumping && grounded)
		{
			rb2D.AddForce (Vector2.up * jump_power);
			jumping = false;
			anim.SetBool ("jumping",true);
		}
	}

	void LeftControl ()
	{
		if (Input.GetKeyDown ("left ctrl"))
		{
			if (prefer_run == true) 
			{
				anim.SetBool ("prefer_run", false);
				prefer_run = !prefer_run;
				maxSpeed = maxSpeed_walk;
				speed = speed_walk;

			} 
			else if (prefer_run == false) 
			{
				anim.SetBool ("prefer_run", true);
				prefer_run = !prefer_run;
				maxSpeed = maxSpeed_run;
				speed = speed_run;

			}
		}
	}


	void Die ()
	{
		SceneManager.LoadScene (0);		//restart
	}
		
	public void Damage(float dmg)
	{
		if (dmg > currentHP) 
		{
			currentHP = 0;
		}

		else 
		{
			currentHP -= dmg;
		}
        HealthCheck();
	}



	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("burak")) 
		{
			Destroy (col.gameObject);
			gm.points += 1;
            gm.Points();
		}
	}



    void Heal(float howmuch = 1f, float cost = 3f)
    {
        if(currentHP<=maxHP-howmuch)
        {
            if (currentMana >= cost)
            {
                currentMana -= cost;
                currentHP += howmuch;
                HealthCheck();
                ManaCheck();
            }

        }


    }

    void Inventory()
    {
        if (Input.GetKeyDown("i"))
        {
            inventory.ToggleInventory();
        }
    }
    void HealingSpell()
    {
        if (Input.GetKeyDown("q"))
        {
            Heal(1, 3);
        }
    }

    void Journal()
    {
        if (Input.GetKeyDown("j"))
        {
            journal.ToggleJournal();


        }
    }

    void PressKey()
    {
        LeftControl();
        Inventory();
        Journal();
        HealingSpell();
    }


   // public PlayerInventory GetInventory()
  //  {
  //      return inventory;
  //  }
}	



