using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LoadingScreen loadingScreen;

    public static GameManager Instance { get; private set; }
    
    private int maxFps = 60;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;

        LimitFps();
        DOTween.SetTweensCapacity(10000, 50);
    }

    public void OnLevelClicked(int level)
    {
        loadingScreen.StartLoading();
        StartCoroutine(LoadLevelAsync("GameplayScene"));
    }

    private IEnumerator LoadLevelAsync(string scene)
    {
        var op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        // loadingScreen.UpdateProgress((int)(op.progress * 100f));

        while (!op.isDone)
        {
            // loadingScreen.UpdateProgress((int)(op.progress * 100f));
            yield return null;
        }


        // GameplayManager.Instance.LoadLevels();

        // yield return new WaitForSeconds(2f);
        // op.allowSceneActivation = true;
    }

    public void SetProgress(float progress)
    {
        loadingScreen.UpdateProgress((int)progress);
        
        if (progress != 100) return;

        StartCoroutine(OnLoadLevelEnded());
    }

    private IEnumerator OnLoadLevelEnded()
    {
        var op = SceneManager.UnloadSceneAsync("TitleScene");
        
        while (!op.isDone)
            yield return null;

        loadingScreen.EndLoading();
    }

    private void LimitFps()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = maxFps;
    }
}