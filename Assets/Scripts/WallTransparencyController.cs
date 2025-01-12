using UnityEngine;

public class WallTransparencyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask wallLayer;
    [Space]
    [SerializeField] private float transparentAlpha = 0.3f;


    private Material originalMaterial;
    private GameObject lastWall;

    void Update()
    {
        HandleWallTransparency();
    }

    void HandleWallTransparency()
    {
        Ray ray = new Ray(Camera.main.transform.position, player.position - Camera.main.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, wallLayer))
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

    void ResetLastWall()
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
