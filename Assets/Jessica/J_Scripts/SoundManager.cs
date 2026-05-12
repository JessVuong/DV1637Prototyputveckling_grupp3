using UnityEngine;

public enum SoundType
{
    Flame,
    BraizerIgnite,
    WrongSequence,
    UnlockDoor,
    AjarClose,
    RopeCut,
    FireCannon

}

[RequireComponent(typeof(AudioSource))]

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;
    

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1, bool loop = false)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);

        if (loop) 
        {
            instance.audioSource.clip = instance.soundList[(int)sound];
            instance.audioSource.volume = volume;
            instance.audioSource.loop = true;
            instance.audioSource.Play();

        }


    }

//    public static void PlayLoop(SoundType sound, fl)
}
