using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public static class Loader  {

    public static AsyncOperation asyncOperation;
    public enum Scene {
        MainMenuScene,
        GameScene,
        LoadingScene,
    }

    private static Scene targetScene;
    // private static TextMeshProUGUI progressText;



    public static async void Load(Scene targetScene) {
        // ����Ŀ�곡��
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }


    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }

 }
