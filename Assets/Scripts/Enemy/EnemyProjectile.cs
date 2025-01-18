using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private EnemyRes enemyOBJ;
    private void Start()
    {
        Destroy(gameObject, 3.5f);
    }
    private void Update()
    {

        transform.Translate(Vector3.forward * 10f * Time.deltaTime);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        if (damageable != null)
        {
            damageable.TakeDamage(enemyOBJ.enemyDamage);
            player.PlayHitSound();
            Destroy(gameObject, 0.15f);
        }  
    }

}
