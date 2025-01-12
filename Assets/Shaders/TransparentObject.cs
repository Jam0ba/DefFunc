using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    //Use with Shader in Assets\Shaders\Shader Graphs_TransparentWallShader
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


        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallLayer);

        for (int i = 0; i < hitObjects.Length; ++i)
        {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int m = 0; m < materials.Length; ++m)
            {
                materials[m].SetVector("_CutoutPos", cutoutPos);
                materials[m].SetFloat("_CutoutSize", 0.2f);
                materials[m].SetFloat("_FalloffSize", 0.05f);
            }
        }

    }
}
