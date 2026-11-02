using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
	//I first start getting the transform (position) of my player
    public Transform target;
        //Then I set up the speed of the enemy, that I can edit later
	public float speed = 2f;
        //Lastly, I added the enemy a rigidbody
	public Rigidbody rb;
	public float attackTimer = 2.5f;
	public float AttackTimerConstant = 2.5f;
	public float damageAmount = 0f;
	public float health = 15f;
	private readonly List<GameObject> inRange = new List<GameObject>(); //TEMP FOR COMBAT
	
  
//First thing, I will create a function that follows the player
void FollowPlayer(){
    	//I will create a vector 3 called pos that stores the movement that I want my player to do
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //I will use these two built-in functions to follow the player
        rb.MovePosition(pos);
       	transform.LookAt(target);
    }
    
void OnTriggerStay(Collider collision){
	  		if (collision.gameObject.CompareTag("Player")){
	    		FollowPlayer();
	    	}

	}

void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tomotroph"))
		{
			// TEMP FOR COMBAT
			inRange.Add(collision.gameObject);



		}

		if (collision.gameObject.CompareTag("Player"))
		{
			
			
				damageAmount += 1f;
				GetComponent<Renderer>().material.color = Color.red;
			
		}

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tomotroph"))
		{
			inRange.Remove(collision.gameObject);
			if (inRange.Count == 0)
			{
				attackTimer = AttackTimerConstant;
			}
		}
    }

	void Update()
	{
		if (inRange.Count > 0) {
			attackTimer -= Time.deltaTime;
			if (attackTimer < 0f)
			{
				Debug.Log("Tomotroph eaten");
				attackTimer = AttackTimerConstant;
				int toKill = Random.Range(0, inRange.Count);
				GameObject victim = inRange[toKill];
				inRange.Remove(victim);
				Destroy(victim);
				if (inRange.Count == 0)
				{
					attackTimer = AttackTimerConstant;
				}
			}
		}

	}

    void FixedUpdate()
    {
        if (health <= 0) Destroy(gameObject); // TEMP DEATH CONDITION
    }

}