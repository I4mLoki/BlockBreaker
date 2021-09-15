using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LoadingScreen loadingScreen;

    [SerializeField]
    private int maxFps = 60;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;

        LimitFps();
    }

    private void LimitFps()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = maxFps;
    }

    public void OnLevelClicked(int level)
    {
        loadingScreen.StartLoading();
        StartCoroutine(LoadLevelAsync("GameplayScene", () => LoadGameplay(level)));
    }

    private IEnumerator LoadLevelAsync(string scene, Action callback)
    {
        var currentScene = SceneManager.GetActiveScene();

        var op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        loadingScreen.UpdateProgress((int)(op.progress*100f));

        while (!op.isDone)
        {
            loadingScreen.UpdateProgress((int)(op.progress*100f));
            yield return null;
        }

        loadingScreen.UpdateProgress((int)(op.progress*100f));
        
        StartCoroutine(OnLoadLevelEnded(currentScene, callback));
    }

    private IEnumerator OnLoadLevelEnded(Scene previousScene, Action callback)
    {
        callback.Invoke();
        
        var op = SceneManager.UnloadSceneAsync(previousScene);

        while (!op.isDone)
            yield return null;

        loadingScreen.EndLoading();
    }

    private void LoadGameplay(int level)
    {
        GameplayManager.Instance.StartGameplay(level);
    }
}