using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour, ISceneLoader
{
    private int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadLevel(int sceneIndex)
    {
        if (currentSceneIndex == 0)
        {
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            currentSceneIndex = sceneIndex;
        }
        else
        {
            SceneManager.UnloadSceneAsync(currentSceneIndex);
            currentSceneIndex = 0;
        }
    }
}
