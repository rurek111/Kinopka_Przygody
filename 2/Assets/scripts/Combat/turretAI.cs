﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretAI : MonoBehaviour {

	//Variables
	public int currentHP;
	public int maxHP = 40;

	public float distance;
	public float wakeRange;
	public float shotInterval;
	public float bulletSpeed = 100f;
	public float bulletTimer;



	public GameObject bullet;
	public Transform target;
	private Animator anim;
	public Transform shootPointLeft, shootPointRight;

	void Awake(){
	}

	void Start(){
		currentHP = maxHP;
		anim = gameObject.GetComponent<Animator>();
        GameObject player =  GameObject.FindGameObjectWithTag("Player");
        target = player.transform;

    }

	void Update(){
		
		anim.SetBool ("attacking", false);

		RangeCheck ();

		if (target.transform.position.x > transform.position.x) 
		{
			anim.SetBool ("looking_right", true);
			//transform.ro
		}
		if (target.transform.position.x < transform.position.x) 
		{
			anim.SetBool ("looking_right", false);

		}
		if (currentHP<=0) {
			Destroy (gameObject);
		}

	}

	void RangeCheck(){
        if(target!= null)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < wakeRange)
            {
                anim.SetBool("awake", true);


            }
            if (distance > wakeRange)
            {
                anim.SetBool("awake", false);

            }
        }
		
	}

	public void attack(bool attackingRight){
		bulletTimer += Time.deltaTime;

		if (bulletTimer >= shotInterval) 
		{

			anim.SetBool ("attacking", true);
			Vector2 directrion = target.transform.position - transform.position;
			directrion.Normalize ();

            GameObject bulletClone;

            if (!attackingRight) 
			{
				
				bulletClone = Instantiate (bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
		
			}
			else
			{
				
				bulletClone = Instantiate (bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
			
			}
            bulletClone.GetComponent<Rigidbody2D>().velocity = directrion * bulletSpeed;
            bulletTimer = 0;
            anim.SetBool("attacking", false);
            anim.SetBool("armed", false);
            anim.SetBool("awake", false);


        }
    }

	public void Damage (int damage){
		currentHP -= damage;
	}

}
