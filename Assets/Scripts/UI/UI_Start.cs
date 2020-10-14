using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Start : UI_Base
{
    public override void OnClick(GameObject button)
    {
        base.OnClick(button);

        if (button.name == "Start Button")
        {
            UI_Manager.Instance.Show(UI_ID.Play, true);

            GameManager.Instance.WaveStart();
        }
    }
}
