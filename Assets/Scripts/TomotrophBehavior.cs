using UnityEngine;
using UnityEngine.InputSystem;

public class TomotrophBehavior : MonoBehaviour
{
    public GameObject followRegion;
	public float speed = 2f;
	public Rigidbody rb;
    private Vector3 followRegionCenter;
    private Vector3 followRegionExtents;
    private Vector3 followPosition;
    private Vector3 followOffset;
    public Vector3 thrownPosition;
    public Vector3 throwForce;
    public float throwTime = 0.5f;
    public bool isThrown = false;
    public bool isActive = false;
    public bool isPlanted = false;

    void OnDestroy()
    {
        TomotrophManager.Deregister(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.linearVelocity = new Vector3(0,0,0);
            // in the future put logic here that checks if there is a fruit or enemy nearby
            if (!isActive)
            {
                isThrown = false;
            }
        }
    }

    /*  
    This method has the tomotroph move to its randomly selected position in the
    follow region object

    */
    void FollowPlayer()
    {

        // saves the offset between the tomotroph's individual spot and the followregion.
        // then updates the position based on that offset and the followregion's current
        // position (so that they can keep following even when the player moves)
        followPosition = followRegion.transform.position + followOffset;
        Vector3 pos = Vector3.MoveTowards(transform.position, new Vector3(followPosition.x, 
        transform.position.y, followPosition.z), speed * Time.deltaTime);

        // The tomotroph is moved here using its rigidbody, which is fine for now but
        // is causing a bug where the tomotrophs fly up into the sky and therefore
        // will need to be changed in the future.
        rb.MovePosition(pos);
        transform.LookAt(pos);



        

    }
    /*
    This OnTriggerEnter is used to determine what the area tomotrophs should navigate to is
    upon the player entering their sphere collider.

    @param player = the collider of the object which has entered the range of the tomotroph

    */
    void OnTriggerEnter(Collider player)
    {
        if(player.tag == "Player"){
            TomotrophManager.Register(this);

            followRegionCenter = followRegion.GetComponent<BoxCollider>().bounds.center;
            followRegionExtents = followRegion.GetComponent<BoxCollider>().bounds.extents;
            followPosition = RandomFollowingPosition(followRegionCenter, followRegionExtents);
            followOffset = followPosition - followRegion.transform.position;
        }
    }

    /*
    When the player remains in radius of the tomotroph, calls followplayer so that the
    tomotroph continues to navigate towards the player

    @param player = the collider of the object which is in the range of the tomotroph

    */
    void OnTriggerStay(Collider player){
  		if(player.tag == "Player" && !isThrown){
			FollowPlayer();
	    }
	}

    /*
    Calculates a random position within the bounds of the followRegion object
    towards which a given tomotroph can move and stay in while following the player

    NOTE: I believe that this just calculates offset, which we recalculate later.
    Revisit to hopefully simplify the code before continuing work here (maybe
    remove the later offset value completely and just define it here???)

    @param center = the center of the followRegion object
    @param extents = the edges of the followregion object

    */
    Vector3 RandomFollowingPosition(Vector3 center, Vector3 extents)
    {
        float randomX = Random.Range(-extents.x, extents.x);
        float randomZ = Random.Range(-extents.z, extents.z);
        return center + new Vector3(randomX, 0, randomZ);
    }

    public void Throw(Vector3 targetPosition)
    {
        isThrown = true;
        thrownPosition = targetPosition;
        Vector3 displacement = targetPosition - transform.position;
        throwForce.x = (displacement.x/throwTime) * 0.35f;
        throwForce.z = (displacement.z/throwTime) * 0.35f;
        throwForce.y = 5 - 0.5f * Physics.gravity.y * throwTime;


        rb.linearVelocity = throwForce;
    }

    // void Update()
    // {
    //     if (isThrown && transform.position.x == thrownPosition.x && transform.position.z == thrownPosition.z)
    //     {
    //         isThrown = false;
    //         rb.linearVelocity = new Vector3(0,0,0);
    //     }
    // }
}