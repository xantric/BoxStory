using UnityEngine;
using System.Collections.Generic;
public class AttackManager : MonoBehaviour
{
    [SerializeField]
    List<WeaponAttackDamage> _weaponAttackDamageMapping;
    [SerializeField]
    Animator _animator;
    [SerializeField]
    WeaponManager _weaponManager;
    [SerializeField]
    GameObject _weaponHolder;
    HitBox _hitBox; // Assuming HitBox is a component that handles hit detection
    float Speed;
    void Start()
    {
        Speed = GetComponent<PlayerStateMachine>()._speed; // Store initial speed
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                _animator.SetFloat("Attack", 0); // Reset attack type after the animation completes
                GetComponent<PlayerStateMachine>()._speed = Speed; // Resume player movement after attack animation
            }
            else
            {
                GetComponent<PlayerStateMachine>()._speed = 0; // Stop player movement during attack animation
            }
        }
        _hitBox = _weaponHolder.GetComponentInChildren<HitBox>();
        if (_hitBox != null)
        {
            _hitBox.isAttacking = _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        }
        else
        {
            Debug.LogWarning("HitBox is not assigned or found.");
        }
    }
    void Attack()
    {
        if (_weaponAttackDamageMapping.Count == 0)
        {
            Debug.LogWarning("No weapon attack damage mapping found.");
            return;
        }
        _hitBox = _weaponHolder.GetComponentInChildren<HitBox>();
        // Assuming the first weapon is the current one
        WeaponAttackDamage currentWeapon = _weaponAttackDamageMapping[_weaponManager._currentWeapon % _weaponAttackDamageMapping.Count];
        if (currentWeapon != null)
        {
            // Perform attack logic here, e.g., apply damage to enemies
            _hitBox.damage = currentWeapon.damage; // Update hitbox damage based on the current weapon
            Debug.Log($"Attacking with {currentWeapon.weaponID} for {currentWeapon.damage} damage.");
        }
        else
        {
            Debug.LogWarning("Current weapon is null.");
        }
        if (_animator != null)
        {
            Debug.Log($"Setting animator attack type to {currentWeapon.attackType}");
            _animator.SetFloat("Attack", currentWeapon.attackType); 
        }
        else
        {
            Debug.LogWarning("Animator is not assigned.");
        }
    }
}
