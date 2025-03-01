using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject PauseMenu;

    public void OnPauseMenu()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
    }
}
