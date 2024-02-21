using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Bird[] birdPreFabs; 

    public float spawnTime;

    public int timeLimit;

    private int m_curTimeLimit;
    private int m_BirdKilled;

    private bool m_isGameOver;

    public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }
    public int BirdKilled { get => m_BirdKilled; set => m_BirdKilled = value; }
    public int CurTimeLimit { get => m_curTimeLimit; set => m_curTimeLimit = value; }

    public override void Awake()
    {
        MakeSingleton(false);

        m_curTimeLimit = timeLimit;
    }

    public override void Start()
    {
        GameGUIManager.Ins.ShowGameGUI(false);
        GameGUIManager.Ins.UpdateKilledCounting(m_BirdKilled);
    }

    public void PlayGame()
    {
        StartCoroutine(GameSpawn());

        StartCoroutine(TimeCountDown());

        GameGUIManager.Ins.ShowGameGUI(true);
    }

    IEnumerator TimeCountDown()
    {
        while (m_curTimeLimit > 0)
        {
            yield return new WaitForSeconds(1f);

            m_curTimeLimit--;

            if (m_curTimeLimit <= 0)
            {
                m_isGameOver = true;

                if (m_BirdKilled > Prefs.bestScore)
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("NEW BEST", "BEST KILLED : X" + m_BirdKilled);
                }
                else
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("YOUR BEST", "BEST KILLED : X" + Prefs.bestScore);
                }

                Prefs.bestScore = m_BirdKilled;

                GameGUIManager.Ins.gameDialog.Show(true);
                GameGUIManager.Ins.CurDialog = GameGUIManager.Ins.gameDialog;

            }

            GameGUIManager.Ins.UpdateTimer(IntToTime(m_curTimeLimit));
        }
    }

    IEnumerator GameSpawn()
    {
        while (!m_isGameOver)
        {
            SpawnBird();
            yield return new WaitForSeconds(spawnTime);
        }
    }    

    void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;

        float randCheck = Random.Range(0f, 1f);

        if (randCheck >= 0.5f)
        {
            spawnPos = new Vector3(10, Random.Range(1.5f,4f),0);
        }
        else
        {
            spawnPos = new Vector3(-10, Random.Range(1.5f, 4f), 0);
        }    

        if (birdPreFabs != null && birdPreFabs.Length > 0)
        {
            int randIdx = Random.Range(0, birdPreFabs.Length);

            if (birdPreFabs[randIdx] != null)
            {
                Bird birdClone = Instantiate(birdPreFabs[randIdx],spawnPos,Quaternion.identity);
            }
        }    
    }
    

    private string IntToTime(int time)
    {
        float minute = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);

        return minute.ToString("00") + ":" + seconds.ToString("00");
    }
}
