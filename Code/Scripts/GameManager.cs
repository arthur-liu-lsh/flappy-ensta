using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    // Spawning pipes
    public float pipeBoundary;
    public float pipeSpawnPos;
    public GameObject pipesPrefab;
    public float spawnInterval;
    private float lastSpawnTime;

    // Pause
    private bool isPaused = false;

    // Score counter
    private int points = 0;
    private TextMeshProUGUI scoreboard;

    // Game Over Menu
    public GameObject gameOverMenu;
    public TextMeshProUGUI highScoreText;
    private int selfHighScore = -1;
    private int globalHighScore = -1;



    float GenerateRandomPosition(float a, float b) { //Generate a random float between a and b
        return Random.Range(a, b);
    }

    void LoadSaveScore() { // Post and get score from server
        StartCoroutine(GetHighScores());
        StartCoroutine(PostScore());
    }



    IEnumerator GetHighScores() {
        UnityWebRequest www = UnityWebRequest.Get(Application.absoluteURL + "scores");
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            ScoreGetModel scores =  JsonUtility.FromJson<ScoreGetModel>(www.downloadHandler.text);
            globalHighScore = scores.highscore;
            int selfHighScoreRequest = scores.userscore;
            if (points < selfHighScoreRequest) {
                selfHighScore = selfHighScoreRequest;
            }
        }
    }

    IEnumerator PostScore() {
        WWWForm form = new WWWForm();
        Debug.Log(points.ToString());
        form.AddField("highscore", points.ToString());

        UnityWebRequest www = UnityWebRequest.Post(Application.absoluteURL + "score", form);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
    


    public void StopTime() {
        LoadSaveScore();
        ShowGameOverMenu();

        isPaused = true;
        Time.timeScale = 0f;
    }

    void ResumeTime() {
        isPaused = false;
        Time.timeScale = 1f;
    }

    void ReloadScene() {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    void SpawnPipes() { // Spawn pipes at random height
        float y = GenerateRandomPosition(-pipeBoundary, pipeBoundary);
        Instantiate(pipesPrefab, new Vector3(pipeSpawnPos, y, 0f), Quaternion.identity);
    }

    void SpawnPipes(float y) { // Spawn pipes at defined height
        Instantiate(pipesPrefab, new Vector3(pipeSpawnPos, y, 0f), Quaternion.identity);
    }

    void UpdateCounterText() {
        scoreboard.text = "Bon sens : " + points.ToString();
    }

    void ShowGameOverMenu() {
        gameOverMenu.SetActive(true);
    }


    void UpdateGameOverText() {
        highScoreText.text = "Votre score : <color=red>" + points.ToString() + "</color>\n" + "Votre record : <color=red>" + selfHighScore.ToString() + "</color>\n"
            + "Meilleur record : <color=red>" + globalHighScore.ToString();
    }

    public void AddPoint() {
        points += 1;
        selfHighScore = points;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get score counter and game over menu text
        scoreboard = GameObject.FindWithTag("Counter").GetComponent<TextMeshProUGUI>();

        // Spawn first pipe and update pipe spawn timer
        SpawnPipes(0f);
        lastSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawnTime > spawnInterval) {
            lastSpawnTime = Time.time;
            SpawnPipes();
        }
        UpdateCounterText();

        if (isPaused) {
            UpdateGameOverText();
            if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) {
                ResumeTime();
                ReloadScene();
            }
        }

    }
}
