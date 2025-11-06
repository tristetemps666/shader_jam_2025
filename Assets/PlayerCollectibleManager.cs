using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollectibleManager : MonoBehaviour
{
    public UnityEvent<int?> OnCoinAmmountIncrease = new UnityEvent<int?>();

    [SerializeField, ReadOnly(true)]
    private int _totalCoinAmount = 0;

    public void CollectCoin(int coinValue, Transform coinPosition)
    {
        _totalCoinAmount += coinValue;
        OnCoinAmmountIncrease.Invoke(_totalCoinAmount);
    }


    private void Start() { }

    private void Update() { }
}
