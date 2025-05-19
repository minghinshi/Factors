using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler instance;

    [SerializeField] private AudioSource clickAudioSource;
    [SerializeField] private AudioSource correctAudioSource;

    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip correctClip;

    private void Awake()
    {
        instance = this;
    }

    private void RandomizePitch(AudioSource audioSource)
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
    }

    public void PlayClick()
    {
        RandomizePitch(clickAudioSource);
        clickAudioSource.PlayOneShot(clickClip);
    }

    public void PlayCorrect()
    {
        correctAudioSource.PlayOneShot(correctClip);
    }
}
