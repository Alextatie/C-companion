using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine.UI;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.CloudSave.Models.Data.Player;
using SaveOptions = Unity.Services.CloudSave.Models.Data.Player.SaveOptions;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using Unity.Services.Leaderboards;
using UnityEngine.SocialPlatforms.Impl;

public class ProfileMenu : Panel
{
    [SerializeField] private TextMeshProUGUI nameText = null, lessons=null, rush=null, fixer = null;
    //[SerializeField] private TextMeshProUGUI highscore = null;
    [SerializeField] private Button backButton = null;
    [SerializeField] private Button unlockButton = null;
    [SerializeField] private GameObject unlockObject = null,trophy = null;
    //[SerializeField] private Button editButton = null;
    [SerializeField] private TextMeshProUGUI _id = null;
    private string previousmenu = null;
    private int leaderFlag = 0, unlockFlag = 0, lessonsUnlocked = 0, rushScore = 0, fixerScore = 0;
    private string rushTime = "00:00:00", fixerTime = "00:00:00";
    public static event Action OnUnlockAll = null;
    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        backButton.onClick.AddListener(GoBack);
        unlockButton.onClick.AddListener(UnlockOptions);
        base.Initialize();
    }

    public async override void Open() 
    {
        if (leaderFlag != 1)
        {
            previousmenu = "main";
        }
        base.Open();
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "stagesUnlocked" }, new LoadOptions(new PublicReadAccessClassOptions()));
        if (data.ContainsKey("stagesUnlocked"))
        {
            string s = data["stagesUnlocked"].Value.GetAs<string>();
            if (int.Parse(s) == 19)
            {
                ////Debug.Log("umum: " + int.Parse(s));
                unlockFlag = 1;
            }
            else
            {
                unlockFlag = 0;
            }
        }
        if (leaderFlag==0)
        {
            if (unlockFlag == 0)
            {
                ////Debug.Log("is this on? " );
                unlockObject.SetActive(true);
                trophy.SetActive(false);
            }
            else
            {
                unlockObject.SetActive(false);
                trophy.SetActive(true);
                trophy.transform.Find("a").GetComponent<Image>().color = new Color(236f / 255f, 192f / 255f, 34f / 255f, 255f / 255f);
                trophy.transform.Find("b").GetComponent<Image>().color = new Color(236f / 255f, 192f / 255f, 34f / 255f, 255f / 255f);
                trophy.transform.Find("c").GetComponent<Image>().color = new Color(236f / 255f, 192f / 255f, 34f / 255f, 255f / 255f);
            }
            _id.text = "Id: " + AuthenticationService.Instance.PlayerId;
            try
            {
                
                string initial = await AuthenticationService.Instance.GetPlayerNameAsync();
                nameText.text = "<color=#D5FFDE>Name: "+ initial.Substring(0, initial.Length - 5);
                //nameText.text = "Welcome, " + AuthenticationService.Instance.PlayerName + "!";
                //nameText.text = "Name: " + await LoadPlayerNameFromCloud();
            }
            catch (Exception)
            {

            }
            //editButton.gameObject.SetActive(true);
            //editButton.onClick.AddListener(RenamePlayer);
        }
        if (leaderFlag == 0)
        {
            unlockFlag = rushScore = fixerScore = 0;
            rushTime = fixerTime = "00:00:00";
            try
            {
                await updateLessons();
                await updateRushStars(AuthenticationService.Instance.PlayerId);
                await updateFixerStars(AuthenticationService.Instance.PlayerId);
                await updateRushTime(AuthenticationService.Instance.PlayerId);
                await updateFixerTime(AuthenticationService.Instance.PlayerId);
            }
            catch (Exception)
            {

            }
        }
        else
        {

        }
        leaderFlag = 0;
        updateProfileText();
        base.Open();
        PanelManager.Close("loading");
    }
    public async void Open(string playerId, string playerName,string pageName)
    {
        //Debug.Log("PlayerID: " + playerId + "\nPlayerName: " + playerName + "\nPageName: " + pageName);
        if (unlockFlag == 0)
        {
            unlockObject.SetActive(false);
        }
        trophy.SetActive(true);
        int tempLessons;
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "stagesUnlocked" }, new LoadOptions(new PublicReadAccessClassOptions(playerId)));
        if (data.ContainsKey("stagesUnlocked"))
        {
            string s = data["stagesUnlocked"].Value.GetAs<string>();
            tempLessons = int.Parse(s);
            //Debug.Log("Lesson OTHER check: " + tempLessons);
        }
        else
        {
            tempLessons=0;
        }
        if (tempLessons == 19)
        {
            trophy.transform.Find("a").GetComponent<Image>().color = new Color(236f / 255f, 192f / 255f, 34f / 255f, 255f / 255f);
            trophy.transform.Find("b").GetComponent<Image>().color = new Color(236f / 255f, 192f / 255f, 34f / 255f, 255f / 255f);
            trophy.transform.Find("c").GetComponent<Image>().color = new Color(236f / 255f, 192f / 255f, 34f / 255f, 255f / 255f);
        }
        else
        {
            trophy.transform.Find("a").GetComponent<Image>().color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 30f / 255f);
            trophy.transform.Find("b").GetComponent<Image>().color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 30f / 255f);
            trophy.transform.Find("c").GetComponent<Image>().color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 30f / 255f);
        }
        leaderFlag = 1;
        previousmenu = pageName;
        _id.text = "Id: " + playerId;
        nameText.text = "Name: " +playerName.Substring(0, playerName.Length - 5);
        unlockFlag = rushScore = fixerScore = 0;
        rushTime = fixerTime = "00:00:00";
        try
        {
            //editButton.gameObject.SetActive(false);
           //debug.log("1");
            await updateOtherLessons(playerId);
           //debug.log("2");
            await updateRushStars(playerId);
           //debug.log("3");
            await updateFixerStars(playerId);
           //debug.log("4");
            await updateRushTime(playerId);
           //debug.log("5");
            await updateFixerTime(playerId);
           //debug.log("6");
        }
        catch (Exception)
        {

        }
        Open();
    }

    private void GoBack()
    {
        PanelManager.Close("profile");
        PanelManager.Open(previousmenu);
    }

    private void UnlockOptions()
    {
        UnlockConfirm panel = (UnlockConfirm)PanelManager.GetSingleton("unlock_confirm");
        panel.Open(UnlockResult);

    }

    private async void UnlockResult(UnlockConfirm.Result result)
    {
        if (result == UnlockConfirm.Result.Positive)
        {
            Dictionary<string, object> data = new Dictionary<string, object> { { "stagesUnlocked", 19 } };
            PanelManager.Open("loading");
            await CloudSaveService.Instance.Data.Player.SaveAsync(data, new SaveOptions(new PublicWriteAccessClassOptions()));
            OnUnlockAll?.Invoke();
            Open();
        }
    }
    private async Task updateLessons()
    {
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "stagesUnlocked" }, new LoadOptions(new PublicReadAccessClassOptions()));
        if (data.ContainsKey("stagesUnlocked"))
        {
            string s = data["stagesUnlocked"].Value.GetAs<string>();
            lessonsUnlocked = int.Parse(s);
            ////Debug.Log("Lesson check: " + lessonsUnlocked);
        }
        else
        {
            ////Debug.Log("Stages test: Doesn't have");
        }

    }
    private async Task updateOtherLessons(string playerId)
    {
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "stagesUnlocked" }, new LoadOptions(new PublicReadAccessClassOptions(playerId)));

        if (data.ContainsKey("stagesUnlocked"))
        {
            string s = data["stagesUnlocked"].Value.GetAs<string>();
            lessonsUnlocked = int.Parse(s);
            ////Debug.Log("Lesson check: " + lessonsUnlocked);
        }
        else
        {
            ////Debug.Log("Stages test: Doesn't have");
        }

    }
    private async Task updateRushStars(string playerId)
    {
        try
        {
            var playerIds = new List<string> { playerId };
            var scoresResponse = await LeaderboardsService.Instance
                .GetScoresByPlayerIdsAsync("Rush_Stars", playerIds);
            rushScore = (int)scoresResponse.Results[0].Score;
        }
        catch
        {

        }

        //Debug.Log("Rush stars: " + scoresResponse.Results[0].Score);

    }
    private async Task updateRushTime(string playerId)
    {
        try
        {
            var playerIds = new List<string> { playerId };
            var scoresResponse = await LeaderboardsService.Instance
                .GetScoresByPlayerIdsAsync("Rush_Time", playerIds);
            int minutes = Mathf.FloorToInt((float)scoresResponse.Results[0].Score / 60);
            int seconds = Mathf.FloorToInt((float)scoresResponse.Results[0].Score % 60);
            int milliseconds = Mathf.FloorToInt(((float)scoresResponse.Results[0].Score % 1) * 100); // Extract 2-digit milliseconds

            rushTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
            //Debug.Log("PlayerID: " + playerId + "\nRush time: " + scoresResponse.Results[0].Score);
        }
        catch
        {

        }
    }
    private async Task updateFixerTime(string playerId)
    {
        try
        {
            var playerIds = new List<string> { playerId };
            var scoresResponse = await LeaderboardsService.Instance
                .GetScoresByPlayerIdsAsync("Fixer_Time", playerIds);
            int minutes = Mathf.FloorToInt((float)scoresResponse.Results[0].Score / 60);
            int seconds = Mathf.FloorToInt((float)scoresResponse.Results[0].Score % 60);
            int milliseconds = Mathf.FloorToInt(((float)scoresResponse.Results[0].Score % 1) * 100); // Extract 2-digit milliseconds

            fixerTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
        catch
        {

        }

        //Debug.Log("Fixer time: " + scoresResponse.Results[0].Score);

    }
    private async Task updateFixerStars(string playerId)
    {
        var playerIds = new List<string> { playerId };
        try{
            var scoresResponse = await LeaderboardsService.Instance
            .GetScoresByPlayerIdsAsync("Fixer_Stars", playerIds);
            fixerScore = (int)scoresResponse.Results[0].Score;
            //Debug.Log("got a thing");
        }
        catch
        {
            //Debug.Log("didnt get a thing");
        }
        //Debug.Log("Fixer stars: " + scoresResponse.Results[0].Score);

    }

    private void updateProfileText()
    {
        if (lessonsUnlocked <= 7)
        {
            lessons.text = (lessonsUnlocked-1)+"/6\n0/6\n0/6";
        }
        else if((lessonsUnlocked >7) && (lessonsUnlocked <= 13))
        {
            lessons.text ="6/6\n"+(lessonsUnlocked-7)+"/6\n0/6";
        }
        else
        {
            lessons.text ="6/6\n6/6\n"+ (lessonsUnlocked - 13)+"/6";
        }
        fixer.text = fixerScore+"\n"+fixerTime;
        rush.text = rushScore+"\n"+rushTime;
    }
    //private void RenamePlayer()   //considering if should include this feature
    //{
    //    GetInputMenu panel = (GetInputMenu)PanelManager.GetSingleton("input");
    //    panel.Open(RenamePlayerConfirm, GetInputMenu.Type.String, 30, "Confirm", "Cancel");
    //}
    //private async void RenamePlayerConfirm(string input)
    //{
    //    //editButton.interactable = false;
    //    try
    //    {
    //        await AuthenticationService.Instance.UpdatePlayerNameAsync(input);
    //        nameText.text = "Name: " + AuthenticationService.Instance.PlayerName;
    //        Dictionary<string, object> data = new Dictionary<string, object> { { "PlayerName", AuthenticationService.Instance.PlayerName } };
    //        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
    //    }
    //    catch
    //    {
    //        ErrorMenu panel = (ErrorMenu)PanelManager.GetSingleton("error");
    //        panel.Open(ErrorMenu.Action.None, "Failed to change account name.", "OK");
    //    }
    //}

    //public async Task<string> LoadPlayerNameFromCloud()
    //{
    //    try
    //    {
    //        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "PlayerName" });

    //        if (data.ContainsKey("PlayerName"))
    //        {
    //            return data["PlayerName"].Value.GetAs<string>(); // Correct way to retrieve the string
    //        }
    //        else
    //        {
    //            return "Unknown Player";
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        //Debug.LogError("Failed to load player name: " + e.Message);
    //        return "Unknown Player";
    //    }
    //}
}
