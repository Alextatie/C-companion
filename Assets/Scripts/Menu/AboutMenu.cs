using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine.UI;

public class AboutMenu : Panel
{
    [SerializeField] private Button backButton = null;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        backButton.onClick.AddListener(OpenMenu);
        base.Initialize();
    }

    private void OpenMenu()
    {
        MenuManager.Singleton.OpenMenu();
    }
}
