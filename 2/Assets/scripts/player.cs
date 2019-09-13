using System.Collections;
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
	public float jump_power = 350f;
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
	private float prep_jump_time = 0.2f;

	public int currentHP;
	public int maxHP = 5;
	public int state;

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
		state = 1;
		anim.SetBool("direction_normal", true);
		anim.SetBool("prefer_run", true);
	}

	// Update is called once per frame
	void Update () 
	{
		Move ();
		Jump ();
		HealthCheck ();
		PressKey ();
		update_state ();
    }

	void FixedUpdate () 
	{
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

	void Move()
	{
		anim.SetFloat("speed", Mathf.Abs(rb2D.velocity.x) );

		if ((Input.GetAxis("Horizontal") < -0.1f) && grounded)
		{
			transform.localScale = new Vector3(-player_scale, player_scale, player_scale);
			anim.SetBool("direction_normal", false);
			rb2D.AddForce (Vector2.left * speed);
		}
		else if ((Input.GetAxis("Horizontal") > 0.1f) && grounded)
		{
			transform.localScale = new Vector3(player_scale, player_scale, player_scale);
			anim.SetBool("direction_normal", true);
			rb2D.AddForce (Vector2.right * speed) ;
		}
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
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);		//restart
	}
		
	public void Damage(int dmg)
	{
		if (dmg > currentHP) 
		{
			currentHP = 0;
		}

		else 
		{
			currentHP -= dmg;
		}	
	}

	public void Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
	{
		float timer = 0;
		{
			timer += Time.deltaTime;
			rb2D.AddForce (new Vector3 (knockbackDir.x * -100, knockbackDir.y * knockbackPwr, transform.position.z));
		}
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

	void update_state()
	{

		if (Input.GetKeyDown(KeyCode.Alpha1)) 
		{
			state = 1;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) 
		{
			state = 2;
		}

	}

    void Inventory()
    {
        if (Input.GetKeyDown("i"))
        {
            inventory.ToggleInventory();
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
    }


    public PlayerInventory GetInventory()
    {
        return inventory;
    }
}	



