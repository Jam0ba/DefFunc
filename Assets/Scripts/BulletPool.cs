using UnityEngine.Pool;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    private int initialSize = 100;
    private IObjectPool<GameObject> pool;

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(
            () => CreateBullet(),    
            bullet => bullet.SetActive(true), 
            bullet => bullet.SetActive(false), 
            bullet => Destroy(bullet),
            false,
            initialSize,
            initialSize);
    }

    public IObjectPool<GameObject> GetPool()
    {
        return pool;
    }

    public GameObject GetBullet()
    {
        return pool.Get();
    }

    public void ReleaseBullet(GameObject bullet)
    {
        pool.Release(bullet);
    }

    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().bulletPool = pool;
        return bullet;
    }
}
