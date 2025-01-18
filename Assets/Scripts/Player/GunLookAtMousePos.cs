using UnityEngine;

public class GunLookAtMousePos : MonoBehaviour
{

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rotationSpeed = 10f;

    void Update()
    {
        RotateTowardsMouse();
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
}
