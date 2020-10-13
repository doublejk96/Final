using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UI_State
{
    Start = 0,
    Play = 1,
    Pause,
    End
}

public class UI_Manager : MonoBehaviour
{
    private static UI_Manager instance;
    public static UI_Manager Instance
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

    [SerializeField]
    private UI_Base[] uiList;

    void Start()
    {
        uiList = GetComponentsInChildren<UI_Base>(true);
        foreach (UI_Base ui in uiList)
        {

        }       
    }

    public void Show(UI_State uiState, bool show)
    {
        foreach (UI_Base ui in uiList)
        {
            ui.gameObject.SetActive(false);
        }

        UI_Base uiBase = uiList[(int)uiState];
        uiBase.gameObject.SetActive(show);
    }
}
