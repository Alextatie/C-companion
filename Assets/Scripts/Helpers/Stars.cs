using UnityEngine;
using UnityEngine.UI;
using System;
public class Stars : MonoBehaviour
{
    public GameObject round, menu;
    public Image s1, s2, s3;
    private float timer,finishTime;
    private Color  off = new Color(0.196f, 0.196f, 0.196f, 0.784f);
    private Color on = new Color(1f, 1f, 1f, 1f);
    private int score,flag1,flag2,flag3;
    public int typeflag;
    private bool anon;
    public static event Action<int> OnTotalStars = null;
    public static event Action<int> OnRushStars = null;
    public static event Action<int> OnFixerStars = null;
    public static event Action<float> OnRushScore = null;
    public static event Action<float> OnFixerScore = null;
    //private void Start()
    //{
    //    MainMenu.OnIfAnon += updateAnon;
    //}

    void OnEnable()
    {
        anon = menu.GetComponent<MainMenu>().getAnon();
        SoundManager.PlaySound("finish2");
        flag1 = flag2 = flag3 = 0;
        s1.color = s2.color= s3.color=off;
        if (typeflag == 0)
        {
            finishTime = round.GetComponent<TimeAttack>().getTime();
            score = round.GetComponent<TimeAttack>().getScore();
        }
        else
        {
            finishTime = round.GetComponent<CodeFixer>().getTime();
            score = round.GetComponent<CodeFixer>().getScore();
        }
        if ((score > 0)&&(!anon))
        {
            //Debug.Log("total stars: "+ score);
            OnTotalStars?.Invoke(score);
            if(typeflag == 0) 
            {
                //Debug.Log("Rush stars: "+ score);
                OnRushStars?.Invoke(score);
                if (score >= 3)
                {
                    OnRushScore?.Invoke(finishTime);
                }
            }
            else
            {
                OnFixerStars?.Invoke(score);
                if (score >= 3)
                {
                    OnFixerScore?.Invoke(finishTime);
                }
            }
        }
        timer = 0;
    }
    //private void updateAnon(bool isanon)
    //{
    //    anon = isanon;
    //}

    private void Update()
    {
        if (timer < 5f)
        {
            timer += Time.deltaTime;
            //int seconds = Mathf.FloorToInt(timer % 60);
            if ((timer >= 3f) && (score >= 3) && (flag3==0))
            {
                flag3 = 1;
                SoundManager.PlaySound("b3");
                s3.color = on;
            }
            else if ((timer >= 2.75f) && (score >= 2) && (flag2 == 0))
            {
                flag2 = 1;
                SoundManager.PlaySound("b2");
                s2.color = on;
            }
            else if ((timer >= 2.5f)&&(score >=1) && (flag1 == 0))
            {
                flag1 = 1;
                SoundManager.PlaySound("b1");
                s1.color = on;
            }
        }
    }
    //private void OnDestroy()
    //{
    //    MainMenu.OnIfAnon -= updateAnon;
    //}
}
