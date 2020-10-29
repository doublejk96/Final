using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UI_ID
{
    PLAY,
    GAMEOVER,
    GAMECLEAR
}

public class UI_Manager : MonoBehaviour
{
    #region Singleton
    private static UI_Manager instance;
    public static UI_Manager Ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UI_Manager>();

                if (null == instance)
                {
                    Debug.LogError("UI_Manager Not Found");
                }
            }
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private UI_Base[] uiList;

    public void Init()
    {
        uiList = GetComponentsInChildren<UI_Base>(true);
        foreach (UI_Base ui in uiList)
        {
            ui.Init();
        }
    }

    public void Show(UI_ID uiID, bool show)
    {
        foreach (UI_Base ui in uiList)
        {
            ui.gameObject.SetActive(false);
        }
        UI_Base uiBase = uiList[(int)uiID];
        uiBase.gameObject.SetActive(show);
    }
}
