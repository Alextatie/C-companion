using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine.UI;

public class IntermediateSelector : Panel
{
    [SerializeField] private Button ifButton = null;
    [SerializeField] private Button switchButton = null;
    [SerializeField] private Button loopsButton = null;
    [SerializeField] private Button arraysButton = null;
    [SerializeField] private Button userButton = null;
    [SerializeField] private Button memoryButton = null;
    [SerializeField] private Button backButton = null;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        ifButton.onClick.AddListener(OpenIifelse);
        switchButton.onClick.AddListener(OpenSwitch);
        loopsButton.onClick.AddListener(OpenLoops);
        arraysButton.onClick.AddListener(OpenArrays);
        userButton.onClick.AddListener(OpenUserinput);
        memoryButton.onClick.AddListener(OpenMemorya);
        backButton.onClick.AddListener(GoBack);
        base.Initialize();
    }

    private void GoBack()
    {
        PanelManager.Close("intermediate");
        PanelManager.Open("learn");
    }
    private void OpenIifelse()
    {
        PanelManager.Close("intermediate");
        PanelManager.Open("ifelse");
    }
    private void OpenSwitch()
    {
        PanelManager.Close("intermediate");
        PanelManager.Open("switch");
    }
    private void OpenLoops()
    {
        PanelManager.Close("intermediate");
        PanelManager.Open("loops");
    }
    private void OpenArrays()
    {
        PanelManager.Close("intermediate");
        PanelManager.Open("arrays");
    }
    private void OpenUserinput()
    {
        PanelManager.Close("intermediate");
        PanelManager.Open("userinput");
    }
    private void OpenMemorya()
    {
        PanelManager.Close("intermediate");
        PanelManager.Open("memorya");
    }
}
