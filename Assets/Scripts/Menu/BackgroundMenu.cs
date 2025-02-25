using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine.UI;

public class BackgroundMenu : Panel
{
    [SerializeField] private Button backButton = null;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        backButton.onClick.AddListener(OnBackButton);
        base.Initialize();
    }

    private void OnBackButton()
    {
        PanelManager.CloseAll();
        PanelManager.Open("options");
    }
}
