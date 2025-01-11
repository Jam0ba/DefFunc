using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private float fireRate = 0.2f;

    [Space]
    public Transform shootPosition;
    public BulletPool bulletPool;
    private HealthComponent healthComponent;

    private Rigidbody rb;
    private Vector3 movement;
    private bool canFire = true;

    [SerializeField] private SoundFXManagerPlayer soundFXManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        healthComponent = GetComponent<HealthComponent>();
        soundFXManager = GetComponentInChildren<SoundFXManagerPlayer>();

    }

    private void Update()
    {

        if (Input.GetMouseButton(0) & canFire)
        {
            ShootBullet();

            soundFXManager?.PlaySound("Shoot");
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;


    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    public void PlayHitSound()
    {
        soundFXManager?.PlaySound("Hit");
    }

    private void ShootBullet()
    {
        if (bulletPool != null)
        {
            StartCoroutine(FireRate());
            GameObject bullet = bulletPool.GetBullet();
            TrailRenderer bulletTrail = bullet.GetComponent<TrailRenderer>();
            bullet.transform.position = shootPosition.position;
            bullet.transform.rotation = shootPosition.rotation;
            bullet.SetActive(true);
            bulletTrail.Clear();
        }
    }
    private IEnumerator FireRate()
    {
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

}
