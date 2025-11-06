using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    public UnityEvent OnCollected = new UnityEvent();

    [SerializeField]
    private int _value = 10;

    [SerializeField]
    Renderer _collectedRenderer;

    [SerializeField]
    private Transform _spawnCollectibleParticles;

    private bool _collected = false;

    public int GetValue() => _value;

    private void OnTriggerEnter(Collider other)
    {
        var collidedPlayerCollectibleManager = other.GetComponent<PlayerCollectibleManager>();
        if (collidedPlayerCollectibleManager != null && !_collected)
        {
            collidedPlayerCollectibleManager.CollectCoin(
                _value,
                _spawnCollectibleParticles ?? transform
            );

            _collected = true;

            OnCollected.Invoke();

            _collectedRenderer.enabled = false;
            Destroy(gameObject, 10f);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _collectedRenderer = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update() { }
}
