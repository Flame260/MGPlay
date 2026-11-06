using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip shootSound;
    public AudioClip enemyDieSound;
    public AudioClip hurtSound;
    public AudioClip powerUpSound;
    public AudioClip gameOverSound;

    AudioSource source;

    void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip, float volume)
    {
        if (source == null || clip == null) return;
        source.PlayOneShot(clip, volume);
    }
}
