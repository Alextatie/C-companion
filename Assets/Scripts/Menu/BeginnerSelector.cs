using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine.UI;

public class BeginnerSelector : Panel
{
    [SerializeField] private Button basicButton = null;
    [SerializeField] private Button outputButton = null;
    [SerializeField] private Button commentsButton = null;
    [SerializeField] private Button variablesButton = null;
    [SerializeField] private Button booleansButton = null;
    [SerializeField] private Button operatorsButton = null;
    [SerializeField] private Button backButton = null;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        basicButton.onClick.AddListener(OpenBasic);
        outputButton.onClick.AddListener(OpenOutput);
        commentsButton.onClick.AddListener(OpenComments);
        variablesButton.onClick.AddListener(OpenVariables);
        booleansButton.onClick.AddListener(OpenBooleans);
        operatorsButton.onClick.AddListener(OpenOperators);
        backButton.onClick.AddListener(GoBack);
        base.Initialize();
    }

    private void GoBack()
    {
        PanelManager.Close("beginner");
        PanelManager.Open("learn");
    }
    private void OpenBasic()
    {
        PanelManager.Close("beginner");
        PanelManager.Open("basics");
    }
    private void OpenOutput()
    {
        PanelManager.Close("beginner");
        PanelManager.Open("output");
    }
    private void OpenComments()
    {
        PanelManager.Close("beginner");
        PanelManager.Open("comments");
    }
    private void OpenVariables()
    {
        PanelManager.Close("beginner");
        PanelManager.Open("variables");
    }
    private void OpenBooleans()
    {
        PanelManager.Close("beginner");
        PanelManager.Open("booleans");
    }
    private void OpenOperators()
    {
        PanelManager.Close("beginner");
        PanelManager.Open("operators");
    }
}
