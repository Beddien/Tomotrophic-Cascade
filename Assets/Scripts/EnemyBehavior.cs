using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

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
	public float maxHealth = 15f;
	private bool isChasing = false;
	private readonly List<GameObject> inRange = new List<GameObject>();
	public Image healthbar;
	
    
void OnTriggerStay(Collider collision){
	  	if (collision.gameObject.CompareTag("Player")) isChasing = true;

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
    
		if (isChasing) {
			Vector3 pos = target.position - rb.position;
			pos.y = 0f;

			if (pos.sqrMagnitude > 0.0001f)
			{
				pos.Normalize();
				rb.linearVelocity = new Vector3 (pos.x * speed, rb.linearVelocity.y, pos.z * speed);

			}
		}
		else
		{
			rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
		}

		isChasing = false;

		health = Mathf.Clamp(health, 0, maxHealth);
		healthbar.fillAmount = health / maxHealth;
	}

}