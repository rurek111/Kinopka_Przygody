using System.Collections;
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

	public bool awake = false;
	public bool looking_right = true;

	public GameObject bullet;
	public Transform target;
	public Animator anim;
	public Transform shootPointLeft, shootPointRight;

	void Awake(){
		anim = gameObject.GetComponent<Animator> ();
	}

	void Start(){
		currentHP = maxHP;
	}

	void Update(){
		anim.SetBool ("awake", awake);
		anim.SetBool ("looking_right", looking_right);
		RangeCheck ();
		if (target.transform.position.x > transform.position.x) {
			looking_right = true;
		}
		if (target.transform.position.x < transform.position.x) {
			looking_right = false;
		}
		if (currentHP<=0) {
			Destroy (gameObject);
		}
	}

	void RangeCheck(){
		distance = Vector3.Distance (transform.position, target.transform.position);
		if (distance < wakeRange) {
			awake = true;
		}
		if (distance > wakeRange) {
			awake = false;
		}
	}

	public void attack(bool attackingRight){
		bulletTimer += Time.deltaTime;
		if (bulletTimer >= shotInterval) {
			Vector2 directrion = target.transform.position - transform.position;
			directrion.Normalize ();
			if (!attackingRight) {
				GameObject bulletClone;
				bulletClone = Instantiate (bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D> ().velocity = directrion * bulletSpeed;
				bulletTimer = 0;
			}
			if (attackingRight) {
				GameObject bulletClone;
				bulletClone = Instantiate (bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D> ().velocity = directrion * bulletSpeed;
				bulletTimer = 0;
			}
		}
	}

	public void Damage (int damage){
		currentHP -= damage;
		gameObject.GetComponent<Animation> ().Play ("player_dmg_flashing");
	}

}
