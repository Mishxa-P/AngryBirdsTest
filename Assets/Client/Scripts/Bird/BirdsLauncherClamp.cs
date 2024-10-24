using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BirdsLauncherClamp : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    public void DrawCatapultRope(Vector3 pullPosition)
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, pullPosition);
    }
    public void RemoveCatapultRope()
    {
        _lineRenderer.positionCount = 0;
    }
}
