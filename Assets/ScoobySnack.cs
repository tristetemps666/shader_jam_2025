using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ScoobySnack : MonoBehaviour
{
    ParticleSystem _particlesystem;
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] ParticleSystem _fishrenderer; //deprecated
    NavMeshAgent _agent;
    [SerializeField] Mesh[] fishmeshes;
    [SerializeField] MeshFilter _meshFilter;

    bool IsActive = true;
    

    // Start iscalled once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _particlesystem = GetComponentInChildren<ParticleSystem>();
        _agent = GetComponentInChildren<NavMeshAgent>();
        _particlesystem.Stop();
        _meshFilter.mesh = fishmeshes[Random.Range(0, fishmeshes.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
        if(_agent.hasPath == false)
        {
            _agent.SetDestination(RandomPointOnNavmesh(transform.position, 20f));
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        MovingCharacter othergameobject = other.GetComponentInParent<MovingCharacter>();


        if(IsActive == true && othergameobject.IsAbletoEat )
        {
            othergameobject.IncrementeScoring();
            _particlesystem.Play();
            _meshRenderer.enabled = false;
            _agent.isStopped = true;
            Destroy(_fishrenderer, 0.3f);

            Destroy(gameObject, 10);
            IsActive = false;
        }
    }


    private static Vector3 RandomPointOnNavmesh(Vector3 center, float radius)
    {
        // 1. Point aléatoire dans une sphère
        Vector3 randomPos = center + Random.insideUnitSphere * radius;

        // 2. Projection sur le NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position; // => point navmesh valable
        }

        return center; // fallback (rarement utilisé)
    }
}



