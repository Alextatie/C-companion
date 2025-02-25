using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Leaderboards.Models;
using UnityEngine.UI;


public class LeaderboardsPlayerItem : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI rankText = null;
    [SerializeField] public TextMeshProUGUI nameText = null;
    [SerializeField] public TextMeshProUGUI scoreText = null;
    [SerializeField] private Button selectButton = null;
    private string myPageName;

    private LeaderboardEntry player = null;
    
    private void Start()
    {
        selectButton.onClick.AddListener(Clicked);
    }
    
    public void Initialize(LeaderboardEntry player,string pageName,bool flag)
    {
        myPageName = pageName;
        this.player = player;
        rankText.text = (player.Rank + 1).ToString();
        nameText.text = player.PlayerName.Substring(0, player.PlayerName.Length - 5);
        if (flag)
        {
            int minutes = Mathf.FloorToInt((float)player.Score / 60);
            int seconds = Mathf.FloorToInt((float)player.Score % 60);
            int milliseconds = Mathf.FloorToInt(((float)player.Score % 1) * 100); // Extract 2-digit milliseconds

            scoreText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);

        }
        else
        {
            scoreText.text = player.Score.ToString(); ;
        }
    }
    
    private void Clicked()
    {
        PanelManager.CloseAll();
        ////Debug.Log("todo-> open profile: " + player.PlayerName);
        ProfileMenu panel = (ProfileMenu)PanelManager.GetSingleton("profile");
        panel.Open(player.PlayerId, player.PlayerName, myPageName);
        PanelManager.Open("loading");
    }
    
}