using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Play : UI_Base
{
    public override void OnClick(GameObject button)
    {
        base.OnClick(button);

        if (button.name == "Pause Button")
        {
            Time.timeScale = 0;

            UI_PauseMenu pauseMenu = GetComponentInChildren<UI_PauseMenu>(true);
            pauseMenu.Show(true);
        }
    }
}
