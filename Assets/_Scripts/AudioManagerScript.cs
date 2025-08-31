using UnityEngine;


public class AudioManagerScript : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip casinoAmbience;
    public AudioClip stagesAmbience;

    public AudioClip slotRollingSFX;
    public AudioClip slotWinSFX;
    public AudioClip slotLoseSFX;
    public AudioClip gameOverSFX;
    public AudioClip headChefSFX;
    public AudioClip chefSFX;
    public AudioClip janitorSFX;
    public AudioClip WalkSFX;

    [Header("Not Filled")]
    public AudioClip EatSFX;
    public AudioClip CollectSFX;
    public AudioClip PillSFX;
    public AudioClip TakeDamageSFX;
    public AudioClip ActivateAbilitySFX;
    public AudioClip HideSFX;
    public AudioClip PoisonedSFX;
    public AudioClip OpenMenuSFX;
    

    private void Start()
    {
        bgmSource.ignoreListenerPause = true;
        bgmSource.ignoreListenerVolume = true;
        bgmSource.clip = backgroundMusic;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
