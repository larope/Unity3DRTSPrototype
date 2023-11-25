public interface IDamageable
{
    public float HP { get; }

    public void TakeDamage(float damage);
}