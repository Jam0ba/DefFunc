using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRes", menuName = "Scriptable Objects/EnemyRes")]
public class EnemyRes : ScriptableObject
{

    public string enemyName;
    [Space]
    public float enemyHealth;
    public float enemyDamage;
    public float enemySpeed;
    [Space]
    public string enemyHitSound;
    public string enemyAttackSound;
    public string enemyWalkingSound;
    [Space]
    public GameObject enemyPrefab;
    public GameObject enemyModel;



}
