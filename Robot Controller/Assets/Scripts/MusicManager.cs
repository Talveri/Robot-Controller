using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundTrack;
    private AudioSource source;

    private static MusicManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
        source.loop = true;
    }
    private void Start()
    {
        PlayMusic();
    }


    private void PlayMusic()
    {
        source.clip = backgroundTrack;
        source.Play();
    }
}
