using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public IObjectPool<GameObject> bulletPool;
    private float speed = 20f;               
    private float DamageAmount = 32;

    private void Start()
    {
      
        if (bulletPool == null)
        {
            bulletPool = FindFirstObjectByType<BulletPool>()?.GetPool();
        }

    }

    private void Update()
    {
       
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (bulletPool != null)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            Enemy enemy = collision.gameObject.GetComponentInChildren<Enemy>();
            if (damageable != null)
            {
                damageable.TakeDamage(DamageAmount);
                enemy.PlayHitSound();
            }
            ReturnToPool();
        }
    }


    private void ReturnToPool()
    {
      
       transform.position = Vector3.zero;
       transform.rotation = Quaternion.identity;
       gameObject.SetActive(false);
       bulletPool.Release(gameObject);
    }
}
