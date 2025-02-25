using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine.UI;

public class LearnSelector : Panel
{
    [SerializeField] private Button beginnerButton = null;
    [SerializeField] private Button intermediateButton = null;
    [SerializeField] private Button advancedButton = null;
    [SerializeField] private Button backButton = null;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        beginnerButton.onClick.AddListener(OpenBeginner);
        intermediateButton.onClick.AddListener(OpenIntermediate);
        advancedButton.onClick.AddListener(OpenAdvanced);
        backButton.onClick.AddListener(OpenMenu);
        base.Initialize();
    }

    private void OpenMenu()
    {
        MenuManager.Singleton.OpenMenu();
    }
    private void OpenBeginner()
    {
        PanelManager.Close("learn");
        PanelManager.Open("beginner");
    }
    private void OpenIntermediate()
    {
        PanelManager.Close("learn");
        PanelManager.Open("intermediate");
    }
    private void OpenAdvanced()
    {
        PanelManager.Close("learn");
        PanelManager.Open("advanced");
    }
}
