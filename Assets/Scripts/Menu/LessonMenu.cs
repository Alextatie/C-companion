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

public class LessonMenu : Panel
{
    [SerializeField] private int num_id = 0, multipleOnPage = 0;
    [SerializeField] private string difficulty = null;
    [SerializeField] private GameObject[] level = null;
    [SerializeField] private Button[] back = null;
    [SerializeField] private Button[] next = null;
    [SerializeField] private Button[] home = null;
    [SerializeField] private Button[] locked = null;
    private int index, unlocked, multipleOnPageIndex;
    private int[] multipleflag = null;
    public static event Action<string> OnCleaner = null;
    public static event Action<int> OnFinishedLesson = null;
    public static event Action OnResetIn = null;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        foreach (Button btn in home)
        {
            btn.onClick.AddListener(OpenMenu);
        }
        foreach (Button btn in back)
        {
            btn.onClick.AddListener(OpenPrevious);
        }
        foreach (Button btn in next)
        {
            btn.onClick.AddListener(OpenNext);
        }
        InputReader.OnUnlockedButton += UnlockButtons;
        InputReader.OnUnlockedMultiple += UnlockMultiple;
        base.Initialize();
    }

    public async override void Open()
    {
        base.Open();
        index = unlocked = 0;
        multipleOnPageIndex = 1;
        multipleflag = new int[multipleOnPage];
        for (int i = 0; i < multipleOnPage; i++)
        {
            multipleflag[i] = 0;
        }
        //PanelManager.Open("loading");
        for (int i = 0; i < locked.Length; i++)
        {
            locked[i].interactable = false;
            locked[i].transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 25f / 255f);
        }
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "stagesUnlocked" }, new LoadOptions(new PublicReadAccessClassOptions()));
        if (data.ContainsKey("stagesUnlocked"))
        {
            string s = data["stagesUnlocked"].Value.GetAs<string>();
            unlocked = int.Parse(s);
        }
        if (unlocked > num_id)
        {
            for (int i = 0; i < locked.Length; i++)
            {
                locked[i].interactable = true;
                if (i == 0)
                {
                    locked[i].transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(243f / 255f, 235f / 255f, 255f / 255f, 255f / 255f);
                }
                else
                {
                    locked[i].transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(240f / 255f, 255f / 255f, 201f / 255f, 255f / 255f);
                }
            }
        }
        base.Open();
        //PanelManager.Close("loading");
    }
    
        public void OpenMenu()
        {
        resetStages(); /// swap this with the next
        MenuManager.Singleton.OpenMenu();
    }
    public void OpenSelector()
    {
        resetStages();  /// swap this with the next
        PanelManager.CloseAll();
        PanelManager.Open(difficulty);
    }
    public void OpenPrevious()
    {
        if(index== 0)
        {
            OpenSelector();
            return;
        }
        SoundManager.PlaySound("buttonClick");
        level[index].SetActive(false);
        level[index-1].SetActive(true);
        --index;
    }
    public void OpenNext()
    {
        if (index == level.Length-1)
        {
            index = 0;
            OpenSelector();
            SoundManager.PlaySound("finish2");
            OnFinishedLesson?.Invoke(num_id);
            return;
        }
        SoundManager.PlaySound("buttonClick");
        level[index].SetActive(false);
        level[index + 1].SetActive(true);
        ++index;
    }
    public void resetStages()
    {
        SoundManager.PlaySound("buttonClick");
        foreach (GameObject lvl in level)
        {
            lvl.SetActive(false);
        }
        level[0].SetActive(true);
        index = 0;
        OnCleaner?.Invoke(ID);
        OnResetIn?.Invoke();
    }

    private void UnlockButtons(string s,int i)
    {
        if (s == ID)
        {
            locked[i].interactable = true;
            if(i==0) 
            {
                locked[i].transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(243f / 255f, 235f / 255f, 255f / 255f, 255f / 255f);
            }
            else
            {
                locked[i].transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(240f / 255f, 255f / 255f, 201f / 255f, 255f / 255f);
            }
            multipleOnPageIndex = 1;
        }
    }
    private void UnlockMultiple(string s, int i,int j)
    {
        if (s == ID)
        {
            if (multipleflag[j] == 0)
            {
                if (multipleOnPageIndex < multipleOnPage)
                {
                    ++multipleOnPageIndex;
                    multipleflag[j] = 1;
                }
                else
                {
                    UnlockButtons(s, i);
                }
            }
        }
    }

    private void OnDestroy()
    {
        InputReader.OnUnlockedButton -= UnlockButtons;
        InputReader.OnUnlockedMultiple -= UnlockMultiple;
    }

}
