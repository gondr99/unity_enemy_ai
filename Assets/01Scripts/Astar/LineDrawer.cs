using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawLine(Vector3[] points)
    {   
        _lineRenderer.positionCount = points.Length;
        _lineRenderer.SetPositions(points);
    }
}
