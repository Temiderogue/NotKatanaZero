using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource _audioSource;

    private static AudioClip _jumpSound, _dashSound, _pointSound;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _jumpSound = Resources.Load<AudioClip>("Jump");
        _dashSound = Resources.Load<AudioClip>("Dash");
        _pointSound = Resources.Load<AudioClip>("Point");
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Jump":
                _audioSource.PlayOneShot(_jumpSound);
                break;
            case "Dash":
                _audioSource.PlayOneShot(_dashSound);
                break;
            case "Point":
                _audioSource.PlayOneShot(_pointSound);
                break;
        }
    }
}
