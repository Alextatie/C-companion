using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Leaderboards;
using Unity.VisualScripting;

public class LeaderboardsMenu : Panel
{

    [SerializeField] private LeaderboardsPlayerItem playerItemPrefab = null;
    [SerializeField] private RectTransform playersContainer = null;
    [SerializeField] public TextMeshProUGUI pageText = null;
    [SerializeField] private Button nextButton = null;
    [SerializeField] private Button prevButton = null;
    [SerializeField] private Button closeButton = null;
    [SerializeField] private Button totalButton = null;
    [SerializeField] private Button fixerButton = null;
    [SerializeField] private Button rushButton = null;
    [SerializeField] private Button ftimeButton = null;
    [SerializeField] private Button rtimeButton = null;

    private int currentPage = 1;
    private int totalPages = 0;
    private int playersPerPage = 7;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        ClearPlayersList();
        closeButton.onClick.AddListener(ClosePanel);
        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
        totalButton.onClick.AddListener(OpenTotal);
        fixerButton.onClick.AddListener(OpenFixerS);
        rushButton.onClick.AddListener(OpenRushS);
        ftimeButton.onClick.AddListener(OpenFixerT);
        rtimeButton.onClick.AddListener(OpenRushT);
        switch (ID)
        {
            case "Total_Stars":
                Stars.OnTotalStars += AddTotalStars;
                totalButton.onClick.RemoveListener(OpenTotal);
                break;
            case "Fixer_Stars":
                Stars.OnFixerStars += AddFixerStars;
                fixerButton.onClick.RemoveListener(OpenFixerS);
                break;
            case "Rush_Stars":
                Stars.OnRushStars += AddRushStars;
                rushButton.onClick.RemoveListener(OpenRushS);
                break;
            case "Fixer_Time":
                Stars.OnFixerScore += AddFixerScore;
                ftimeButton.onClick.RemoveListener(OpenFixerT);
                break;
            case "Rush_Time":
                Stars.OnRushScore += AddRushScore;
                rtimeButton.onClick.RemoveListener(OpenRushT);
                break;
        }
        base.Initialize();
    }
    
    public override void Open()
    {
        pageText.text = "-";
        nextButton.interactable = false;
        prevButton.interactable = false;
        base.Open();
        ClearPlayersList();
        currentPage = 1;
        totalPages = 0;
        LoadPlayers(1,0);
    }

    public void AddFixerScore(float amount)
    {
        AddScoreAsync("Fixer_Time", amount);
    }
    public void AddRushScore(float amount)
    {
        AddScoreAsync("Rush_Time", amount);
    }
    public void GlobalAdder(string boardName,int amount)
    {
        switch (amount)
        {
            case 1:
                ////Debug.Log("case 1: " + boardName + " " + amount + " stars.");
                AddScoreAsync(boardName, 1);
                break;
            case 2:
                ////Debug.Log("case 2: " + boardName + " " + amount + " stars.");
                AddScoreAsync(boardName, 2);
                break;
            case 3:
                ////Debug.Log("case 3: " + boardName + " " + amount + " stars.");
                AddScoreAsync(boardName, 3);
                break;

        }
    }
    
    public void AddTotalStars(int amount)
    {
        GlobalAdder("Total_Stars", amount);
    }
    public void AddRushStars(int amount)
    {
        GlobalAdder("Rush_Stars", amount);
    }
    public void AddFixerStars(int amount)
    {
        GlobalAdder("Fixer_Stars", amount);
    }
    public async void AddScoreAsync(string boardName,int score)
    {
        //addScoreButton.interactable = false;
        try
        {
            ////Debug.Log("Final: " + boardName + " " + score + " stars.");
            var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(boardName, score);
            LoadPlayers(currentPage,1);
        }
        catch (Exception)
        {
            //Debug.Log(exception.Message);
        }
        //addScoreButton.interactable = true;
    }
    public async void AddScoreAsync(string boardName, float score)
    {
        //addScoreButton.interactable = false;
        try
        {
            ////Debug.Log("Final: " + boardName + " " + score + " stars.");
            var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(boardName, score);
            LoadPlayers(currentPage, 1);
        }
        catch (Exception)
        {
            //Debug.Log(exception.Message);
        }
        //addScoreButton.interactable = true;
    }

    private async void LoadPlayers(int page, int flag)
    {
        if (flag == 0)
        {
            PanelManager.Open("loading");
        }
        nextButton.interactable = false;
        prevButton.interactable = false;
        try
        {
            GetScoresOptions options = new GetScoresOptions();
            options.Offset = (page - 1) * playersPerPage;
            options.Limit = playersPerPage;
            var scores = await LeaderboardsService.Instance.GetScoresAsync(ID, options);
            ClearPlayersList();
            for (int i = 0; i < scores.Results.Count; i++)
            {
                LeaderboardsPlayerItem item = Instantiate(playerItemPrefab, playersContainer);
                bool formatFlag = false;
                if ((ID == "Fixer_Time") || (ID == "Rush_Time"))
                {
                    formatFlag = true;
                }
                item.Initialize(scores.Results[i],ID, formatFlag);
            }
            totalPages = Mathf.CeilToInt((float)scores.Total / (float)scores.Limit);
            currentPage = page;
        }
        catch (Exception)
        {
            //Debug.Log(exception.Message);
        }
        pageText.text = currentPage.ToString() + "/" + totalPages.ToString();
        bool checkNext = currentPage < totalPages && totalPages > 1;
        bool checkPrev = currentPage > 1 && totalPages > 1;
        nextButton.interactable = checkNext;
        prevButton.interactable = checkPrev;
        if (checkNext)   //// might need to change colors here
        {
            nextButton.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(1, 1, 1, 255f / 255f);
        }
        if(checkPrev)
        {
            prevButton.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(1, 1, 1, 255f / 255f);
        }
        PanelManager.Close("loading");
    }

    private void NextPage()
    {
        if (currentPage + 1 > totalPages)
        {
            LoadPlayers(1,0);
        }
        else
        {
            LoadPlayers(currentPage + 1,0);
        }
    }

    private void PrevPage()
    {
        if (currentPage - 1 <= 0)
        {
            LoadPlayers(totalPages, 0);
        }
        else
        {
            LoadPlayers(currentPage - 1,0);
        }
    }

    private void ClosePanel()
    {
        PanelManager.CloseAll();
        PanelManager.Open("play");
    }

    private void ClearPlayersList()
    {
        LeaderboardsPlayerItem[] items = playersContainer.GetComponentsInChildren<LeaderboardsPlayerItem>();
        if (items != null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                //Debug.Log("Destroying object: " + items[i].gameObject.name);
                if (Application.isPlaying)
                {
                    Destroy(items[i].gameObject);
                }
            }
        }
    }
    private void OnDisable()
    {
        // Unsubscribe when object is disabled/destroyed
        Stars.OnTotalStars -= AddTotalStars;
        Stars.OnFixerStars -= AddFixerStars;
        Stars.OnRushStars -= AddRushStars;
        Stars.OnFixerScore -= AddFixerScore;
        Stars.OnRushScore -= AddRushScore;
    }

    private void OpenTotal()
    {
        PanelManager.CloseAll();
        PanelManager.Open("Total_Stars");
    }
    private void OpenFixerS()
    {
        PanelManager.CloseAll();
        PanelManager.Open("Fixer_Stars");
    }
    private void OpenRushS()
    {
        PanelManager.CloseAll();
        PanelManager.Open("Rush_Stars");
    }
    private void OpenFixerT()
    {
        PanelManager.CloseAll();
        PanelManager.Open("Fixer_Time");
    }
    private void OpenRushT()
    {
        PanelManager.CloseAll();
        PanelManager.Open("Rush_Time");
    }


}