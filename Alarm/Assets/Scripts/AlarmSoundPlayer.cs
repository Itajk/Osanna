using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(AlarmTrigger))]
public class AlarmSoundPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    private AlarmTrigger _alarmTrigger;
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
        _alarmTrigger = GetComponent<AlarmTrigger>();
        _volumeChangeFrequency = Time.fixedDeltaTime;
    }

    private void OnEnable()
    {
        _alarmTrigger.ThiefEntered += OnThiefEntered;
        _alarmTrigger.ThiefLeft += OnThiefLeft;
    }

    private void OnDisable()
    {
        _alarmTrigger.ThiefEntered -= OnThiefEntered;
        _alarmTrigger.ThiefLeft -= OnThiefLeft;
    }

    private void OnThiefEntered()
    {
        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }

        if (_volumeChange != null)
        {
            StopCoroutine(_volumeChange);
        }

        _volumeChange = StartCoroutine(Increasing());
    }

    private IEnumerator Increasing()
    {
        WaitForSeconds wait = new WaitForSeconds(_volumeChangeFrequency);

        while (_audioSource.volume < _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume,
                _volumeChangeRate * _volumeChangeFrequency);

            yield return wait;
        }
    }

    private void OnThiefLeft()
    {
        if (_volumeChange != null)
        {
            StopCoroutine(_volumeChange);
        }

        _volumeChange = StartCoroutine(Fading());
    }

    private IEnumerator Fading()
    {
        WaitForSeconds wait = new WaitForSeconds(_volumeChangeFrequency);

        while (_audioSource.volume > _minVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume,
                _volumeChangeRate * _volumeChangeFrequency);

            yield return wait;
        }

        _audioSource.Stop();
    }
}
