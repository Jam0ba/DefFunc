using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DashSystem : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f; 
    public Slider dashCapacitySlider; 
    [Space]
    public TrailRenderer dashTrail;

    private Rigidbody rb;
    public float dashCapacity = 100f;
    private Vector3 dashDirection;

    public bool isFilling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (dashCapacitySlider != null)
        {
            dashCapacitySlider.maxValue = 100;
            dashCapacitySlider.value = dashCapacity;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashCapacity > 30 & !isFilling)
        {
            StartCoroutine(SmoothDash());
            dashTrail.emitting = true;
        }
        dashCapacitySlider.value = dashCapacity;
    }

    private IEnumerator SmoothDash()
    {

        dashCapacity -= 30;

        if (dashCapacitySlider != null)
            dashCapacitySlider.value = dashCapacity;

        dashDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        float elapsed = 0f;
        while (elapsed < dashDuration)
        {
            rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime);
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        dashTrail.emitting = false;


    }

    private IEnumerator RefillDashCapacity()
    {
        while (dashCapacity < 100 & isFilling)
        {
            dashCapacity = Mathf.Min(dashCapacity + 1, 100);
            yield return new WaitForSeconds(0.06f);
        }

        
    }

    public void EnableDashRegen()
    {
        StartCoroutine(RefillDashCapacity());
    }
}
