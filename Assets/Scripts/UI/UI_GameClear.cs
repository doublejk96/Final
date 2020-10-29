using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameClear : UI_Base
{
    public override void ButtonClick(GameObject button)
    {
        base.ButtonClick(button);

        if (button.name == "Quit Button")
        {
            UnityEditor.EditorApplication.isPlaying = false;

            Application.Quit();
        }
    }
}
