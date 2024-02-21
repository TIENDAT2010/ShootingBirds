using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGUI;
    public GameObject gameGUI;


    public Dialog gameDialog;
    public Dialog pauseDialog;

    public Image fireRateFilled;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI killedCountingText;

    private Dialog m_curDialog;

    public Dialog CurDialog { get => m_curDialog; set => m_curDialog = value; }

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void ShowGameGUI(bool isShow)
    {
        if(gameGUI != null)
        {
            gameGUI.SetActive(isShow);
        }
        if(homeGUI != null)
        {
            homeGUI.SetActive(!isShow);
        }
    }

    public void UpdateTimer(string time)
    {
        if(timerText != null)
        {
            timerText.text = time;
        }
    }

    public void UpdateKilledCounting(int killed)
    {
        if(killedCountingText != null)
        {
            killedCountingText.text = "X" + killed.ToString();
        }
    }

    public void UpdateFireRate(float rate)
    {
        if(fireRateFilled != null)
        {
            fireRateFilled.fillAmount = rate;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        if(pauseDialog != null)
        {
            pauseDialog.Show(true);
            pauseDialog.UpdateDialog("GAME PAUSE", "BEST KILLED : X" + Prefs.bestScore);
            m_curDialog = pauseDialog;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        if(m_curDialog)
        {
            m_curDialog.Show(false);
        }
    }

    public void BacktoHome()
    {
        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReplayGame()
    {
        if(m_curDialog)
        {
            m_curDialog.Show(false);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Application.Quit();
    }
}
