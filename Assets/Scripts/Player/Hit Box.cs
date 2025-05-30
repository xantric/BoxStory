using UnityEngine;

public class HitBox : MonoBehaviour
{
    public float damage = 10f; // Damage to apply to enemies
    public bool isAttacking = false; // Flag to check if the player is attacking
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && isAttacking)
        {
            // Assuming the enemy has a HealthSystem component
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                // Apply damage to the enemy
                healthSystem.TakeDamage(damage * Time.deltaTime); // Example damage value
                Debug.Log("Hit an enemy and applied damage.");
            }
            else
            {
                Debug.LogWarning("Enemy does not have a HealthSystem component.");
            }
        }
    }
}
