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

public class ButtonActivator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Button intermediateButton = null;
    [SerializeField] private Button advancedButton = null;
    [SerializeField] private Button playButton1 = null;
    [SerializeField] private Button mediumButton1 = null;
    [SerializeField] private Button hardButton1 = null;
    [SerializeField] private Button playButton2 = null;
    [SerializeField] private Button mediumButton2 = null;
    [SerializeField] private Button hardButton2 = null;
    [SerializeField] private Button[] classButton = null;
    [SerializeField] private GameObject explanationText1 = null;
    private TextMeshProUGUI theText1 = null;
    [SerializeField] private GameObject explanationText2 = null;
    private TextMeshProUGUI theText2 = null;
    [SerializeField] private GameObject expcontainer1 = null;
    [SerializeField] private GameObject expcontainer2 = null;
    private Vector3 y2 = new Vector3(186.9001f, -361f, 0f), y3 = new Vector3(186.9001f, -450f, 0f);
    private Color offColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 25f / 255f);


    private int unlocked = 0;

    private void Start()
    {
        MainMenu.OnStagesUnlocked += updateAnon;
        ProfileMenu.OnUnlockAll += unlockAll;
        LessonMenu.OnFinishedLesson += increaseByOne;
        theText1 =  explanationText1.transform.Find("text").GetComponent<TextMeshProUGUI>();
        theText2 = explanationText2.transform.Find("text").GetComponent<TextMeshProUGUI>();
    }
    
    private async void updateAnon()
    {
        resetAll();
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "stagesUnlocked" }, new LoadOptions(new PublicReadAccessClassOptions()));
        if (data.ContainsKey("stagesUnlocked"))
        {
            string s = data["stagesUnlocked"].Value.GetAs<string>();
            unlocked = int.Parse(s);
            ////Debug.Log("Stages test: " + s);
        }
        else
        {
            ////Debug.Log("Stages test: Doesn't have");
        }
        unlockStages();
    }
    public async void increaseByOne(int flag)
    {
        if (flag != unlocked)
        {
            return;
        }
        ++unlocked;
        if (unlocked == 7)
        {
            intermediateButton.interactable = true;
            intermediateButton.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 255f / 255f, 201f / 255f, 255f / 255f);
            playButton1.interactable = true;
            playButton1.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(240f / 255f, 255f / 255f, 201f / 255f , 255f / 255f);
            playButton2.interactable = true;
            playButton2.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(240f / 255f, 255f / 255f, 201f / 255f, 255f / 255f);
            explanationText1.transform.localPosition = y2;
            explanationText2.transform.localPosition = y2;
            theText1.text = "Finish the second learing\nchapter to unlock";
            theText2.text = "Finish the second learing\nchapter to unlock";
            expcontainer1.SetActive(false);
            expcontainer2.SetActive(false);
        }
        if (unlocked == 13)
        {
            advancedButton.interactable = true;
            advancedButton.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 228f / 255f, 201f / 255f, 255f / 255f);
            mediumButton1.interactable = true;
            mediumButton1.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 255f / 255f, 201f / 255f, 255f / 255f);
            mediumButton2.interactable = true;
            mediumButton2.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 255f / 255f, 201f / 255f, 255f / 255f);
            explanationText1.transform.localPosition = y3;
            explanationText2.transform.localPosition = y3;
            theText1.text = "Finish the third learing\nchapter to unlock";
            theText2.text = "Finish the third learing\nchapter to unlock";
        }
        if (unlocked == 19)
        {
            hardButton1.interactable = true;
            hardButton1.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 228f / 255f, 201f / 255f, 255f / 255f);
            hardButton2.interactable = true;
            hardButton2.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 228f / 255f, 201f / 255f, 255f / 255f);
            explanationText1.SetActive(false);
            explanationText2.SetActive(false);
            Dictionary<string, object> data2 = new Dictionary<string, object> { { "usedUnlock", 1 } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(data2, new SaveOptions(new PublicWriteAccessClassOptions()));
        }
        if (unlocked < 19)
        {
            classButton[unlocked - 1].interactable = true;
            classButton[unlocked - 1].transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(221f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        }
        Dictionary<string, object> data = new Dictionary<string, object> { { "stagesUnlocked", unlocked } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data, new SaveOptions(new PublicWriteAccessClassOptions()));
    }

    private void unlockStages()
    {
        if(unlocked < 7) {
            expcontainer1.SetActive(true);
            expcontainer2.SetActive(true);
        }
        if (unlocked >= 7)
        {
            intermediateButton.interactable = true;
            intermediateButton.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 255f / 255f, 201f / 255f, 255f / 255f);
            playButton1.interactable = true;
            playButton1.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(240f / 255f, 255f / 255f, 201f / 255f, 255f / 255f);
            playButton2.interactable = true;
            playButton2.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(240f / 255f, 255f / 255f, 201f / 255f, 255f / 255f);
            explanationText1.transform.localPosition = y2;
            explanationText2.transform.localPosition = y2;
            theText1.text = "Finish the second learing\nchapter to unlock";
            theText2.text = "Finish the second learing\nchapter to unlock";
            expcontainer1.SetActive(false);
            expcontainer2.SetActive(false);
        }
        if (unlocked >= 13)
        {
            advancedButton.interactable = true;
            advancedButton.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 228f / 255f, 201f / 255f, 255f / 255f);
            mediumButton1.interactable = true;
            mediumButton1.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 255f / 255f, 201f / 255f, 255f / 255f);
            mediumButton2.interactable = true;
            mediumButton2.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 255f / 255f, 201f / 255f, 255f / 255f);
            explanationText1.transform.localPosition = y3;
            explanationText2.transform.localPosition = y3;
            theText1.text = "Finish the third learing\nchapter to unlock";
            theText2.text = "Finish the third learing\nchapter to unlock";
        }
        if (unlocked == 19)
        {
            hardButton1.interactable = true;
            hardButton1.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 228f / 255f, 201f / 255f, 255f / 255f);
            hardButton2.interactable = true;
            hardButton2.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255f / 255f, 228f / 255f, 201f / 255f, 255f / 255f);
            explanationText1.SetActive(false);
            explanationText2.SetActive(false);
        }
        for (int i = 0; i < unlocked; ++i)
        {
            if (i == 18)
            {
                break;
            }
            classButton[i].interactable = true;
            classButton[i].transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(221f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        }
    }

    private void unlockAll()
    {
        unlocked = 19;
        unlockStages();
    }
    private void OnDisable()
    {
        MainMenu.OnStagesUnlocked -= updateAnon;
        ProfileMenu.OnUnlockAll -= unlockAll;
        LessonMenu.OnFinishedLesson -= increaseByOne;
    }

    private void resetAll()
    {
        intermediateButton.interactable = false;
        advancedButton.interactable = false;
        playButton1.interactable = false;
        mediumButton1.interactable = false;
        hardButton1.interactable = false;
        playButton2.interactable = false;
        mediumButton2.interactable = false;
        hardButton2.interactable = false;
        intermediateButton.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = offColor;
        advancedButton.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = offColor;
        playButton1.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = offColor;
        mediumButton1.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = offColor;
        hardButton1.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = offColor;
        playButton2.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = offColor;
        mediumButton2.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = offColor;
        hardButton2.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = offColor;
        explanationText1.SetActive(true);
        explanationText2.SetActive(true);
        explanationText1.transform.localPosition = new Vector3(186.9f, -307.11f, 0f);
        explanationText2.transform.localPosition = new Vector3(186.9f, -307.11f, 0f);
        theText1.text = "Finish the first learing\nchapter to unlock";
        theText2.text = "Finish the first learing\nchapter to unlock";
        for (int i = 0; i < 18; ++i)
        {
            if ((i == 0)|| (i == 6) || (i == 12))
            {
                continue;
            }
            classButton[i].interactable = false;
            classButton[i].transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = offColor;
        }
    }

    public void activator()
    {
        if (unlocked >= 7)
        {
            expcontainer1.SetActive(true);
            expcontainer2.SetActive(true);
        }
    }
    public void deactivator()
    {
        if (unlocked >= 7)
        {
            expcontainer1.SetActive(false);
            expcontainer2.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        MainMenu.OnStagesUnlocked -= updateAnon;
        ProfileMenu.OnUnlockAll -= unlockAll;
        LessonMenu.OnFinishedLesson -= increaseByOne;
    }
}
