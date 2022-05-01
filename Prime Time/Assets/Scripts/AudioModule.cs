using UnityEngine;

public class AudioModule : MonoBehaviour
{
    public static AudioModule instance;

    private AudioSource audioSource;

    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip correctClip;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void RandomizePitch()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
    }

    private void SetPitchToNormal()
    {
        audioSource.pitch = 1;
    }

    public void PlayClick()
    {
        RandomizePitch();
        audioSource.PlayOneShot(clickClip);
    }

    public void PlayCorrect()
    {
        SetPitchToNormal();
        audioSource.PlayOneShot(correctClip);
    }
}
