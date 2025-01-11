using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHP = 100;
    [SerializeField] private float _currentHP;

    public float CurrentHealth { get => _currentHP; private set => _currentHP = value; }
    public float MaxHealth { get => _maxHP; private set => _maxHP = value; }

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
        Destroy(gameObject);
    }
}
