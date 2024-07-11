using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(AlarmTrigger))]
public class AlarmSoundPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    private AlarmTrigger _alarmTrigger;
    private float _volumeChangeRate = 0.1f;
    private bool _isPlaying = false;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.volume = _minVolume;
        _alarmTrigger = GetComponent<AlarmTrigger>();
    }

    private void OnEnable()
    {
        _alarmTrigger.FrontEntered += OnFrontEntered;
        _alarmTrigger.BackEntered += OnBackEntered;
    }

    private void OnDisable()
    {
        _alarmTrigger.BackEntered -= OnBackEntered;
        _alarmTrigger.FrontEntered -= OnFrontEntered;
    }

    private void Update()
    {
        if (_isPlaying)
        {
            if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }

            if (_audioSource.volume < _maxVolume)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume,
                    _volumeChangeRate * Time.deltaTime);
            }
        }
        else if (_audioSource.isPlaying)
        {
            if (_audioSource.volume > _minVolume)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume,
                    _volumeChangeRate * Time.deltaTime);
            }
            else
            {
                _audioSource.Stop();
            }
        }
    }

    private void OnFrontEntered()
    {
        _isPlaying = true;
    }

    private void OnBackEntered()
    {
        _isPlaying = false;
    }
}
