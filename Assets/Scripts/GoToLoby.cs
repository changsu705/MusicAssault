using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLoby : MonoBehaviour
{
    public void goToLoby()
    {
        SceneManager.LoadScene("Loby");
    }

    public void goToDeneb()
    {
        SceneManager.LoadScene("Deneb");
    }

    public void goTotnshi()
    {
        SceneManager.LoadScene("tn-shi");
    }
}
