using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScenario : MonoBehaviour
{
    [SerializeField] Progress progress;
    [SerializeField] SceneNames nextScene;

    private void Awake()
    {
        SystemSetUp();
    }
    void SystemSetUp()
    {
        //�ػ� ����(1960,1080)
        int width = Screen.width;
        int height = Screen.height;
        Debug.Log(height);
        Screen.SetResolution(width, height, true);

        //ȭ���� �������ʵ��� ����
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        progress.Play(OnAfterProgress);
    }
    void OnAfterProgress()
    {
        BuildScene.LoadScene(nextScene);
    }
}
