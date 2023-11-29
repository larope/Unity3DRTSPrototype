using UnityEngine;
using UnityEngine.Serialization;

public abstract class Unit : MonoBehaviour, IDamageable
{
    [HideInInspector] public ArmyManager manager;
    public float HP { get => health; }

    protected GameObject target;
    
    
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackRate;

    protected GameObject GetNearestEnemyUnit() => manager.GetNearestEnemyUnit(transform.position);
    


    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0) Die();
    }
    
    private void Die()
    {
        manager.RemoveUnit(gameObject);
        DyingAnimation();
    }

    private void DyingAnimation()
    {
        
    }
}
