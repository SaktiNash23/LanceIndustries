using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using System;

public enum SCENE_ENUM
{
    MAIN_MENU = 0,
    LOADING_SCENE = 1,
    GAMEPLAY_SCENE = 2
}

public class SceneLoader : MonoBehaviour
{
    [BoxGroup("SINGLETON SETTINGS")] [SerializeField] bool dontDestroyOnLoad;
    
    private SCENE_ENUM targetScene;
    private LoadingProgressBar loadingProgressBar;

    private AsyncOperation sceneLoadingOperation = null;

    private static SceneLoader _instance;
    public static SceneLoader Instance { get => _instance; }

    private void Awake()
    {
        if(_instance != null)
        {
            if(_instance != this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            _instance = this;
            if(dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene sceneLoaded, LoadSceneMode loadSceneMode)
    {
        if (sceneLoaded.buildIndex == (int)SCENE_ENUM.LOADING_SCENE)
        {
            StartCoroutine(DelayAction(() =>
            {
                LoadSceneAsyncSingle(targetScene);
                loadingProgressBar = FindObjectOfType<LoadingProgressBar>();
                if (loadingProgressBar != null)
                {
                    StartCoroutine(LoadingProgressUpdate());
                }
            }, 0.5f)
            );
        }
    }

    public void LoadSceneWithLoadingScreen(SCENE_ENUM targetScene)
    {
        this.targetScene = targetScene;
        SceneManager.LoadScene((int)SCENE_ENUM.LOADING_SCENE, LoadSceneMode.Single);
    }

    public void LoadSceneWithLoadingScreen(int targetSceneIndex)
    {
        if(targetSceneIndex < Enum.GetValues(typeof(SCENE_ENUM)).Length)
        {
            this.targetScene = (SCENE_ENUM)targetSceneIndex;
        }
        else
        {
            Debug.LogError("DOESN'T HAVE A SCENE WITH INDEX " + targetSceneIndex + " IN BUILD SETTINGS.");
        }

        SceneManager.LoadScene((int)SCENE_ENUM.LOADING_SCENE, LoadSceneMode.Single);
    }

    public void LoadSceneAsyncSingle(SCENE_ENUM targetScene)
    {
        sceneLoadingOperation = SceneManager.LoadSceneAsync((int)targetScene, LoadSceneMode.Single);
        sceneLoadingOperation.allowSceneActivation = false;
    }

    private IEnumerator LoadingProgressUpdate()
    {
        bool stillLoading = true;

        while (stillLoading)
        {
            loadingProgressBar.UpdateProgressBar(sceneLoadingOperation.progress);
            if (sceneLoadingOperation.progress >= 0.9f)
            {
                loadingProgressBar.UpdateProgressBar(1f);
                stillLoading = false;
            }
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        sceneLoadingOperation.allowSceneActivation = true;
    }

    private IEnumerator DelayAction(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);

        action?.Invoke();
    }
}
