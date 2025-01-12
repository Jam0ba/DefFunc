using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private LayerMask wallLayer;

    private Camera _cam;


    private void Start()
    {
        _cam = GetComponent<Camera>();
    }


    private void Update()
    {
        Vector2 cutoutPos = _cam.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

    }
}
