using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public Text ScoreUI;

    void Update()
    {
        ScoreUI.text = "Menos killed: " + score.ToString();
    }

    //зміна счету вбивств
    public void Kill()
    {
        score++;
    }
}
