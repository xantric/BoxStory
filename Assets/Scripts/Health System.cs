using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    float health = 100f; // Default health value
    public float maxHealth = 100f; // Maximum health value
    public ParticleSystem _deathEffect;
    public Animator _animator;
    void Start()
    {
        health = maxHealth; // Initialize health to max health at the start
    }
    public void TakeDamage(float damage)
    {
        health -= damage; // Reduce health by the damage amount
        Debug.Log($"Health reduced of {gameObject.name}. Current health: {health}");
        if (health <= 0f)
        {
            _animator.SetTrigger("Death");
            // Die(); // Call the Die method if health is zero or below
        }
    }
    public void Die()
    {
        // Handle death logic here, such as playing an animation or destroying the object
        Debug.Log("Character has died.");
        Destroy(gameObject); // Destroy the game object when health reaches zero
    }

    public void PlayDeathParticleEffect()
    {
        if (_deathEffect != null)
        {
            _deathEffect.Play();
        }
    }
}
