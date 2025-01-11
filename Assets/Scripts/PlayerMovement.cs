using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float rotationSpeed = 120.0f;
    private float wheelRotationSpeed = 200.0f;

    private float moveInput;
    private float rotationInput;

    [SerializeField] private GameObject[] leftWheel;
    [SerializeField] private GameObject[] rightWheel;

    private Rigidbody rb;



    private float fireRate = 0.5f;

    [Space]
    public Transform shootPosition;
    public BulletPool bulletPool;
    [Space]
    [SerializeField] private SoundFXManagerPlayer soundFXManager;
    [SerializeField] private Slider healthSlider;
    private HealthComponent healthComponent;

    

    private bool canFire = true;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthSlider.maxValue = 100;

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

        moveInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");

        TankWheelRotation(moveInput, rotationInput);


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
        healthSlider.maxValue = 100;
        healthSlider.value = healthComponent.CurrentHealth;

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
