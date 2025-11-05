using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaterialManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    Camera _camera;

    [SerializeField]
    Vector2 mousePosition2D;

    private Material _material;
    private Renderer _renderer;

    private void Awake()
    {
        _camera = Camera.main;
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

        // screen position of the object.
        Vector2 objectScreenPosition2D = _camera.WorldToScreenPoint(transform.position);

        _material.SetVector("_MouseScreenPosition", mousePosition2D);
        _material.SetVector("_objectScreenPosition", objectScreenPosition2D);

        if(_renderer.material != _material)
        {
            _material = _renderer.material;
        }
    }
}
