using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    private PolygonCollider2D poly2D;
    private float fov;
    private Vector3 origin;
    private float startingAngle;
    private bool captureColliders;
    float timeStart;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        mesh.name = "Mesh";
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 30f;
        origin = Vector3.zero;
        poly2D = gameObject.AddComponent<PolygonCollider2D>();
        poly2D.isTrigger = true;
        captureColliders = false;
        timeStart = Time.time;
    }

    private void LateUpdate() {

        int rayCount = 30;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        float viewDistance = 10f;
        float timeNow = Time.time;

        if(timeNow - timeStart > 0.5f)
        {
            captureColliders = true;
        }

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector2[] vertices2D = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);

        for(int i = 0; i < vertices.Length; i++)
        {
            vertices2D[i] = new Vector2(vertices[i].x, vertices[i].y);
        }

        poly2D.points = vertices2D;
    }

    Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }
        return n;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    public void SetFoV(float fov)
    {
        this.fov = fov;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(captureColliders == true)
        {
            
        }
        
    }
}
