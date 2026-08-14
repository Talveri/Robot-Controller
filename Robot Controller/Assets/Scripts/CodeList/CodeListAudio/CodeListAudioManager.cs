using UnityEngine;
public class CodeListAudioManager : MonoBehaviour
{
    [Header("CodeList SFX")]
    [SerializeField] AudioClip nextCodeBlock;
    [SerializeField] AudioClip lastCodeBlock;
    [SerializeField] AudioClip addCodeBlock;
    [SerializeField] AudioClip switchCodeBlock;
    [SerializeField] AudioClip deleteCodeBlock;

    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        CodeListEvents.OnAddBlock += PlayAddCodeBlock;
        CodeListEvents.OnLastCodeBlock += PlayLastCodeBlock;
        CodeListEvents.OnNextCodeBlock += PlayNextCodeBlock;
        CodeListEvents.OnSwitchBlock += PlaySwitchCodeBlock;
        CodeListEvents.OnDeleteBlock += PlayDeleteCodeBlock;
    }

    void OnDisable()
    {
        CodeListEvents.OnAddBlock -= PlayAddCodeBlock;
        CodeListEvents.OnLastCodeBlock -= PlayLastCodeBlock;
        CodeListEvents.OnNextCodeBlock -= PlayNextCodeBlock;
        CodeListEvents.OnSwitchBlock -= PlaySwitchCodeBlock;
        CodeListEvents.OnDeleteBlock -= PlayDeleteCodeBlock;
    }

    void PlayNextCodeBlock() => source.PlayOneShot(nextCodeBlock);
    void PlayLastCodeBlock() => source.PlayOneShot(lastCodeBlock);
    void PlaySwitchCodeBlock() => source.PlayOneShot(switchCodeBlock);
    void PlayAddCodeBlock() => source.PlayOneShot(addCodeBlock);
    void PlayDeleteCodeBlock() => source.PlayOneShot(deleteCodeBlock);


}