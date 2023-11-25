using UnityEngine;

public class Zombie : Unit, IDamageable
{
    [SerializeField] private float _health;
    
    public float HP { get => _health; }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0) Die();
    }

    private void Die()
    {
        manager.RemoveUnit(this);
        DyingAnimation();
    }

    private void DyingAnimation()
    {
        
    }
}
