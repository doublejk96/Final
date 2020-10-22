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
    public Text ammoText;

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
        ammoText.text = curAmmo.ToString();

        if (curAmmo <= maxAmmo * 0.2f)
        {
            ammoText.GetComponent<Text>().color = Color.red;
        }
        else
        {
            ammoText.GetComponent<Text>().color = Color.white;
        }
    }
}
