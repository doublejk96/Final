using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameOver : UI_Base
{
    public override void OnClick(GameObject button)
    {
        base.OnClick(button);

        if (button.name == "Restart Button")
        {
            // 스테이지 재시작
        }

        if (button.name == "Quit Button")
        {
            // 메인메뉴
        }
    }
}
