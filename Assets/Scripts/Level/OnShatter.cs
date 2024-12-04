using Player;
using UnityEngine;

public class OnShatter : MonoBehaviour
{
    public GameObject cubePrefab; // 碎片预制体
    public Vector3 cubeSize = new Vector3(1, 1, 1);
    public float shatterDelay = 5f;
    private PlayerController playerController;

    [Header("呼吸效果")]
    public Color normalEmissionColor = Color.black; // 正常发光颜色
    public Color highlightEmissionColor = Color.yellow; // 高亮发光颜色
    public float breathSpeed = 2f; 
    private Renderer objectRenderer; 
    private Material objectMaterial; 

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            objectMaterial = objectRenderer.material;
        }
    }

    private void Update()
    {
        if (objectMaterial != null)
        {
            float lerpValue = Mathf.PingPong(Time.time * breathSpeed, 1f); 
            Color emissionColor = Color.Lerp(normalEmissionColor, highlightEmissionColor, lerpValue);

            objectMaterial.SetColor("_EmissionColor", emissionColor);
            objectMaterial.EnableKeyword("_EMISSION");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController != null && playerController.currentSizeState == SizeState.Large)
            {
                AudioManaager.Instance.Play("stone");
                Shatter();
            }
        }
    }

    private void Shatter()
    {
        Bounds bounds = GetComponent<Renderer>().bounds;

        int divisionsX = Mathf.CeilToInt(bounds.size.x / cubeSize.x);
        int divisionsY = Mathf.CeilToInt(bounds.size.y / cubeSize.y);
        int divisionsZ = Mathf.CeilToInt(bounds.size.z / cubeSize.z);

        for (int x = 0; x < divisionsX; x++)
        {
            for (int y = 0; y < divisionsY; y++)
            {
                for (int z = 0; z < divisionsZ; z++)
                {
                    Vector3 cubePosition = new Vector3(
                        bounds.min.x + (cubeSize.x * (x + 0.5f)),
                        bounds.min.y + (cubeSize.y * (y + 0.5f)),
                        bounds.min.z + (cubeSize.z * (z + 0.5f))
                    );

                    if (IsPointInsideMesh(cubePosition))
                    {
                        GameObject cube = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
                        cube.transform.localScale = cubeSize;
                        Rigidbody rb = cube.AddComponent<Rigidbody>();
                        rb.AddExplosionForce(Random.Range(100, 300), cubePosition, 5f);

                        Destroy(cube, shatterDelay);
                    }
                }
            }
        }

        gameObject.SetActive(false);
    }

    private bool IsPointInsideMesh(Vector3 point)
    {
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            Debug.LogWarning("MeshCollider is required for accurate inside check.");
            return false;
        }

        Vector3 closestPoint = meshCollider.ClosestPoint(point);
        return closestPoint == point;
    }
}
