using NUnit.Framework;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
	//I first start getting the transform (position) of my player
    public Transform target;
        //Then I set up the speed of the enemy, that I can edit later
	public float speed = 2f;
        //Lastly, I added the enemy a rigidbody
	public Rigidbody rb;
	public int currentTomotrophs = 0;
	public float attackTimer = 0f;
	public float damageAmount = 0f;
	
  
//First thing, I will create a function that follows the player
void FollowPlayer(){
    	//I will create a vector 3 called pos that stores the movement that I want my player to do
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //I will use these two built-in functions to follow the player
        rb.MovePosition(pos);
       	transform.LookAt(target);
    }
    
//Finally, I add a collider function that calls the FollowPlayer() function when it is within its range
void OnTriggerStay(Collider player){
	  		if(player.tag == "Player"){
	    		FollowPlayer();
	    	}
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tomotroph"))
		{
			currentTomotrophs++;
            Debug.Log("Tomotroph entered enemy radius");

		if(attackTimer <= 0f) attackTimer = 2f;
			{
				Destroy(collision.gameObject);
				Debug.Log("Tomotroph died");
				attackTimer = 0f;
			}

		}

		if (collision.gameObject.CompareTag("Player"))
		{
			
			
				Debug.Log("damaged player!");
				damageAmount =+ 1f;
				GetComponent<Renderer>().material.color = Color.red;
			
		}

    }

}