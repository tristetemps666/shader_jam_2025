using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource _audioSource;

    [SerializeField]
    List<AudioClip> _audioClips = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void PlaySound(string soundName)
    {
        if (_audioSource.isPlaying)
        {
            return;
        }
        var clipToPlay = _audioClips.Find(x => x.name.Equals(soundName));

        _audioSource.PlayOneShot(clipToPlay);
    }
}
