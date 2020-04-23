using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



//목표: UI 스코어, 크로스헤어 매니져를 생성하고 접근하여 데이터를 쉽게 쓰자~
public class SBS_UIManager : MonoBehaviour
{
    private static SBS_UIManager instance;
    
    public static SBS_UIManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<SBS_UIManager>();

            return instance;
        }
    }

 

    //목표: 게임 시작 화면으로 넘어가자 

    public void GameRestart()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Volcano1");
    }


    //목표: 게임 Play화면으로 넘어가자
    public void GameTitleStart()
    {
        //SceneManager.LoadScene("TitleScene");
        SceneManager.LoadScene("NightFury_test04");
    }

    //목표:게임 종료 하자~
    public void OnClickEndBtn()
    {
        Application.Quit();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
            Application.openURL("http://google.com");
#else
            Application.Quit();
#endif
    }
}