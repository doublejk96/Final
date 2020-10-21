using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Play : MonoBehaviour
{
    [Header("Hp")]
    public GameObject hpBar;
    public Image hpGauge;

    void FixedUpdate()
    {
        ResetGauge();
    }

    void ResetGauge()
    {
        float curHp = Player.Ins.curHp;
        float maxHp = Player.Ins.maxHp;
        hpGauge.fillAmount = Mathf.Lerp(hpGauge.fillAmount, curHp / maxHp, Time.deltaTime * 10);

        if (curHp >= maxHp)
        {
            hpBar.SetActive(false);
        }
        else
        {
            hpBar.SetActive(true);
        }
    }
}
