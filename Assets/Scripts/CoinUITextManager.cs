using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinUITextManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    GameManager _gameManager;

    [SerializeField]
    private TextMeshProUGUI _coinTMP;

    void Start()
    {
        _coinTMP = GetComponent<TextMeshProUGUI>();
        _coinTMP.text = "0" + "/" + _gameManager.ammountOfCoinToWin.ToString();
    }

    public void UpdateCoinText(int? newCoinAmmount)
    {
        _coinTMP.text =
            newCoinAmmount.ToString() + "/" + _gameManager.ammountOfCoinToWin.ToString();
    }

    // Update is called once per frame
    void Update() { }
}
