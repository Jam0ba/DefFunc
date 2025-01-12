using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    private int DamageAmount = 10;

    [SerializeField] private Image healthBarImg;
    [SerializeField] private SoundFXManagerEnemy soundFXManager;
    [SerializeField] private HealthComponent healthComponent;


    private void Start()
    {
        healthComponent = GetComponentInParent<HealthComponent>();

    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        if (damageable != null)
        {
            damageable.TakeDamage(DamageAmount);
            player.PlayHitSound();
        }
    }
    public void PlayHitSound()
    {
        if(healthComponent.CurrentHealth > 0.0f)
        {
            soundFXManager.PlaySound("Hit");
            healthBarImg.fillAmount = healthComponent.CurrentHealth / healthComponent.MaxHealth;
        }
        
    }
}
