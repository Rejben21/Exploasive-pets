using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour
{
    public GameObject playButton, levels;

    public Text[] speedText, rangeText, bombsText;
    public PlayerController[] playersCon;
    public PlayerBombsController[] playersBomb;

    void Start()
    {
        
    }

    void Update()
    {
        if (playersCon.Length >= 2)
        {
            speedText[0].text = " " + playersCon[0].speedBonus.ToString();
            rangeText[0].text = " " + playersBomb[0].explosionRadius.ToString();
            bombsText[0].text = " " + playersBomb[0].bombAmount.ToString();


            speedText[1].text = " " + playersCon[1].speedBonus.ToString();
            rangeText[1].text = " " + playersBomb[1].explosionRadius.ToString();
            bombsText[1].text = " " + playersBomb[1].bombAmount.ToString();

            if (playersCon.Length >= 3)
            {
                speedText[2].text = " " + playersCon[2].speedBonus.ToString();
                rangeText[2].text = " " + playersBomb[2].explosionRadius.ToString();
                bombsText[2].text = " " + playersBomb[2].bombAmount.ToString();

                if (playersCon.Length == 4)
                {
                    speedText[3].text = " " + playersCon[3].speedBonus.ToString();
                    rangeText[3].text = " " + playersBomb[3].explosionRadius.ToString();
                    bombsText[3].text = " " + playersBomb[3].bombAmount.ToString();
                }
            }
        }
    }

    public void PlayButton()
    {
        AudioManager.instance.PlaySFX(0);
        playButton.SetActive(false);
        levels.SetActive(true);
    }

    public void TwoPlayers()
    {
        AudioManager.instance.PlaySFX(0);
        SceneManager.LoadScene(1);
    }

    public void ThreePlayers()
    {
        AudioManager.instance.PlaySFX(0);
        SceneManager.LoadScene(2);
    }

    public void FourPlayers()
    {
        AudioManager.instance.PlaySFX(0);
        SceneManager.LoadScene(3);
    }
}
