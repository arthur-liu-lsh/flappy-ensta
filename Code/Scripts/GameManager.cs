using System.Collections;
using System.Text;

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
        if (points > 0) {
            StartCoroutine(PostScore());
        }
    }

    // Generate hash from input using MD5 method
    public string CreateMD5(string input) {
        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
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
        string highscore = points.ToString();

        // Get password from Resources folder, need to create password.txt containing said password.
        string password = Resources.Load<TextAsset>("password").text;
        // Generate checksum using password and highscore
        // The server needs to do the same then compare the checksums
        string checksum = CreateMD5(password + highscore);
        form.AddField("highscore", highscore);
        form.AddField("checksum", checksum);

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
        Debug.Log(points);
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
