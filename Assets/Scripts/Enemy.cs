using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int DamageAmount = 10;

    [SerializeField] private SoundFXManagerEnemy soundFXManager;
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
    }
}
