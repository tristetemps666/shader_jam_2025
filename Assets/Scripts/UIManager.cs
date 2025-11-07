using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public UnityEvent OnMainMenuFadeEnd = new UnityEvent();

    public UnityEvent OnInstructionFadeInEnd = new UnityEvent();

    [SerializeField]
    CanvasGroup _mainMenuCanvasGroup;

    [SerializeField]
    CanvasGroup _pauseMenuCanvasGroup;

    [SerializeField]
    CanvasGroup _gameCanvasGroup;

    [SerializeField]
    CanvasGroup _instructionsCanvasGroup;

    [SerializeField]
    float timeMainMenuFade = 2f;

    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void SetGameUI()
    {
        _mainMenuCanvasGroup.gameObject.SetActive(false);
        _pauseMenuCanvasGroup.gameObject.SetActive(false);

        _gameCanvasGroup.gameObject.SetActive(true);
    }

    public void SetPauseUI()
    {
        _mainMenuCanvasGroup.gameObject.SetActive(false);
        _gameCanvasGroup.gameObject.SetActive(false);
        _instructionsCanvasGroup.gameObject.SetActive(false);

        _pauseMenuCanvasGroup.gameObject.SetActive(true);
    }

    public void SetMainMenuUI()
    {
        _gameCanvasGroup.gameObject.SetActive(false);
        _pauseMenuCanvasGroup.gameObject.SetActive(false);
        _instructionsCanvasGroup.gameObject.SetActive(false);

        _mainMenuCanvasGroup.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        StartCoroutine(FadeGroup(_mainMenuCanvasGroup, false, OnMainMenuFadeEnd));

        Debug.Log("startFade");

        OnMainMenuFadeEnd.AddListener(() =>
            StartCoroutine(FadeGroup(_instructionsCanvasGroup, true, OnInstructionFadeInEnd))
        );
    }

    public void FadeOutInstruction()
    {
        StartCoroutine(FadeGroup(_instructionsCanvasGroup, false));
    }

    private IEnumerator FadeGroup(
        CanvasGroup groupToFade,
        bool fadeIn,
        UnityEvent? eventToCall = null
    )
    {
        float t = timeMainMenuFade;

        groupToFade.gameObject.SetActive(true);
        groupToFade.alpha = fadeIn ? 0f : 1f;

        while (t >= 0f)
        {
            t -= Time.deltaTime;
            float alpha = t / timeMainMenuFade;
            groupToFade.alpha = fadeIn ? 1 - alpha : alpha;
            yield return null;
        }

        if (eventToCall != null)
        {
            eventToCall.Invoke();
        }
    }
}
