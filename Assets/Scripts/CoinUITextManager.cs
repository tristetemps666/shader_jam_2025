using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinUITextManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private TextMeshProUGUI _coinTMP;
    void Start()
    {
        _coinTMP = GetComponent<TextMeshProUGUI>();
        _coinTMP.text = "0";

    }

    public void UpdateCoinText(int? newCoinAmmount)
    {
        _coinTMP.text = newCoinAmmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
