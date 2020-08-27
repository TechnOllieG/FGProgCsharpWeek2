using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Shootable : MonoBehaviour
{
    public ShootableObject shootableScriptObject;

    #region ScriptableObjectVars
    private int _startHitPoints;
    private AudioClip _destroyAudio;
    private AudioMixerGroup _mixerGroup;
    private int _secsUntilDestroy;
    #endregion
    
    private int _currentHitPoints;
    private AudioSource _destroyAudioSource;

    private void Awake()
    {
        _startHitPoints = shootableScriptObject.startHitPoints;
        _destroyAudio = shootableScriptObject.destroyAudio;
        _mixerGroup = shootableScriptObject.mixerGroup;
        _secsUntilDestroy = shootableScriptObject.secsUntilDestroy;
       
        _currentHitPoints = _startHitPoints;
        _destroyAudioSource = GetComponent<AudioSource>();
        
        SetUpAudioSource();
    }

    private void SetUpAudioSource()
    {
        _destroyAudioSource.clip = _destroyAudio;
        _destroyAudioSource.outputAudioMixerGroup = _mixerGroup;
        _destroyAudioSource.playOnAwake = false;
    }

    public int OnHit(int damage)
    {
        _currentHitPoints -= damage;
        if (_currentHitPoints <= 0)
        {
            _destroyAudioSource.Play();
            StartCoroutine(Destroy());
        }

        return _startHitPoints;
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_secsUntilDestroy);
        Destroy(gameObject);
    }
}
