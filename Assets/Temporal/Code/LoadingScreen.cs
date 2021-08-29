using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_Text progressText;

    private Canvas canvas;
    private GraphicRaycaster raycaster;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        
        canvas = GetComponent<Canvas>();
        raycaster = GetComponent<GraphicRaycaster>();
    }

    public void StartLoading()
    {
        progressText.SetText("0%");
        canvas.enabled = true;
        raycaster.enabled = true;
    }
    
    public void EndLoading()
    {
        canvas.enabled = false;
        raycaster.enabled = false;
        progressText.SetText("0%");
    }

    public void UpdateProgress(int progress)
    {
        progressText.SetText($"{progress}%");
    }
}