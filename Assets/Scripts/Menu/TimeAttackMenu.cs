using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine.UI;

public class TimeAttackMenu : Panel
{
    [SerializeField] private Button backButton = null;
    [SerializeField] private Button homeButton = null;
    [SerializeField] private Button secondhomeButton = null;
    //[SerializeField] private Button secondbackButton = null;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        backButton.onClick.AddListener(GoBack);
        //secondbackButton.onClick.AddListener(GoBack);
        homeButton.onClick.AddListener(OpenMenu);
        secondhomeButton.onClick.AddListener(OpenMenu);
        base.Initialize();
    }

    private void OpenMenu()
    {
        MenuManager.Singleton.OpenMenu();
    }
    private void GoBack()
    {
        PanelManager.Close("timeattack");
        PanelManager.Open("play");
    }
}
