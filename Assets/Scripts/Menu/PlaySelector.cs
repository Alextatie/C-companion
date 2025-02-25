using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine.UI;

public class PlaySelector : Panel
{
    [SerializeField] private Button timeButton = null;
    [SerializeField] private Button fixButton = null;
    [SerializeField] private Button leaderButton = null;
    [SerializeField] private Button backButton = null;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        backButton.onClick.AddListener(OpenMenu);
        timeButton.onClick.AddListener(OpenTime);
        leaderButton.onClick.AddListener(OpenLeader);
        fixButton.onClick.AddListener(OpenFix);
        base.Initialize();
    }

    private void OpenMenu()
    {
        MenuManager.Singleton.OpenMenu();
    }
    private void OpenTime()
    {
        PanelManager.Close("play");
        PanelManager.Open("timeattack");
    }
    private void OpenFix()
    {
        PanelManager.Close("play");
        PanelManager.Open("codefixer");

    }
    private void OpenLeader()
    {
        PanelManager.Close("play");
        PanelManager.Open("Total_Stars");
        //PanelManager.Open("loading");

    }
}
