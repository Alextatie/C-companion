using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine.UI;

public class CodeFixerMenu : Panel
{
    [SerializeField] private Button backButton = null;
    [SerializeField] private Button homeButton = null;
    [SerializeField] private Button secondhomeButton = null;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        backButton.onClick.AddListener(GoBack);
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
        PanelManager.Close("codefixer");
        PanelManager.Open("play");
    }
}
