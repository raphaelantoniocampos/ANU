using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance;

    public string sceneName;
    public int lvl;
    public bool scoreView = false;
    public GameObject score;

    private GameMaster gameMaster;
    private Vector2 startPosition;
    private GameObject audioManager;
    private TextMeshProUGUI scoreLvl;
    private TextMeshProUGUI scoreDeaths;
    private TextMeshProUGUI scoreTime;
    private TextMeshProUGUI deathsTotal;
    private TextMeshProUGUI timeTotal;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");

        sceneName = SceneManager.GetActiveScene().name;
        lvl = int.Parse(sceneName.Substring(sceneName.Length - 1, 1));

        instance = this;
    }

    void Update()
    {
        if (scoreView || lvl == 0)
        {
            if (Input.GetButtonDown("Submit"))
            {

                ChangeS();
            }
        }
    }

    public void ChangeS()
    {
        Destroy(audioManager);

        sceneName = SceneManager.GetActiveScene().name;
        lvl = int.Parse(sceneName.Substring(sceneName.Length - 1, 1));
        if (lvl >= 5)
        {
            lvl = -1;
        }
        SceneManager.LoadScene(lvl + 1);
        startPosition = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>().position;
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        gameMaster.lastCheckPointPos = startPosition;
        DeathsAndTime.running = true;
        Time.timeScale = 1;
        DeathsAndTime.totalDeath += DeathsAndTime.deathCount;
        DeathsAndTime.totalTime += DeathsAndTime.timeCount;
        DeathsAndTime.timeCount = 0;
        DeathsAndTime.deathCount = 0;
        GameObject.FindGameObjectWithTag("PauseGame").GetComponent<PauseGame>().gamePaused = false;
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void ScoreView()
    {

        sceneName = SceneManager.GetActiveScene().name;
        lvl = int.Parse(sceneName.Substring(sceneName.Length - 1, 1));

        var foundTextMeshObjects = FindObjectsByType<TextMeshProUGUI>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var textMesh in foundTextMeshObjects)
        {
            switch (textMesh.name)
            {
                case "scoreLvl":
                    scoreLvl = textMesh;
                    break;
                case "scoreDeaths":
                    scoreDeaths = textMesh;
                    break;
                case "scoreTime":
                    scoreTime = textMesh;
                    break;
            }
        }

        GameObject.FindGameObjectWithTag("PauseGame").GetComponent<PauseGame>().gamePaused = true;
        scoreLvl.text = $"Nivel {lvl}";
        scoreDeaths.text = DeathsAndTime.deathCount.ToString();
        scoreTime.text = DeathsAndTime.niceTime;
        Time.timeScale = 0;
        scoreView = true;
        score.SetActive(true);
        DeathsAndTime.running = false;
    }

    public void FinalScoreView()
    {

        sceneName = SceneManager.GetActiveScene().name;

        var audio = audioManager.GetComponent<AudioManager>();
        audio.Stop(sceneName);
        audio.Play("final");

        lvl = int.Parse(sceneName.Substring(sceneName.Length - 1, 1));

        var foundTextMeshObjects = FindObjectsByType<TextMeshProUGUI>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var textMesh in foundTextMeshObjects)
        {
            switch (textMesh.name)
            {
                case "scoreLvl":
                    scoreLvl = textMesh;
                    break;
                case "scoreDeaths":
                    scoreDeaths = textMesh;
                    break;
                case "scoreTime":
                    scoreTime = textMesh;
                    break;
                case "deathsTotal":
                    deathsTotal = textMesh;
                    break;
                case "timeTotal":
                    timeTotal = textMesh;
                    break;
            }
        }

        GameObject.FindGameObjectWithTag("PauseGame").GetComponent<PauseGame>().gamePaused = true;

        DeathsAndTime.totalDeath += DeathsAndTime.deathCount;
        DeathsAndTime.totalTime += DeathsAndTime.timeCount;

        scoreLvl.text = $"Nivel {lvl}";
        scoreDeaths.text = DeathsAndTime.deathCount.ToString();
        scoreTime.text = DeathsAndTime.niceTime;
        deathsTotal.text = DeathsAndTime.totalDeath.ToString();
        timeTotal.text = DeathsAndTime.niceTotalTime;
        Time.timeScale = 0;
        scoreView = true;
        score.SetActive(true);
        DeathsAndTime.running = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ScoreView();
        }
    }
}
