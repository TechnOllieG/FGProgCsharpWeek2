using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Shootable Data", menuName = "Shootable ScriptableObject")]
public class ShootableObject : ScriptableObject
{
    public int startHitPoints;
    public AudioClip destroyAudio;
    [Tooltip("The mixer group the destroy sound should be played through")]
    public AudioMixerGroup mixerGroup;
    [Tooltip("Specifies the amount of seconds it will take before the object is destroyed when hitpoints are 0 or lower.")]
    public int secsUntilDestroy;
}
