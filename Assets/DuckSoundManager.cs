using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DuckSoundManager : MonoBehaviour
{
    [SerializeField]
    InputActionMap _duckSoundInput;

    [SerializeField]
    private List<AudioClip> _audioClips = new List<AudioClip>();

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private float _delayBetweenSounds = 1.5f;

    private float delayTime;

    public void EnableDuckSound() => _duckSoundInput.Enable();

    public void DisableDuckSound() => _duckSoundInput.Disable();

    void Start()
    {
        _duckSoundInput["DuckSound"].performed += PlayRandomDuckSound;
        _audioSource = GetComponent<AudioSource>();

        DisableDuckSound();
    }

    private bool CanPlaySound() => delayTime <= 0f;

    private IEnumerator DelaySoundActivation()
    {
        delayTime = _delayBetweenSounds;
        while (delayTime >= 0f)
        {
            delayTime -= Time.deltaTime;
            yield return null;
        }
    }

    private void PlayRandomDuckSound(InputAction.CallbackContext ctx)
    {
        if (!CanPlaySound())
        {
            return;
        }

        int randomIndex = Random.Range(0, _audioClips.Count);
        var duckSound = _audioClips[randomIndex];
        _audioSource.PlayOneShot(duckSound);

        StartCoroutine(DelaySoundActivation());
    }

    void Update() { }
}
