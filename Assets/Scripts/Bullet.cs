using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public IObjectPool<GameObject> bulletPool;
    private float speed = 20f;               
    private int DamageAmount = 10;

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
            if (damageable != null)
            {
                Debug.Log($"Applying {DamageAmount} damage to {collision.gameObject.name}");
                damageable.TakeDamage(DamageAmount);
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
