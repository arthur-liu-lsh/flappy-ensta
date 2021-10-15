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

    IEnumerator GetGlobalHighScore(){
        UnityWebRequest www = UnityWebRequest.Get("https://flappy.data-ensta.fr/highscore");
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            globalHighScore =  JsonUtility.FromJson<ScoreModel>(www.downloadHandler.text).highscore;
        }
    }

    IEnumerator GetSelfHighScore(){
        UnityWebRequest www = UnityWebRequest.Get("https://flappy.data-ensta.fr/score");
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            selfHighScore =  JsonUtility.FromJson<ScoreModel>(www.downloadHandler.text).highscore;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        highScoreText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(GetGlobalHighScore());
        StartCoroutine(GetSelfHighScore());
    }

    void Update()
    {
        highScoreText.text = "Votre record : <color=red>" + selfHighScore.ToString() + "</color>\n" + "Meilleur record : <color=red>" + globalHighScore.ToString() + "</color>";
    }
}
