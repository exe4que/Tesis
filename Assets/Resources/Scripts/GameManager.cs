using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static float[] bulletDamageTable = new float[] { 5f, 1f, 15f };

    private GameObject[] enemyBases;
    private Image[] enemyBasesIndicators;
    private Text clockText;
    public float roundTime = 180f;
    public int activeBases = 6;
    public GameObject pauseMenu, gameOver, levelComplete, helpMenu;
    public LayerMask playerMask, botMask;
    private UnityEngine.Object[] _weaponsList;

    private int _score;

    public int score
    {
        get { return _score; }
        set { _score = value; }
    }




    public UnityEngine.Object[] weaponsList
    {
        get { return _weaponsList; }
        set { _weaponsList = value; }
    }


    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        Application.targetFrameRate = -1;
        Time.timeScale = 1f;

        if (PlayerPrefs.GetInt("FirstPlay") == 0)
        {
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.SetInt("Sound", 1);
            PlayerPrefs.SetInt("FirstPlay", 1);
        }

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            this.enemyBases = GameObject.FindGameObjectsWithTag("EnemyBase");
            this.enemyBasesIndicators = GameObject.Find("MainCanvas/BasesPanel").GetComponentsInChildren<Image>();
            this.clockText = GameObject.Find("MainCanvas/TimeLeftText/ClockText").GetComponent<Text>();
            this.clockText.text = FormatTime(this.roundTime);
            EnemySpawnManager.instance.enemyBases = enemyBases.Length;
        }

        weaponsList = new GameObject[(Resources.LoadAll("Prefabs/Weapons").Length)];
        weaponsList = Resources.LoadAll("Prefabs/Weapons");
        _instance = this;

    }

    void Start()
    {
        
        if (SceneManager.GetActiveScene().buildIndex != 0)
            InitIndicators();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.Exit();
                Debug.Log("Quit");
            }
        }
        else
        {
            roundTime -= Time.deltaTime;
            this.clockText.text = roundTime >= 0 ? FormatTime(roundTime) : "00:00";
            this.enabled = roundTime > 0;

            if (roundTime <= 0 && !levelComplete.activeSelf)
            {
                this.GameOver();
            }

            if (EnemySpawnManager.instance.currentEnemies == 0 && this.activeBases == 0)
            {
                this.LevelComplete();
            }

            if (Input.GetKeyDown(KeyCode.Escape) && !gameOver.activeSelf && !levelComplete.activeSelf && !helpMenu.activeSelf)
            {
                GameManager.instance.Pause();
            }
        }
    }

    public void DeactivateBaseIndicator(int _instanceId)
    {
        for (int i = 0; i < enemyBases.Length; i++)
        {
            if (enemyBases[i].GetInstanceID() == _instanceId)
            {
                enemyBasesIndicators[i].GetComponent<IAnimable>().SetBool("TurningOff", true);
                _score += 30;
                activeBases--;
            }
        }
    }

    void InitIndicators()
    {
        for (int i = 0; i < enemyBasesIndicators.Length; i++)
        {
            if (i >= enemyBases.Length)
            {
                Image thisImage = enemyBasesIndicators[i].GetComponent<Image>();
                Color c = thisImage.color;
                c.a = 0.5f;
                thisImage.color = c;
                activeBases--;
            }
        }
    }

    public void AddPoints(int _value = 10)
    {
        _score += _value;
    }

    string FormatTime(float _seconds)
    {
        TimeSpan t = TimeSpan.FromSeconds(_seconds);

        string ret = string.Format("{0:D2}:{1:D2}",
                        t.Minutes,
                        t.Seconds);
        return ret;
    }

    public void Pause()
    {
        if (!helpMenu.activeSelf)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void PauseHiddenMenu()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        this.gameOver.SetActive(true);
    }

    public void GameOverDelayed()
    {
        Invoke("GameOver", 1f);
    }

    private void LevelComplete()
    {
        //Time.timeScale = 0;
        this.levelComplete.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
