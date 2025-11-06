using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollectibleManager : MonoBehaviour
{
    public UnityEvent<Transform> OnCollectiblePicked = new UnityEvent<Transform>();

    [SerializeField, ReadOnly(true)]
    private int _totalCoinAmount = 0;

    public void CollectCoin(int coinValue, Transform coinPosition)
    {
        _totalCoinAmount += coinValue;
        OnCollectiblePicked.Invoke(coinPosition);
    }

    private void Start() { }

    private void Update() { }
}
