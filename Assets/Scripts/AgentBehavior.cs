using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AgentBehavior : MonoBehaviour
{
    private Camera _camera;

    [SerializeField]
    private InputActionMap _duckGameplayActions;

    [SerializeField]
    NavMeshAgent _duckNavMeshAgent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnEnable()
    {
        _duckGameplayActions.Enable();
    }

    void Start()
    {
        _camera = Camera.main;
        _duckGameplayActions["DuckClick"].performed += SelectDestination;
    }

    // public void ClickDuck(InputAction.CallbackContext context)
    // {
    //     if (context.started) { }
    //     else if (context.performed)
    //     {
    //         SelectDestination();
    //     }
    //     else if (context.canceled) { }
    // }

    // Update is called once per frame
    void Update() { }

    private void SelectDestination(InputAction.CallbackContext context)
    {
        Debug.Log("click");
        var mouseRay = _camera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(mouseRay.origin, mouseRay.direction * 5f, Color.blue, 2f);

        RaycastHit hit;
        if (
            Physics.Raycast(
                mouseRay.origin,
                mouseRay.direction,
                out hit,
                100f,
                LayerMask.GetMask("Ground")
            )
        )
        {
            var hitPosition = hit.point;
            Debug.DrawRay(hitPosition, Vector3.up * 3f, Color.red, 0.5f);

            Vector3 newDestination = hitPosition;
            newDestination.z = transform.position.z;

            _duckNavMeshAgent.SetDestination(hitPosition);
        }
    }
}
