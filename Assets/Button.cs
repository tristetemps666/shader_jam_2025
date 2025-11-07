using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour
{
    private Button _button;
    private TextMeshProUGUI _textMeshPro;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button = GetComponent<Button>();
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HoveringStyle()
    {
        _textMeshPro.fontStyle = FontStyles.Bold;
        _textMeshPro.fontSize = 28;
    }

    public void BasicStyle()
    {
        _textMeshPro.fontStyle = FontStyles.Normal;
        _textMeshPro.fontSize = 24;
    }



}
