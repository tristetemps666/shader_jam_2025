using UnityEngine;

[ExecuteAlways]
public class TwoPointLine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private Transform _startLineTransform;

    [SerializeField]
    private Transform _endLineTransform;

    [SerializeField]
    private LineRenderer _lineRenderer;

    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (_startLineTransform == null || _endLineTransform == null)
        {
            return;
        }

        Vector3[] points = { _startLineTransform.position, _endLineTransform.position };
        _lineRenderer.SetPositions(points);
    }
}
