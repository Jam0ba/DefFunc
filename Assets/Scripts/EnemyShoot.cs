using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject shootOBJ;

    public GameObject Test()
    {
        GameObject enemyProjectile = Instantiate(shootOBJ, transform.position, transform.rotation);

        return enemyProjectile;
    }
}
