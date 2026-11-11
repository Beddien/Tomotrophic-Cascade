using System.Threading;
using UnityEngine;
using UnityEngine.UI;

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
    public float damage = 1.0f;
    public float attackTimer = 1.0f;
    public float AttackTimerConstant = 1.0f;

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

            Vector3 pos = currentTarget.position;
            if (new Vector2(pos.x - rb.position.x, pos.z - rb.position.z).magnitude > workDistance)
            {
                MoveToward(pos);
            }
            else
            {
                Freeze();
            }

            
        }
        else if (isFollowing) FollowPlayer();
        else Freeze();
  
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

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.CompareTag("Enemy")){
	    	attackTimer -= Time.deltaTime;
            if (!other.gameObject.transform.GetComponentInChildren<Image>().enabled)
            {
                other.gameObject.transform.GetComponentInChildren<Image>().enabled = true;
            }
			if (attackTimer < 0f)
			{

				attackTimer = AttackTimerConstant;
                if (other.gameObject.TryGetComponent(out EnemyBehavior enemy))
                {
                    enemy.health -= damage;
                }
			}
	    }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) attackTimer = AttackTimerConstant;
    }

    Transform FindTarget()
    {
        Collider[] suspects = Physics.OverlapSphere(transform.position, searchRadius);
        Transform bestTarget = null;
        float bestSqr = float.MaxValue;
        foreach (Collider i in suspects)
        {
            if (!i.CompareTag("Enemy") && !i.CompareTag("Fruit") && !i.CompareTag("Wall")) continue;
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
        MoveToward(followPosition);

    }

    void MoveToward(Vector3 goal)
    {
        Vector3 pos = new Vector3(goal.x - rb.position.x, 0f, goal.z - rb.position.z);
        
        Vector3 desired = pos.magnitude > speed * Time.fixedDeltaTime
            ? pos.normalized * speed
            : pos / Time.fixedDeltaTime;

        rb.linearVelocity = new Vector3(desired.x, rb.linearVelocity.y, desired.z);
    }

    void Freeze()
    {
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
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