using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Play : MonoBehaviour
{
    private PlayerController controller;

    [Header("Hp")]
    public GameObject hpBar;
    public Image hpGauge;

    [Header("Ammo")]
    public GameObject ammoBar;
    public Image ammoGauge;

    void FixedUpdate()
    {
        ResetGauge();
    }

    void ResetGauge()
    {
        // 체력
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

        // 총알
        float curAmmo = GunManager.Ins.curAmmo;
        float maxAmmo = GunManager.Ins.maxAmmo;
        ammoGauge.fillAmount = Mathf.Lerp(ammoGauge.fillAmount, curAmmo / maxAmmo, Time.deltaTime * 20);

        if (curAmmo <= 1)
        {
            ammoGauge.GetComponent<Image>().color = Color.red;
        }
        else
        {
            ammoGauge.GetComponent<Image>().color = Color.white;
        }
    }
}
