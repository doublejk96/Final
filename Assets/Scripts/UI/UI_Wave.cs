using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Wave : MonoBehaviour
{
    public Image waveGauge;
    public Text waveNum;

    void Update()
    {
        float curWave = GameManager.Instance.curWaveNum;
        float maxWave = GameManager.Instance.waves.Length;

        waveNum.text = curWave + " / " + maxWave.ToString();
        waveGauge.fillAmount = Mathf.Lerp(waveGauge.fillAmount, curWave / maxWave, Time.deltaTime * 10);
    }
}
