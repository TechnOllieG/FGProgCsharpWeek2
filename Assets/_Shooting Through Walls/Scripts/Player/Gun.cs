using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public int shotDamage;
    public AudioClip shotAudio;
    public AudioMixerGroup mixerGroup;
    
    private Transform _transform; // this.transform
    private RaycastHit[] _hits; // Array to hold all objects hit with the raycast when shooting
    private int _currentShotDamage;
    private AudioSource _shotAudioSource;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _shotAudioSource = GetComponent<AudioSource>();

        SetUpAudioSource();
    }

    private void SetUpAudioSource()
    {
        _shotAudioSource.clip = shotAudio;
        _shotAudioSource.outputAudioMixerGroup = mixerGroup;
        _shotAudioSource.playOnAwake = false;
    }
    
    public void Shoot()
    {
        _shotAudioSource.Play();
        _currentShotDamage = shotDamage;
        // Performs the raycast to check which colliders are in the way and saves to array
        _hits = Physics.RaycastAll(_transform.position, Vector3.left);

        // Prints the unsorted array in the console
        foreach (RaycastHit hit in _hits)
        {
            Debug.Log($"Distance to wall in unsorted array is: {hit.distance}");
        }
        
        // Sorts the array based on distance from this.transform.position
        for(int i = 0; i < _hits.Length; i++)
        {
            float shortestDistance = _hits[i].distance;
            int shortestIndex = i;
            
            for(int j = i; j < _hits.Length; j++)
            {
                float distance = _hits[j].distance;

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    shortestIndex = j;
                }
            }

            RaycastHit temp = _hits[i];
            _hits[i] = _hits[shortestIndex];
            _hits[shortestIndex] = temp;
        }

        // Prints the sorted array in the console
        foreach (RaycastHit hit in _hits)
        {
            Debug.Log($"Distance to wall in sorted array is: {hit.distance}");
        }

        foreach (RaycastHit hit in _hits)
        {
            _currentShotDamage -= hit.transform.gameObject.GetComponent<Shootable>().OnHit(_currentShotDamage);
            if (_currentShotDamage <= 0)
            {
                break;
            } 
        }
    }
}
