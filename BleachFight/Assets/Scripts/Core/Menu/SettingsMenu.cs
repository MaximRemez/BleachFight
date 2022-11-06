using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Toggle Sound;
    private void Awake()
    {
        Sound.isOn = AudioListener.pause;
    }

    public void OpenMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    //перемикання звуків в грі
    public void StopSound()
    {
        if (Sound.isOn == true)
            AudioListener.pause = true;
        if (Sound.isOn == false)
        {
            AudioListener.pause = false;
        }
    }
}
