using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

using TMPro;

public class MenuHighScore : MonoBehaviour
{

    private TextMeshProUGUI highScoreText;

    private int globalHighScore = -1;
    private int selfHighScore = -1;

    IEnumerator GetHighScores() {
        UnityWebRequest www = UnityWebRequest.Get(Application.absoluteURL + "scores");
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            ScoreGetModel scores =  JsonUtility.FromJson<ScoreGetModel>(www.downloadHandler.text);
            globalHighScore = scores.highscore;
            selfHighScore = scores.userscore;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        highScoreText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(GetHighScores());
    }

    void Update()
    {
        highScoreText.text = "Votre record : <color=red>" + selfHighScore.ToString() + "</color>\n" + "Meilleur record : <color=red>" + globalHighScore.ToString() + "</color>";
    }
}
