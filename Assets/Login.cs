using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public InputField usernameInput;
    public Button loginButton;

    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(() => {
            
            PlayerPrefs.SetString("playersName", usernameInput.text);
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
        });
    }


}
