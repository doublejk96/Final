using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : UI_Base
{
    public override void ButtonClick(GameObject button)
    {
        base.ButtonClick(button);

        if (button.name == "Retry Button")
        {
            // 최종 저장위치로부터 시작
            SceneManager.LoadScene(1);
        }

        if (button.name == "Quit Button")
        {
            UnityEditor.EditorApplication.isPlaying = false;

            Application.Quit();
        }
    }
}
