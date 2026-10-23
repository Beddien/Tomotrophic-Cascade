using UnityEngine;

public class TomotrophBehavior : MonoBehaviour
{
    public GameObject followRegion;
    private Transform currentTarget;
    public float searchRadius = 6.0f;
    public float workDistance = 0.5f;
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
    public bool isLanded = false;
    public bool isFollowing = false;

    public bool isPlanted = false;

    void FixedUpdate()
    {
        if (isThrown) return;

        if (isActive)
        {
            if (currentTarget == null || !currentTarget.gameObject.activeInHierarchy)
            {
                isActive = false;
                isLanded = false;
                currentTarget = null;
                isFollowing = true;
                return;
            }

            Vector3 goal = new Vector3(currentTarget.position.x, rb.position.y, currentTarget.position.z);
            if ((goal - transform.position).magnitude > workDistance)
            {
                Vector3 pos = Vector3.MoveTowards(rb.position, goal, speed * Time.fixedDeltaTime);
                rb.MovePosition(pos);
            }
            else
            {
                // attack logic here later
            }

            
        }
        if (isFollowing) FollowPlayer();
  
    }

    void OnDestroy()
    {
        TomotrophManager.Deregister(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isThrown)
        {
            isThrown = false;
            
            rb.linearVelocity = Vector3.zero;
            // in the future put logic here that checks if there is a fruit or enemy nearby

            currentTarget = FindTarget();
            isLanded = currentTarget != null;
            isActive = currentTarget != null;
            isFollowing = currentTarget == null;
        }
    }

    Transform FindTarget()
    {
        Collider[] suspects = Physics.OverlapSphere(transform.position, searchRadius);
        Transform bestTarget = null;
        float bestSqr = float.MaxValue;
        foreach (Collider i in suspects)
        {
            if (!i.CompareTag("Enemy") && !i.CompareTag("Fruit")) continue;
            float currentSqr = (i.transform.position - transform.position).sqrMagnitude;
            if (currentSqr < bestSqr)
            {
                bestSqr = currentSqr;
                bestTarget = i.transform;
            }
        }

        return bestTarget;
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



        

    }
    /*
    This OnTriggerEnter is used to determine what the area tomotrophs should navigate to is
    upon the player entering their sphere collider.

    @param player = the collider of the object which has entered the range of the tomotroph

    */
    void OnTriggerEnter(Collider player)
    {
        if(player.tag == "Player" && !isThrown && !isActive){
            isFollowing = true;
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
        isFollowing = false;
        isActive = false;
        isLanded = false;
        currentTarget = null;
        thrownPosition = targetPosition;
        Vector3 displacement = targetPosition - transform.position;
        throwForce.x = (displacement.x/throwTime) * 0.35f;
        throwForce.z = (displacement.z/throwTime) * 0.35f;
        throwForce.y = 5 - 0.5f * Physics.gravity.y * throwTime;


        rb.linearVelocity = throwForce;
    }
    public void Recall()
    {
        if (isThrown) return;
        isActive = false;
        isLanded = false;
        currentTarget = null;
        isFollowing = true;

    }
}