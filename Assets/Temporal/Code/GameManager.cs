using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void OnLevelClicked(int level)
    {
        StartCoroutine(LoadLevelAsync("GameplayScene"));
    }

    private IEnumerator LoadLevelAsync(string scene)
    {
        var op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            Debug.Log(op.progress.ToString());
            yield return null;
        }

        Debug.Log("Level loaded");

        yield return new WaitForSeconds(2f);
        op.allowSceneActivation = true;
    }
}