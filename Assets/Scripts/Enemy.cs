using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int DamageAmount = 10;
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Debug.Log($"Applying {DamageAmount} damage to {collision.gameObject.name}");
            damageable.TakeDamage(DamageAmount);
        }
    }
}
