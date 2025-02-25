using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine.UI;
using Unity.VisualScripting;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.CloudSave.Models.Data.Player;
using SaveOptions = Unity.Services.CloudSave.Models.Data.Player.SaveOptions;
using UnityEngine.Windows;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.SocialPlatforms.Impl;
//using Unity.Services.Friends;

public class MainMenu : Panel
{

    [SerializeField] private TextMeshProUGUI nameText = null;
    //[SerializeField] private TextMeshProUGUI details = null;
    [SerializeField] private Button logoutButton = null;
    [SerializeField] private Button profileButton = null;
    [SerializeField] private Button playButton = null;
    [SerializeField] private Button learnButton = null;
    [SerializeField] private Button aboutButton = null;
    [SerializeField] private Button optionsButton = null;
    //[SerializeField] private Button anonButton = null;   ///test, delete later
    //[SerializeField] private Button disableButton = null;   ///test, delete later
    //[SerializeField] private Button[] toDisable = null;   ///test, delete later
    //private int disableFlag = 0;
    private bool isAnon = false;
    //private bool containsAnon = false;
    //[SerializeField] private Button leaderboardsButton = null;
    //[SerializeField] private Button friendsButton = null;
    //[SerializeField] private Button renameButton = null;
    //[SerializeField] private Button customizationButton = null;

    //private bool isFriendsServiceInitialized = false;
    //public static event Action<bool> OnIfAnon = null;
    public static event Action OnStagesUnlocked = null;
    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        logoutButton.onClick.AddListener(SignOut);
        profileButton.onClick.AddListener(OpenProfile);
        playButton.onClick.AddListener(OpenPlay);
        learnButton.onClick.AddListener(OpenLearn);
        aboutButton.onClick.AddListener(OpenAbout);
        optionsButton.onClick.AddListener(OpenOptions);
        //disableButton.onClick.AddListener(OnDisables);
        //anonButton.onClick.AddListener(OnIsAnon);
        //leaderboardsButton.onClick.AddListener(Leaderboards);
        //friendsButton.onClick.AddListener(Friends);
        //renameButton.onClick.AddListener(RenamePlayer);
        //customizationButton.onClick.AddListener(Customization);
        base.Initialize();
    }

    public async override void Open()
    {
        //friendsButton.interactable = isFriendsServiceInitialized;


        //if (isFriendsServiceInitialized == false)
        //{
        //    InitializeFriendsServiceAsync();
        //}

        base.Open();
        await UpdatePlayerNameUI();
        base.Open();
    }


    public bool getAnon()
    {
        return isAnon;
    }

    private void OpenProfile()
    {
        MenuManager.Singleton.PlayerProfile();
        PanelManager.Open("loading");
    }
    private void OpenPlay()
    {
        PanelManager.CloseAll();
        PanelManager.Open("play");
    }
    private void OpenLearn()
    {
        PanelManager.CloseAll();
        PanelManager.Open("learn");
    }
    private void OpenAbout()
    {
        PanelManager.CloseAll();
        PanelManager.Open("about");
    }                      
    private void OpenOptions()
    {
        PanelManager.CloseAll();
        PanelManager.Open("options");
    }
    //private void OnDisables()
    //{
    //    if (disableFlag == 0)
    //    {
    //        foreach (Button x in toDisable)
    //        {
    //            x.interactable = false;
    //        }
    //        disableFlag = 1;
    //    }
    //    else
    //    {
    //        foreach (Button x in toDisable)
    //        {
    //            x.interactable = true;
    //        }
    //        disableFlag = 0;
    //    }
    //}
    public async Task UpdatePlayerNameUI()
    {
        PanelManager.Open("loading");

        int elapsedTime = 0, timeout = 30, checkInterval = 2;
        string myvalue = null;

        try
        {
            // Wait until the "isAnon" key is found or timeout is reached
            while (elapsedTime < timeout)
            {

                var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "isAnon" });
                if (data.ContainsKey("isAnon"))
                {
                    myvalue = data["isAnon"].Value.GetAs<string>(); // Correct way to retrieve the string
                    ////Debug.Log("isAnon value: " + myvalue); // Print extracted value to console
                    break;
                }
                elapsedTime += checkInterval;
                await Task.Delay(checkInterval * 100);  // Wait before checking again
            }
            string initial = await AuthenticationService.Instance.GetPlayerNameAsync();
            nameText.text = "<color=#D5FFDE>Welcome, "+initial.Substring(0, initial.Length - 5)+"!";
        }
        catch (System.Exception)
        {
            //Debug.LogError("Error during Cloud Save or Authentication: " + e.Message);
        }
        if (myvalue == "true") {
            isAnon = true;
            profileButton.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 25f / 255f);
            profileButton.interactable = false;
            //OnIfAnon?.Invoke(true);
        }
        else
        {
            isAnon = false;
            profileButton.interactable = true;
            profileButton.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(240f / 255f, 255f / 255f, 201f / 255f, 255f / 255f);
            //OnIfAnon?.Invoke(false);
        }
        OnStagesUnlocked?.Invoke();
        PanelManager.Close("loading");
    }
    private void SignOut()
    {
        ActionConfirmMenu panel = (ActionConfirmMenu)PanelManager.GetSingleton("action_confirm");
        panel.Open(SignOutResult, "Are you sure?", "Yes", "No");

    }

    private void SignOutResult(ActionConfirmMenu.Result result)
    {
        if (result == ActionConfirmMenu.Result.Positive)
        {
            MenuManager.Singleton.SignOut(isAnon);
        }
    }

}