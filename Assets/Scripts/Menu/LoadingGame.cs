using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    //loading panel
    public GameObject loadingPanel;

    //loading bar fill
    public Image loadingBarFill;

    //load scene
    public void LoadScene(int sceneIndex)
    {
        //show loading panel
        loadingPanel.SetActive(true);

        //start loading scene
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    //load scene async
    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        //load scene async
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        //wait until scene is loaded
        while (!operation.isDone)
        {
            //calculate progress
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            //set loading bar fill
            loadingBarFill.fillAmount = progress;

            //wait for a frame
            yield return null;
        }

        //hide loading panel
        loadingPanel.SetActive(false);
    }
}
