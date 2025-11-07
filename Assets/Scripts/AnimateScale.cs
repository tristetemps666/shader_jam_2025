using System.Collections;
using UnityEngine;

[ExecuteAlways]
public class AnimateScale : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    AnimationCurve _scaleAnimation;

    [SerializeField]
    private RectTransform _rectTransform;

    [SerializeField]
    float _animationSpeed = 1f;

    bool _isRunning = false;

    public void PlayAnimation()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        Debug.Log("Test");
        StartCoroutine(UpdateAnimation());
    }

    void Enable()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() { }

    [ContextMenu("TestAnimation")]
    private void TestAnimation()
    {
        StartCoroutine(UpdateAnimation());
    }

    private IEnumerator UpdateAnimation()
    {
        float t = 0;
        _isRunning = true;
        while (t <= 1f)
        {
            t += Time.deltaTime * _animationSpeed;
            float scale = _scaleAnimation.Evaluate(t);

            if (!gameObject.activeSelf)
            {
                continue;
            }

            if (_rectTransform != null)
            {
                _rectTransform.localScale = Vector3.one * scale;
            }
            else
            {
                transform.localScale = Vector3.one * scale;
            }

            yield return null;
        }
        _isRunning = false;
    }
}
