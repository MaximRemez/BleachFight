using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Variables

    public static AudioManager instance { get; private set; }
    private AudioSource source;

    #endregion

    #region Basic_Method

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

    }

    #endregion

    #region Custom_Method

    //запуск музики
    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

    #endregion

}
