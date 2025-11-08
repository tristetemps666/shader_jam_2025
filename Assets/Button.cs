using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour
{
    private Button _button;
    private TextMeshProUGUI _textMeshPro;
    private AudioSource _audioSource;
    private CanvasGroup _canvasGroup;

    [SerializeField] AudioClip[] clips;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _button = GetComponent<Button>();
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        _canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        _audioSource.volume = _canvasGroup.alpha - 0.5f;
    }

    public void HoveringStyle()
    {
        _audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        _textMeshPro.fontStyle = FontStyles.Bold;
        _textMeshPro.fontSize = 28;
    }

    public void BasicStyle()
    {
        _textMeshPro.fontStyle = FontStyles.Normal;
        _textMeshPro.fontSize = 24;
    }



}
