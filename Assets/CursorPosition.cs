using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class CursorPosition : MonoBehaviour
{
    private Camera MainCamera;
    public Vector3 offset;
    [SerializeField] RenderTexture rendertexture;
    [SerializeField] VisualEffect traileffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainCamera = Camera.main;
        traileffect = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        var mouseRay = MainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit, 100f, LayerMask.GetMask("Ground")))
        {
            var Destination = hit.point;
            Debug.DrawRay(Destination, Vector3.up * 3f, Color.red);
            transform.position = Destination ;
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log("prout");
            
        }

    }

}
