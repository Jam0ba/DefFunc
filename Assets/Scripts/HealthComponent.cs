using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHP = 100;
    [SerializeField] private SoundFXManagerEnemy soundFXManagerEnemy;
    [SerializeField] private SoundFXManagerPlayer soundFXManagerPlayer;
    [SerializeField] private int _currentHP;
    [SerializeField] private Slider healthSliderPlayer;

    public int CurrentHealth { get => _currentHP; private set => _currentHP = value; }
    public int MaxHealth { get => _maxHP; private set => _maxHP = value; }



    private void Start()
    {
        if (healthSliderPlayer != null)
        {
            CurrentHealth = MaxHealth;
            healthSliderPlayer.maxValue = MaxHealth;
            healthSliderPlayer.value = CurrentHealth;
        }

        if (soundFXManagerEnemy == null || soundFXManagerPlayer == null)
        {
            return;
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log($"CurrentHealth after damage: {CurrentHealth}");
        soundFXManagerEnemy?.PlaySound("Hit");
        soundFXManagerPlayer?.PlaySound("Hit");
        healthSliderPlayer.value = CurrentHealth;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            OnDeath();
        }
    }
    /*
    public void Heal(int healAmount)
    {
        CurrentHealth += healAmount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
    */
    public bool IsDead => CurrentHealth <= 0;

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
