using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum SceneNames { loading =0, Lobby, Defense }
public class BuildScene
{
  public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }
    public static void LoadScene(string sceneName = "")
    {
        if(sceneName == "")
        {
            SceneManager.LoadScene(GetActiveScene());
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    public static void LoadScene(SceneNames sceneName)
    {
        //���������� �Ű������� �޾ƿ� ��� Tostring() ó��
        SceneManager.LoadScene(sceneName.ToString());
    }
}
