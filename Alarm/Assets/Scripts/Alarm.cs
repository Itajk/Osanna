using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _volumeChangeRate = 0.1f;
    private float _volumeChangeFrequency;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private Coroutine _volumeChange;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.volume = _minVolume;
        _volumeChangeFrequency = Time.fixedDeltaTime;
    }

    public void TurnOn()
    {
        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }

        ChangeVolume(_maxVolume);
    }

    public void TurnOff()
    {
        ChangeVolume(_minVolume);
    }

    private void ChangeVolume(float volume)
    {
        if (_volumeChange != null)
        {
            StopCoroutine(_volumeChange);
        }

        _volumeChange = StartCoroutine(Changing(volume));
    }

    private IEnumerator Changing(float targetVolume)
    {
        WaitForSeconds wait = new WaitForSeconds(_volumeChangeFrequency);

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume,
                _volumeChangeRate * _volumeChangeFrequency);

            yield return wait;
        }

        if (targetVolume == _minVolume)
        {
            _audioSource.Stop();
        }
    }
}
