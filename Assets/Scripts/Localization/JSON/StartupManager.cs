using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        while (!LocalizationManager.instance.GetReady())
        {
            yield return null;
        }
        SceneManager.LoadScene("New Scene 2");
    }
}
