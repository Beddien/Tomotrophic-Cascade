using UnityEngine;
using UnityEngine.UIElements;

public class TomotrophBehavior : MonoBehaviour
{
	//I first start getting the transform (position) of my player
    public GameObject followRegion;
    
        //Then I set up the speed of the enemy, that I can edit later
	public float speed = 2f;
        //Lastly, I added the enemy a rigidbody
	public Rigidbody rb;

    private Vector3 followRegionCenter;
    private Vector3 followRegionExtents;
    private Vector3 followPosition;
    private Vector3 followOffset;
    private bool isFollowing = false;
  
    void Update()
    {
        if(isFollowing)
        {
            followPosition += followRegion.transform.position + followOffset;
        }
    }
    void FollowPlayer()
    {


        if (transform.position == followPosition)
        {
            
        }
        else
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, followPosition,
            speed * Time.deltaTime);

            //I will use these two built-in functions to follow the player
            rb.MovePosition(pos);
       	    transform.LookAt(pos);
        }


        

    }
    
//Finally, I add a collider function that calls the FollowPlayer() function when it is within its range
    void OnTriggerEnter(Collider player)
    {
        if(player.tag == "Player"){
            isFollowing = true;
            followRegionCenter = followRegion.GetComponent<BoxCollider>().bounds.center;
            followRegionExtents = followRegion.GetComponent<BoxCollider>().bounds.extents;
            followPosition = RandomFollowingPosition(followRegionCenter, followRegionExtents);
            followOffset = followPosition - followRegion.transform.position;
        }
    }
    void OnTriggerStay(Collider player){
  		if(player.tag == "Player"){
			FollowPlayer();
	    }
	}

    void OnTriggerExit(Collider player)
    {
        if(player.tag == "Player"){
			isFollowing = false;
	    }
    }

    Vector3 RandomFollowingPosition(Vector3 center, Vector3 extents)
    {
        float randomX = Random.Range(-extents.x, extents.x);
        float randomZ = Random.Range(-extents.z, extents.z);
        return center + new Vector3(randomX, 0, randomZ);
    }
}