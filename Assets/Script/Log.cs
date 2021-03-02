using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Log : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(RegisterLog("Jonas", 1, "93", "78"));
    }

    public IEnumerator RegisterLog(string playerName, string level, string fitness, string score)
    {

        // Create a form object 
        WWWForm form = new WWWForm();
        form.AddField("Jogador", playerName );
        form.AddField("Fase", level);
        form.AddField("Fitness", fitness);
        form.AddField("Pontuacao", score);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/rooz/registerUser.php",form)){
             yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError){

                Debug.Log( "Error: " + www.error );
            } else {

                Debug.Log(www.downloadHandler.text);
            }

        }

    }
}
