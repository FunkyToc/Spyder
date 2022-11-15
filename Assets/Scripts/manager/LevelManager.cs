using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{


    public void GoLastCheckpoint()
    {

    }

    public void ReloadLevel()
    {

    }
    
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(SceneUtility.GetBuildIndexByScenePath(levelName));
    }
}
