using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionRange = 5f;
    public Animator animator;
    public float moveSpeed = 2f;
    public LayerMask playerLayer; 
    private Transform targetPlayer;
    public float damage = 10f; // Damage to apply to the player on collision
    private void Update()
    {
        DetectPlayer();
        if (targetPlayer != null)
        {
            Vector3 direction = (targetPlayer.position - transform.position);
            direction.y = 0; // Keep the enemy on the same horizontal plane
            direction.Normalize(); // Normalize the direction vector to get a unit vector
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.LookAt(targetPlayer.position - new Vector3(0, targetPlayer.position.y - transform.position.y, 0)); // Look at the player
            if(animator != null)
            {
                animator.SetBool("isMoving", true);
            }
        }
        else
        {
            if(animator != null)
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    void DetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange, playerLayer);

        if (hits.Length > 0)
        {
            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    targetPlayer = hit.transform;
                    return; 
                }
            }
        }
        else
        {
            targetPlayer = null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with Player!");
            
            collision.gameObject.GetComponent<HealthSystem>()?.TakeDamage(damage); // Example damage value
            animator.SetTrigger("Death");
            //Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
