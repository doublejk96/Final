using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PauseMenu : UI_Base
{
    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }

    public override void OnClick(GameObject button)
    {
        base.OnClick(button);

        if (button.name == "Back Button")
        {
            Time.timeScale = 1;

            Show(false);
        }

        if (button.name == "Quit Button")
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
