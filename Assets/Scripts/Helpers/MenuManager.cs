using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine.Windows;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.CloudSave.Models.Data.Player;
using SaveOptions = Unity.Services.CloudSave.Models.Data.Player.SaveOptions;
using System.Threading.Tasks;

public class MenuManager : MonoBehaviour
{

    private bool initialized = false;
    private bool eventsInitialized = false;
    //private int anonflag = 0;
    private static MenuManager singleton = null;

    public static MenuManager Singleton
    {
        get
        {
            if (singleton == null)
            {
                singleton = FindFirstObjectByType<MenuManager>();
                singleton.Initialize();
            }
            return singleton;
        }
    }

    private void Initialize()
    {
        if (initialized) { return; }
        initialized = true;
    }

    private void OnDestroy()
    {
        if (singleton == this)
        {
            singleton = null;
        }
    }

    private void Awake()
    {
        Application.runInBackground = true;
        StartClientService();
    }

    public async void StartClientService()
    {
        PanelManager.CloseAll();
        PanelManager.Open("loading");
        try
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                var options = new InitializationOptions();
                options.SetProfile("default_profile");
                await UnityServices.InitializeAsync();
            }

            if (!eventsInitialized)
            {
                SetupEvents();
            }

            if (AuthenticationService.Instance.SessionTokenExists)
            {
                SignInAnonymouslyAsync();
            }
            else
            {
                PanelManager.Close("loading");
                PanelManager.Open("auth");
            }
        }
        catch (Exception)
        {
            ShowError(ErrorMenu.Action.StartService, "<size=46>Failed to connect to the network.", "<size=46>Retry");
        }
    }

    public async void SignInAnonymouslyAsync()
    {
        //anonflag = 1;
        PanelManager.Open("loading");
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            if(!(await CheckIfNew()))
            {
                await AuthenticationService.Instance.UpdatePlayerNameAsync("Guest");
                Dictionary<string, object> data = new Dictionary<string, object> { { "stagesUnlocked", 19 } };
                await CloudSaveService.Instance.Data.Player.SaveAsync(data, new SaveOptions(new PublicWriteAccessClassOptions()));
                Dictionary<string, object> data2 = new Dictionary<string, object> { { "isAnon", true } };
                await CloudSaveService.Instance.Data.Player.SaveAsync(data2);
            }
            //string initial = await AuthenticationService.Instance.GetPlayerNameAsync();
            ////Debug.Log("im here: "+initial); 
        }
        catch (AuthenticationException)
        {
            ShowError(ErrorMenu.Action.OpenAuthMenu, "<size=46>Failed to sign in.", "Ok");
        }
        catch (RequestFailedException)
        {
            ShowError(ErrorMenu.Action.SignIn, "<size=46>Failed to connect to the network.", "<size=46>Retry");
        }
    }

    public async void SignInWithUsernameAndPasswordAsync(string username, string password)
    {
        PanelManager.Open("loading");
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
        }
        catch (AuthenticationException)
        {
            ShowError(ErrorMenu.Action.OpenAuthMenu, "<size=44>Username or password are incorrect.", "Ok"); /// migh need to swap
        }
        catch (RequestFailedException)
        {
            ShowError(ErrorMenu.Action.OpenAuthMenu, "<size=44>Username or password are incorrect.", "Ok"); /// migh need to swap
        }
    }

    public async void SignUpWithUsernameAndPasswordAsync(string username, string password)
    {
        PanelManager.Open("loading");
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            await AuthenticationService.Instance.UpdatePlayerNameAsync(username);
            //adding a field to tell if anonymous
            Dictionary<string, object> data = new Dictionary<string, object> { { "stagesUnlocked", 1 } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(data, new SaveOptions(new PublicWriteAccessClassOptions()));
            Dictionary<string, object> data2 = new Dictionary<string, object> { { "isAnon", false } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(data2);
        }
        catch (AuthenticationException)
        {
            ShowError(ErrorMenu.Action.OpenAuthMenu, "<size=46>Username already taken.", "Ok"); //can expand more on codes
        }
        catch (RequestFailedException)
        {
            ShowError(ErrorMenu.Action.OpenAuthMenu, "<size=30>Username must be 3-20 characters, contain only letters, digits, or the symbols (<color=#E7423E>.</color>,<color=#E7423E>-</color>,<color=#E7423E>_</color>,<color=#E7423E>@</color>)", "Ok");
        }
    }

    public async void SignOut(bool anon)
    {
        if (anon)
        {
            try
            {
                // Deletes the currently authenticated player account
                await AuthenticationService.Instance.DeleteAccountAsync();
                //Debug.Log("Player account deleted successfully.");
            }
            catch (System.Exception)
            {
                //Debug.LogError("Failed to delete account: " + ex.Message);
            }
        }
        else
        {
            AuthenticationService.Instance.SignOut();
        }
        AuthenticationService.Instance.ClearSessionToken();
        PanelManager.CloseAll();
        PanelManager.Open("auth");

    }

    private void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += () =>
        {
            PanelManager.CloseAll();
            PanelManager.Open("main");
        };

        AuthenticationService.Instance.SignedOut += () =>
        {
            PanelManager.CloseAll();
            PanelManager.Open("auth");
        };

        AuthenticationService.Instance.Expired += () =>
        {
            SignInAnonymouslyAsync();
        };
    }

    private void ShowError(ErrorMenu.Action action = ErrorMenu.Action.None, string error = "", string button = "")
    {
        PanelManager.Close("loading");
        ErrorMenu panel = (ErrorMenu)PanelManager.GetSingleton("error");
        panel.Open(action, error, button);
    }

    //private void onError()
    //{
    //    PanelManager.CloseAll();
    //    PanelManager.Open("auth");
    //}

    //private async void confirmation()
    //{
    //    try
    //    {
    //        if (anonflag == 1)
    //        {
    //            await AuthenticationService.Instance.UpdatePlayerNameAsync("player");
    //            anonflag = 0;
    //        }
    //        //Debug.Log("not async: " + AuthenticationService.Instance.PlayerName);
    //        //Debug.Log("yes async: " + await AuthenticationService.Instance.GetPlayerNameAsync());
    //    }
    //    catch (Exception)
    //    {

    //    }
    //    PanelManager.CloseAll();
    //    PanelManager.Open("main");
    //}

    public void PlayerProfile()
    {
        PanelManager.Close("main");
        PanelManager.Open("profile");
    }

    public void OpenMenu()
    {
        PanelManager.CloseAll();
        PanelManager.Open("main");
    }
    public async Task<bool> CheckIfNew()
{
    var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "isAnon" });

    if (data.ContainsKey("isAnon"))
    {
        ////Debug.Log("isAnon value: " + true);
        return true;
    }
    else
    {
        ////Debug.Log("isAnon value: " + false);
        return false;
    }
}

}