using UnityEngine;
using TMPro;
using UnityEngine.Windows;
using System;

public class CodeFixer : MonoBehaviour
{
    public GameObject round, finishb, deselect;
    private string input_a, input_b, input_c, input_d;
    public TextMeshProUGUI result;
    public GameObject[] levels;
    int[] levelReference;
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    public TMP_InputField output,ans;
    public TextMeshProUGUI answer, title;
    private int score, questions, currentLevel, levelsLeft, timeFlag,clockFlag, finishFlag,Difficulty, easyLeft, mediumLeft, hardLeft;
    void OnEnable()
    {
        title.text = "Round 1";
        answer.text = "";
        //output.text = "";
        timeFlag = 0;
        clockFlag = 0;
        finishFlag = 0;
        easyLeft = 0;
        mediumLeft = 0;
        hardLeft = 0;
        finishb.SetActive(false);
        deselect.SetActive(false);
        elapsedTime = 0;
        score = 0;
        questions = 1;
        if (Difficulty != 0)
        {
            setDifficulty(Difficulty);
        }
    }

    public void setDifficulty(int x)
    {
        Difficulty = x;
        switch (Difficulty)
        {
            case 1:
                break;
            case 2:
                easyLeft = 2;
                mediumLeft = 7;
                hardLeft = 0;
                break;
            case 3:
                easyLeft = 0;
                mediumLeft = 2;
                hardLeft = 7;
                break;
            default:
               //debug.log("difficulty error 0\n");
                break;
        }
        System.Random random = new System.Random();
        int randomNumber;
        levelReference = new int[70]; //total number of levels
        randomNumber = random.Next(1, 31); //total number of easy levels + 1
        levelsLeft = 9; //total number of easy levels - 1
        //Debug.Log(randomNumber);
        currentLevel = randomNumber;
        levels[currentLevel - 1].SetActive(true);
        levelReference[currentLevel - 1] = 1;
        for (int i = 0; i < 70; i++) //i dont know why this is needed, i almost blew my brains out trying to figure out why it bugs without it
        {
            if (levels[i].activeSelf)
            {
                if (i != (currentLevel - 1))
                {
                    levels[i].SetActive(false);
                }
            }
        }
    }
    public void newlevel()
    {
        input_a = input_b = input_c = input_d = "";
        levels[currentLevel - 1].transform.Find("Code").gameObject.transform.Find("answer").gameObject.GetComponent<TMP_InputField>().text = "";
        if (levels[currentLevel - 1].transform.Find("Code").gameObject.transform.Find("answer (1)") != null)
        {
            levels[currentLevel - 1].transform.Find("Code").gameObject.transform.Find("answer (1)").gameObject.GetComponent<TMP_InputField>().text = "";
        }
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(false);
        if (levelsLeft <= 1)
        {
            finishb.SetActive(true);
            //deselect.SetActive(true);
            //finish();
            //return;
        }
        answer.text = "";
        //output.text = "";
        questions += 1;
        title.text = "Round " + questions;
        int count = 0; //creating a randomizer
                       //int realSize;
                       //switch (Difficulty)
                       //{
                       //    case 1:
                       //        realSize = 5;
                       //        break;
                       //    case 2:
                       //        realSize = 7;
                       //        break;
                       //    case 3:
                       //        realSize = 10;
                       //        break;
                       //    default:
                       //        realSize = 0;
                       //        //Debug.Log("difficulty error 2\n");
                       //        break;
                       //}
        for (int i = 0; i < 70; i++) //used to be realSize instead of 30
        {
            if (levelReference[i] != 1)
            {
                count++;
            }
        }
        int[] nums = new int[count];
        int index = 0;
        for (int i = 0; i < 70; i++) //used to be realSize instead of 30
        {
            if (levelReference[i] != 1)
            {
                nums[index] = i;
                index++;
            }
        }
        //Debug.Log("easy: "+easyLeft+" medium: "+mediumLeft+" hard: "+hardLeft+"\n");
        switch (Difficulty)
        {
            case 1:
                levelsLeft -= 1;
                nextLevelChooser(1, nums);
                break;
            case 2:
                levelsLeft -= 1;
                nextLevelChooser(randomGenerator(), nums);
                break;
            case 3:
                levelsLeft -= 1;
                nextLevelChooser(randomGenerator(), nums);
                break;
            default:
                //Debug.Log("difficulty error 3\n");
                break;
        }
    }
    private int randomGenerator()
    {
        System.Random random = new System.Random();
        if (hardLeft == 0 && mediumLeft == 0)
        {
            //Debug.Log("xxx--1\n");
            easyLeft -= 1;
            return 1;
        }
        else if (hardLeft == 0 && easyLeft == 0)
        {
            mediumLeft -= 1;
            return 2;
        }
        else if (mediumLeft == 0 && easyLeft == 0)
        {
            hardLeft -= 1;
            return 3;
        }
        else if (hardLeft == 0)
        {
            int myRand = random.Next(1, 3);
            if (myRand == 1)
            {
                easyLeft -= 1;
            }
            else
            {
                mediumLeft -= 1;
            }
            //Debug.Log("xxx--2\n");
            return myRand;
        }
        else if (easyLeft == 0)
        {
            int myRand = random.Next(2, 4);
            if (myRand == 2)
            {
                mediumLeft -= 1;
            }
            else
            {
                hardLeft -= 1;
            }
            //Debug.Log("xxx--3\n");
            return myRand;
        }
        else if (mediumLeft == 0)
        {
            int myRand = random.Next(0, 2) * 2 + 1;
            if (myRand == 1)
            {
                easyLeft -= 1;
            }
            else
            {
                hardLeft -= 1;
            }
            //Debug.Log("xxx--4\n");
            return myRand;
        }
        else
        {
            int myRand = random.Next(1, 4);
            if (myRand == 1)
            {
                easyLeft -= 1;
            }
            else if (myRand == 2)
            {
                mediumLeft -= 1;
            }
            else
            {
                hardLeft -= 1;
            }
            //Debug.Log("xxx--5\n");
            return myRand;
        }

    }
    private void nextLevelChooser(int diff, int[] nums)
    {
       //debug.log(diff);
        int min, max;
        switch (diff)
        {
            case 1:
                min = 0;
                max = nums.Length - 40;
                break;
            case 2:
                min = 30;
                max = nums.Length - 20;
                break;
            case 3:
                min = 50;
                max = nums.Length;
                break;
            default:
               //debug.log("difficulty error 4\n");
                min = 0;
                max = 0;
                break;
        }
        System.Random random = new System.Random();
        int randomIndex = random.Next(min, max);
        int randomNumber = nums[randomIndex];
        levels[currentLevel - 1].SetActive(false);
        currentLevel = randomNumber + 1;
        levels[randomNumber].SetActive(true);
        levelReference[randomNumber] = 1;
    }
    void Update()
    {
        if (timeFlag == 0)
        {
            elapsedTime += Time.deltaTime;
        }
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        if (minutes >= 2)
        {
            timeFlag = 1;
            timerText.text = string.Format("<color=red>02:00");
            if (finishFlag == 0)
            {
                finishFlag = 1;
                finishb.SetActive(true);
                deselect.SetActive(true);
                finish();
            }
        }
        else if (elapsedTime>110)
        {
            if (clockFlag == 0)
            {
                clockFlag = 1;
                SoundManager.PlaySound("clock");
            }
            timerText.text = string.Format("<color=orange>{0:00}:{1:00}", minutes, seconds);
        }
        else if (elapsedTime > 100)
        {
            timerText.text = string.Format("<color=yellow>{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = string.Format("<color=white>{0:00}:{1:00}", minutes, seconds);
        }
    }
    public void disableLast()
    {
        levels[currentLevel - 1].SetActive(false);
    }

    public void ReadInput(string s)
    {
        input_a = s;
        //Debug.Log(input_a);
    }

    public void ReadDoubleInput(string s)
    {
        input_b = s;
        //Debug.Log(input_b);
    }
    public void ReadTrippleInput(string s)
    {
        input_c = s;
        //Debug.Log(input_b);
    }
    public void Read4Input(string s)
    {
        input_d = s;
        //Debug.Log(input_b);
    }
    private void timestop()
    {
        timeFlag = 1;
    }
    public void finish()
    {
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(false);
        input_a = input_b = input_c = input_d = "";
        levels[currentLevel - 1].transform.Find("Code").gameObject.transform.Find("answer").gameObject.GetComponent<TMP_InputField>().text = "";
        if (levels[currentLevel - 1].transform.Find("Code").gameObject.transform.Find("answer (1)")!= null)
        {
            levels[currentLevel - 1].transform.Find("Code").gameObject.transform.Find("answer (1)").gameObject.GetComponent<TMP_InputField>().text = "";
        }
        timeFlag = 1;
        string colora, colorb;
        int ok_a, good_a, ok_b, good_b;
        switch (Difficulty)
        {
            case 1:
                ok_a = 1;
                good_a = 2;
                ok_b = 1;
                good_b = 2;
                break;
            case 2:
                ok_a = 2;
                good_a = 3;
                ok_b = 2;
                good_b = 3;
                break;
            case 3:
                ok_a = 2;
                good_a = 4;
                ok_b = 2;
                good_b = 4;
                break;
            default:
                ok_a = 0;
                good_a = 0;
                ok_b = 0;
                good_b = 0;
                //Debug.Log("difficulty error 3\n");
                break;
        }
        if (score >= good_a)
        {
            colora = "<color=green>";
        }
        else if (score >= ok_a)
        {
            colora = "<color=yellow>";
        }
        else
        {
            colora = "<color=orange>";
        }
        if (questions >= good_b)
        {
            colorb = "<color=green>";
        }
        else if (questions >= ok_b)
        {
            colorb = "<color=yellow>";
        }
        else
        {
            colorb = "<color=orange>";
        }
        result.text = "You answered " + colora + score + "<color=white> out of " + colorb + questions + "<color=white> questions correctly!";
        //Debug.Log("Finished all.");
    }
    public float getTime()
    {
        return elapsedTime;
    }

    public int getScore()
    {
        if (score > 6)
        {
            return 3;
        }
        else if (score > 4)
        {
            return 2;
        }
        else if (score > 2)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void Q0(string s)
    {
        if (input_a == s)
        {
            //Debug.Log("correct");
            answer.text = "<color=green>Correct!";
            SoundManager.PlaySound("correct");
            score += 1;
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(true);
        if (levelsLeft <= 0)
        {
            finishb.SetActive(true);
            deselect.SetActive(true);
            timestop();
            return;
        }
    }

    public void Q2()
    {
        if ((input_a.TrimStart().StartsWith("//"))|| (input_a.TrimStart().StartsWith("/*") && input_a.EndsWith("*/")))
        {
            //Debug.Log("correct");
            answer.text = "<color=green>Correct!";
            string temp = ans.text;
            ans.text = "<color=#00ad12>"+temp;
            SoundManager.PlaySound("correct");
            score += 1;
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(true);
        if (levelsLeft <= 0)
        {
            finishb.SetActive(true);
            deselect.SetActive(true);
            timestop();
            return;
        }
    }

    public void Q36()
    {
        if (((input_a == "a")||(input_a == "*ptr"))&& ((input_b == "&a") || (input_b == "ptr")))
        {
            //Debug.Log("correct");
            answer.text = "<color=green>Correct!";
            SoundManager.PlaySound("correct");
            score += 1;
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(true);
        if (levelsLeft <= 0)
        {
            finishb.SetActive(true);
            deselect.SetActive(true);
            timestop();
            return;
        }
    }
    public void Q20()
    {
        if ((input_a == "true") || (int.TryParse(input_a, out int result) && result != 0)|| (input_a.StartsWith("\"") && input_a.EndsWith("\"")) || (input_a.StartsWith("\'") && input_a.EndsWith("\'")))
        {
            //Debug.Log("correct");
            answer.text = "<color=green>Correct!";
            SoundManager.PlaySound("correct");
            score += 1;
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(true);
        if (levelsLeft <= 0)
        {
            finishb.SetActive(true);
            deselect.SetActive(true);
            timestop();
            return;
        }
    }
    public void Q45()
    {
        if (input_a.StartsWith("x<"))
        {
            string numberPart = input_a.Substring(2);
            if (int.TryParse(numberPart, out int number))
            {
                if (number >= 17 && number <= 32)
                {
                    //Debug.Log("correct");
                    answer.text = "<color=green>Correct!";
                    SoundManager.PlaySound("correct");
                    score += 1;
                }
                else
                {
                    //Debug.Log("false");
                    answer.text = "<color=red>Wrong!";
                    SoundManager.PlaySound("wrong");
                }
            }
            else
            {
                //Debug.Log("false");
                answer.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
            }
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(true);
        if (levelsLeft <= 0)
        {
            finishb.SetActive(true);
            deselect.SetActive(true);
            timestop();
            return;
        }
    }
    public void Q54()
    {
        if ((input_a == "char*") || (input_a == "char* ") || (input_a == "char *") )
        {
            //Debug.Log("correct");
            answer.text = "<color=green>Correct!";
            SoundManager.PlaySound("correct");
            score += 1;
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(true);
        if (levelsLeft <= 0)
        {
            finishb.SetActive(true);
            deselect.SetActive(true);
            timestop();
            return;
        }
    }

    public void Q56()
    {
        if (((input_a == "printf(\"%d\", num);") || (input_a == "printf(\"%d\",num);") || (input_a == "printf(\"%d\" ,num);"))&&((input_b.Trim()=="")|| (input_b == null)))
        {
            //Debug.Log("correct");
            answer.text = "<color=green>Correct!";
            SoundManager.PlaySound("correct");
            score += 1;
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(true);
        if (levelsLeft <= 0)
        {
            finishb.SetActive(true);
            deselect.SetActive(true);
            timestop();
            return;
        }
    }
    public void Q57()
    {
        if (((input_a == "int num = 10;") || (input_a == "int num =10;") || (input_a == "int num= 10;") || (input_a == "int num=10;")) && ((input_b.Trim() == "") || (input_b == null)))
        {
            //Debug.Log("correct");
            answer.text = "<color=green>Correct!";
            SoundManager.PlaySound("correct");
            score += 1;
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(true);
        if (levelsLeft <= 0)
        {
            finishb.SetActive(true);
            deselect.SetActive(true);
            timestop();
            return;
        }
    }
    public void Q58()
    {
        if ((input_a == "int num") && (input_b == "char letter") && ((input_c == "char text[]") ||(input_c == "char *text") ||(input_c == "char* text")))
        {
            //Debug.Log("correct");
            answer.text = "<color=green>Correct!";
            SoundManager.PlaySound("correct");
            score += 1;
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(true);
        if (levelsLeft <= 0)
        {
            finishb.SetActive(true);
            deselect.SetActive(true);
            timestop();
            return;
        }
    }
    public void Q52()
    {
        if ((input_a == "a*b")  || (input_a == "b*a"))
        {
            //Debug.Log("correct");
            answer.text = "<color=green>Correct!";
            SoundManager.PlaySound("correct");
            score += 1;
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(true);
        if (levelsLeft <= 0)
        {
            finishb.SetActive(true);
            deselect.SetActive(true);
            timestop();
            return;
        }
    }

    public void Q62()
    {
        if ((input_a == "n==1") || (input_a == "n-1"))
        {
            //Debug.Log("correct");
            answer.text = "<color=green>Correct!";
            SoundManager.PlaySound("correct");
            score += 1;
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(true);
        if (levelsLeft <= 0)
        {
            finishb.SetActive(true);
            deselect.SetActive(true);
            timestop();
            return;
        }
    }
    public void Q63()
    {
        if ((input_a == "b==0") || (input_a == "a"))
        {
            //Debug.Log("correct");
            answer.text = "<color=green>Correct!";
            SoundManager.PlaySound("correct");
            score += 1;
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
        levels[currentLevel - 1].transform.Find("AmericanBlock").gameObject.SetActive(true);
        if (levelsLeft <= 0)
        {
            finishb.SetActive(true);
            deselect.SetActive(true);
            timestop();
            return;
        }
    }
}
