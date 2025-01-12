using UnityEngine;

public class WallTransparencyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float transparentAlpha = 0.3f;

    private Camera cam;
    private Material originalMaterial;
    private GameObject lastWall;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        HandleWallTransparency();
    }

    private void HandleWallTransparency()
    {
        Ray ray = new Ray(cam.transform.position, player.position - cam.transform.position);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, wallLayer))
        {
            GameObject hitWall = hit.collider.gameObject;

            if (hitWall != lastWall)
            {
                ResetLastWall();

                Renderer renderer = hitWall.GetComponent<Renderer>();
                if (renderer != null)
                {
                    originalMaterial = renderer.material;
                    Material transparentMaterial = new Material(originalMaterial);
                    Color color = transparentMaterial.color;
                    color.a = transparentAlpha;
                    transparentMaterial.color = color;

                    renderer.material = transparentMaterial;
                    lastWall = hitWall;
                }
            }
        }
        else
        {
            ResetLastWall();
        }
    }

    private void ResetLastWall()
    {
        if (lastWall != null)
        {
            Renderer renderer = lastWall.GetComponent<Renderer>();
            if (renderer != null && originalMaterial != null)
            {
                renderer.material = originalMaterial;
            }
            lastWall = null;
        }
    }
}