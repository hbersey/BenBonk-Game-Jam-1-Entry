namespace Util.Health
{
    public interface IDamageable
    {
        bool CanTakeDamage();
        void TakeDamage(float amount);
    }
}