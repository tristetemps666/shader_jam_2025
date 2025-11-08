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

    public bool IsAbletoEat;

    [SerializeField] GameManager _gamemanager;

    [SerializeField]
    private ScoreManager scoremanager;

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
        //Debug.Log(MovingSpeed);

        if(_gamemanager.IsTitleScreen == true | _gamemanager.IsPauseMenu == true)
        {
            IsAbletoEat = false;
        }
        else { IsAbletoEat=true; }
        Debug.Log(IsAbletoEat);

        if(CheckIfMoving() == true)
        {
            materialshark.SetFloat("_IsMoving", 1f);
        }
        else
        {
            materialshark.SetFloat("_IsMoving", 0f);
        }

        var mouseRay = MainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mouseRay.origin, mouseRay.direction * 100f, Color.red);

        if (_gamemanager.IsPauseMenu == false && _gamemanager.IsTitleScreen == false )
        {

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
                Quaternion targetRotation = Quaternion.FromToRotation(SharkTransform.up, normalhit.normal) * SharkTransform.rotation;
                //targetRotation.y = SharkTransform.rotation.y;
                SharkTransform.rotation = Quaternion.Slerp(SharkTransform.rotation, targetRotation, Time.deltaTime * 10f);

            }
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

    public void IncrementeScoring()
    {
        scoremanager.score++;
    }



}
