using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 8f;
    private float rotationSpeed = 120.0f;
    private float wheelRotationSpeed = 800.0f;
    private float fireRate = 0.5f;

    private float moveInput;
    private float rotationInput;

    private bool canFire = true;

    private Rigidbody rb;
    private HealthComponent healthComponent;

    [SerializeField] private GameObject[] leftWheel;
    [SerializeField] private GameObject[] rightWheel;

    [Space]
    public Transform shootPosition;
    public BulletPool bulletPool;
    [Space]
    [SerializeField] private SoundFXManagerPlayer soundFXManager;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private ParticleSystem exhaust;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthComponent = GetComponent<HealthComponent>();
        soundFXManager = GetComponentInChildren<SoundFXManagerPlayer>();

        healthSlider.maxValue = healthComponent.MaxHealth;

    }
    private void Update()
    {
        MovementInput();
        MouseInput();

    }
    private void FixedUpdate()
    {
        MoveTank(moveInput);
        RotateTank(rotationInput);
    }
    private void MoveTank(float input)
    {
        Vector3 moveDirection = transform.forward * input * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);

    }
    private void RotateTank(float input)
    {
        float rotation = input * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
    private void TankWheelRotation(float moveInput, float rotationInput)
    {
        float wheelRotation = moveInput * wheelRotationSpeed * Time.deltaTime;

        //Left Wheels
        foreach(GameObject wheel in leftWheel)
        {
            if(wheel != null)
            {
                wheel.transform.Rotate(wheelRotation - rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
        }

        //Right Wheels
        foreach (GameObject wheel in rightWheel)
        {
            if (wheel != null)
            {
                wheel.transform.Rotate(wheelRotation + rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
        }
    }
    public void PlayHitSound()
    {
        if(healthComponent.CurrentHealth > 0.0f)
        {
            healthSlider.value = healthComponent.CurrentHealth;
            soundFXManager?.PlaySound("Hit");
        }  
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
    private void MouseInput()
    {
        if (Input.GetMouseButton(0) & canFire)
        {
            ShootBullet();
            soundFXManager?.PlaySound("Shoot");
        }
    }
    private void MovementInput()
    {
        moveInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");

        TankWheelRotation(moveInput, rotationInput);
    }
}
