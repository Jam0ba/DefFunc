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

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (damageable != null)
        {
            damageable.TakeDamage(DamageAmount);
            player.PlayHitSound();
        }
    }


    public void PlayHitSound()
    {
        soundFXManager?.PlaySound("Hit");
        healthBarImg.fillAmount = healthComponent.CurrentHealth / healthComponent.MaxHealth;

    }
}
