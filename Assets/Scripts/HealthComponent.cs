using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHP = 100;
    [SerializeField] private int _currentHP;

    public int CurrentHealth { get => _currentHP; private set => _currentHP = value; }
    public int MaxHealth { get => _maxHP; private set => _maxHP = value; }

    public bool IsDead => CurrentHealth <= 0;

    private void Start()
    {
        CurrentHealth = MaxHealth;

    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log($"CurrentHealth after damage: {CurrentHealth}");

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
