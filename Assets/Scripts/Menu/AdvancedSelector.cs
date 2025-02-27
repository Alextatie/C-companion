using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine.UI;

public class AdvancedSelector : Panel
{
    [SerializeField] private Button functionsButton = null;
    [SerializeField] private Button recursionButton = null;
    [SerializeField] private Button structsButton = null;
    [SerializeField] private Button enumsButton = null;
    [SerializeField] private Button filesButton = null;
    [SerializeField] private Button memorymButton = null;
    [SerializeField] private Button backButton = null;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        functionsButton.onClick.AddListener(OpenFunctions);
        recursionButton.onClick.AddListener(OpenRecursion);
        structsButton.onClick.AddListener(OpenStructs);
        enumsButton.onClick.AddListener(OpenEnums);
        filesButton.onClick.AddListener(OpenFiles);
        memorymButton.onClick.AddListener(OpenMemorym);
        backButton.onClick.AddListener(GoBack);
        base.Initialize();
    }

    private void GoBack()
    {
        PanelManager.Close("advanced");
        PanelManager.Open("learn");
    }
    private void OpenFunctions()
    {
        PanelManager.Close("advanced");
        PanelManager.Open("functions");
    }
    private void OpenRecursion()
    {
        PanelManager.Close("advanced");
        PanelManager.Open("recursion");
    }
    private void OpenStructs()
    {
        PanelManager.Close("advanced");
        PanelManager.Open("structs");
    }
    private void OpenEnums()
    {
        PanelManager.Close("advanced");
        PanelManager.Open("enums");
    }
    private void OpenFiles()
    {
        PanelManager.Close("advanced");
        PanelManager.Open("files");
    }
    private void OpenMemorym()
    {
        PanelManager.Close("advanced");
        PanelManager.Open("memorym");
    }
}
