using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private Image healthBarImg;
    [SerializeField] private SoundFXManagerEnemy soundFXManager;
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private EnemyRes enemyOBJ;


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
            damageable.TakeDamage(enemyOBJ.enemyDamage);
            player.PlayHitSound();
        }
    }
    public void PlayHitSound()
    {
        if(healthComponent.CurrentHealth > 0.0f)
        {
            soundFXManager.PlaySound(enemyOBJ.enemyHitSound);
            healthBarImg.fillAmount = healthComponent.CurrentHealth / healthComponent.MaxHealth;
        }
        
    }
}
