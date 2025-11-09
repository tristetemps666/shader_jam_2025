using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSourceClairObscur;

    [SerializeField]
    private float _scrollPitchSensitivity = 0.5f;

    bool _isClairObscurRunning => _audioSourceClairObscur.isPlaying;

    float _pitchValue = 1.5f;

    float _currentSoundTimePosition = 0f;

    [SerializeField]
    private InputActionMap _clairObscurInput;

    [SerializeField]
    AudioSource _audioSource;

    [SerializeField]
    List<AudioClip> _audioClips = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _clairObscurInput.Enable();
        _clairObscurInput["ClairObscur"].performed += ToggleClairObscur;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isClairObscurRunning)
        {
            UpdatePitchOnScrollWheel(Input.mouseScrollDelta.y);
        }
    }

    public void PlaySound(string soundName)
    {
        if (_audioSource.isPlaying)
        {
            return;
        }
        var clipToPlay = _audioClips.Find(x => x.name.Equals(soundName));

        _audioSource.PlayOneShot(clipToPlay);
    }

    private void ToggleClairObscur(InputAction.CallbackContext ctx)
    {
        Debug.Log("CLAIIIIIIIIIR OBSCUUUUUUUUUUUR LALILA // " + _isClairObscurRunning);
        if (!_isClairObscurRunning)
        {
            _audioSourceClairObscur.Play();
            _audioSourceClairObscur.time = _currentSoundTimePosition;
        }
        else
        {
            _currentSoundTimePosition = _audioSourceClairObscur.time;
            _audioSourceClairObscur.Stop();
        }
    }

    void UpdatePitchOnScrollWheel(float deltaWheel)
    {
        _pitchValue += deltaWheel * _scrollPitchSensitivity;
        _pitchValue = Mathf.Clamp(_pitchValue, 0.3f, 2f);
        _audioSourceClairObscur.pitch = _pitchValue;
    }
}
