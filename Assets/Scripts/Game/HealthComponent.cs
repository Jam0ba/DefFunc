using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private float maxHP = 100f;
    private float currentHP;

    public float CurrentHealth
    {
        get => currentHP;
        private set => currentHP = value;
    }

    public float MaxHealth
    {
        get => maxHP;
        private set => maxHP = value;
    }

    public bool IsDead => CurrentHealth <= 0;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Destroy(gameObject, 0.1f);
    }
}