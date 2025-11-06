using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MovingCharacter : MonoBehaviour
{
    private Camera MainCamera;
    private NavMeshAgent navmeshagent;
    [SerializeField]
    private Transform SharkTransform;

    private float MovingSpeed;

    private Material materialshark;
    [SerializeField]
    private Renderer _renderer;

    private bool IsMoving;
    private float speedacceleration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainCamera = Camera.main;
        navmeshagent = GetComponent<NavMeshAgent>();
        materialshark = _renderer.material;

    }

    // Update is called once per frame
    void Update()
    {
        MovingSpeed = -(Vector3.Dot(SharkTransform.forward, navmeshagent.velocity));
        Debug.Log(MovingSpeed);

        /*
        IsMoving = CheckIfMoving();
        if (IsMoving)
        {
            speedacceleration += 0.1f;
        } */

        Debug.Log(CalculateSpeed());

        //materialshark.SetFloat("_MoveSpeed_1", Mathf.Lerp());

        var mouseRay = MainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mouseRay.origin, mouseRay.direction * 100f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit, 100f, LayerMask.GetMask("Ground")))
            {
            var Destination = hit.point;
            Debug.DrawRay(Destination, Vector3.up * 3f, Color.red);
            navmeshagent.SetDestination(Destination);
        }

        RaycastHit normalhit;
        if (Physics.Raycast(SharkTransform.position, Vector3.down, out normalhit, 100f, LayerMask.GetMask("Ground")))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(SharkTransform.up,normalhit.normal) * SharkTransform.rotation;
            SharkTransform.rotation = Quaternion.Slerp(SharkTransform.rotation, targetRotation, Time.deltaTime * 10f);
        }

    }


    public bool CheckIfMoving()
    {
        if(MovingSpeed != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float CalculateSpeed()
    {
        IsMoving = CheckIfMoving();
        if (IsMoving && speedacceleration <= 3.5f)
        {
            speedacceleration += 0.1f * Time.deltaTime;
        }
        else
        {
            if(speedacceleration >= 0)
            {
                speedacceleration -= 0.1f * Time.deltaTime;
            }
        }

        //float currentacceleratio
        


        //Mathf.Lerp()

        return speedacceleration;

    }
}
