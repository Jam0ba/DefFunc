using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float health = 100.0f;
    private float maxMovementSpeed = 5f;
    public float speed = 5f;
    public float runSpeed = 8f;
    public LayerMask groundLayer;
    public float rotationSpeed = 10f;
    private float fireRate = 0.2f;

    [Space]
    public TrailRenderer runTrail;

    [Space]
    public Transform[] shootPosition;
    public BulletPool bulletPool;

    private Rigidbody rb;
    private Vector3 movement;
    private DashSystem dashSystem;

    private bool canFire = true;

    [SerializeField] private SoundFXManagerPlayer soundFXManager;

    private void Start()
    {
        runTrail.emitting = false;
        dashSystem = GetComponent<DashSystem>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;


        soundFXManager = GetComponentInChildren<SoundFXManagerPlayer>();

        if (soundFXManager == null)
        {
            Debug.LogError("SoundFXManagerPlayer Missing");
        }
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift) & dashSystem.dashCapacity > 1.0f & !dashSystem.isFilling)
        {
            runTrail.emitting = true;
            speed = runSpeed;
            dashSystem.dashCapacity -= 18.8f * Time.deltaTime;

            //soundFXManager?.PlaySound("Run");
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || dashSystem.dashCapacity < 1.0f || dashSystem.isFilling)
        {

            speed = maxMovementSpeed;
            runTrail.emitting = false;
        }
        if (Input.GetMouseButton(0) & canFire)
        {
            ShootBullet();

            soundFXManager?.PlaySound("Shoot");
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        RotateTowardsMouse();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void ShootBullet()
    {
        if (bulletPool != null)
        {
            StartCoroutine(FireRate());
            GameObject bullet = bulletPool.GetBullet();
            TrailRenderer bulletTrail = bullet.GetComponent<TrailRenderer>();
            bullet.transform.position = shootPosition[Random.Range(0, shootPosition.Length)].position;
            bullet.transform.rotation = shootPosition[Random.Range(0, shootPosition.Length)].rotation;
            bullet.SetActive(true);
            bulletTrail.Clear();
        }
    }

    private void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundLayer))
        {
            Vector3 targetPoint = hitInfo.point;
            Vector3 direction = (targetPoint - transform.position);
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private IEnumerator RefillHealthCapacity()
    {
        while (health < 100 & dashSystem.isFilling)
        {
            health = Mathf.Min(health + 1, 100);
            yield return new WaitForSeconds(0.8f);
        }
    }

    private IEnumerator FireRate()
    {
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    public void EnableHealthRegen()
    {
        StartCoroutine(RefillHealthCapacity());
    }
}
