using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLoby : MonoBehaviour
{
    void GoToMenu()
    {
        SceneManager.LoadScene("Loby");
    }

    void GoToDeneb()
    {
        SceneManager.LoadScene("Deneb");
    }

    void GoTotnshi()
    {
        SceneManager.LoadScene("tn-shi");
    }
}
