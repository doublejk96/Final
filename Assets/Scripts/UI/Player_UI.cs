using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    [Header("Hp")]
    public Image hpGauge;
    public Text hpValue;

    [Header("Ammo")]
    public Image ammoGauge;
    public Text ammoValue;

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        UpdateHp();
        UpdateAmmo();
    }

    void UpdateHp()
    {
        float curHp = player.curHp;
        float maxHp = player.maxHp;

        hpGauge.fillAmount = Mathf.Lerp(hpGauge.fillAmount, curHp / maxHp, Time.deltaTime * 10);
        hpValue.text = curHp + " / " + maxHp.ToString(); 
    }

    void UpdateAmmo()
    {
        float curAmmo = player.curAmmo;
        float maxAmmo = player.maxAmmo;

        ammoGauge.fillAmount = Mathf.Lerp(ammoGauge.fillAmount, curAmmo / maxAmmo, Time.deltaTime * 10);
        ammoValue.text = curAmmo + " / " + maxAmmo.ToString();
    }
}
