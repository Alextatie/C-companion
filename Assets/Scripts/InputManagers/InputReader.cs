using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;
using Unity.VisualScripting;

public class InputReader : MonoBehaviour
{
    private string input, input_b, input_c, input_d, input_e, input_f, user_i, user_i_2, save, saved;
    private int user_flag, selected;
    public int stage;
    public GameObject user_input,user_input_2;
    public TMP_InputField output;
    public TextMeshProUGUI answer, a2, a3, a4;
    public static event Action<string,int> OnUnlockedButton = null;
    public static event Action<string,int,int> OnUnlockedMultiple = null;
    // Start is called before the first frame update
    void Start()
    {
        user_flag = 0;
        selected = 0;
        LessonMenu.OnResetIn += OnResetInputs;
    }
    public void OnResetInputs()
    {

        input = input_b = input_c = input_d = input_e = input_f = "";
    }

    // Update is called once per frame
    void Update()
    {
        if ((user_flag == 1) && (selected == 1))
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                SoundManager.PlaySound("click");
                ////Debug.Log(user_input.GetComponent<TMP_InputField>().text);
                switch (stage)
                {
                    case 150:
                        Q15_1();
                        break;
                    case 152:
                        Q15_3();
                        break;
                    case 154:
                        Q15_6();
                        break;
                    case 155:
                        Q15_7();
                        break;
                    case 181:
                        Q15_81();
                        break;

                    default:
                        break;
                }
            }
        }
    }

    private void OnDestroy()
    {
        LessonMenu.OnResetIn -= OnResetInputs;
    }


    public void select(int s)
    {
        selected = 1;
        stage = s;
    }
    public void deselect()
    {
        selected = 0;
    }
    public void ReadInput(string s)
    {
        input = s;
        ////Debug.Log(input);
    }

    public void ReadDoubleInput(string s)
    {
        input_b = s;
        ////Debug.Log(input_b);
    }
    public void ReadTrippleInput(string s)
    {
        input_c = s;
        ////Debug.Log(input_c);
    }
    public void Read4Input(string s)
    {
        input_d = s;
        ////Debug.Log(input_d);
    }
    public void Read5Input(string s)
    {
        input_e = s;
        ////Debug.Log(input_e);
    }
    public void Read6Input(string s)
    {
        input_f = s;
        ////Debug.Log(input_f);
    }

    public void Q1_00()
    {
        ////Debug.Log("true");
        output.text = "<color=white>Hello World!";
        SoundManager.PlaySound("correct");
    }

    public void Q1_01()
    {
        //Debug.Log("wrong");
        output.text = "<color=red>Error!";
        SoundManager.PlaySound("wrong");
    }
    public void Q1_1()
    {
        //return input is "printf";
        if (input is "printf")
        {
            //Debug.Log("true");
            answer.text = "<color=green>Correct!";
            output.text = "<color=white>Hello World!";
            SoundManager.PlaySound("correct");
            OnUnlockedButton?.Invoke("output", 1);
        }
        else if (input is "Printf")
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong! try lowercase";
            output.text = "<color=red>Error!";
            SoundManager.PlaySound("wrong");
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong!";
            output.text = "<color=red>Error!";
            SoundManager.PlaySound("wrong");
        }
    }
    public void Q1_2()
    {
        if (input is "printf(\"Hello World!\");")
        {
            //Debug.Log("true");
            answer.text = "<color=green>Correct!";
            output.text = "<color=white>Hello World!";
            SoundManager.PlaySound("correct");
            OnUnlockedButton?.Invoke("output",0);
        }
        else
        {
            //Debug.Log("false");
            
            answer.text = "<color=red>Wrong!";
            output.text = "<color=red>Error!";
            SoundManager.PlaySound("wrong");
        }
    }

    public void Q2_1()
    {
        if (((input == null) || (input.Trim().Length == 0)) && ((input_b == null) || (input_b.Trim().Length == 0)))
        {
            //Debug.Log("false");
            answer.text = "<color=red>Wrong! You didn't write any comment.";
            output.text = "<color=white>Hello World!\nI learned how to write comments!";
            SoundManager.PlaySound("wrong");
        }
        else if ((input_b == null) || (input_b.Trim().Length == 0))
        {
            if ((input.StartsWith("//")) || ((input.StartsWith("/*")) && (input.EndsWith("*/"))))
            {
                //Debug.Log("true");
                answer.text = "<color=green>Correct!";
                output.text = "<color=white>Hello World!\nI learned how to write comments!";
                SoundManager.PlaySound("correct");
                OnUnlockedButton?.Invoke("comments", 0);
            }
        }
        else if ((input == null) || (input.Trim().Length == 0))
        {
            if ((input_b.StartsWith("//")) || ((input_b.StartsWith("/*")) && (input_b.EndsWith("*/"))))
            {
                //Debug.Log("true");
                answer.text = "<color=green>Correct!";
                output.text = "<color=white>Hello World!\nI learned how to write comments!";
                SoundManager.PlaySound("correct");
                OnUnlockedButton?.Invoke("comments", 0);
            }
        }
        else
        {
            if (((input.StartsWith("//")) && (input_b.StartsWith("//"))) || (((input.StartsWith("/*")) && (input_b.EndsWith("*/")))) || (((input.StartsWith("/*")) && (input.EndsWith("*/")))) && (input_b.StartsWith("//")) || (((input_b.StartsWith("/*")) && (input_b.EndsWith("*/")))) && (input.StartsWith("//")))
            {
                //Debug.Log("true");
                answer.text = "<color=green>Correct!";
                output.text = "<color=white>Hello World!\nI learned how to write comments!";
                SoundManager.PlaySound("correct");
                OnUnlockedButton?.Invoke("comments", 0);
            }
            else
            {
                //Debug.Log("false");
                answer.text = "<color=red>Wrong!";
                output.text = "<color=red>Error!";
                SoundManager.PlaySound("wrong");
            }
        }
    }

    public void Q3_0()
    {
        //Debug.Log("true");
        output.text = "<color=white>15";
        SoundManager.PlaySound("correct");
    }

    public void Q3_01()
    {
        //Debug.Log("true");
        output.text = "<color=white>15\n189.13\nz";
        SoundManager.PlaySound("correct");
    }
    public void Q3_02()
    {
        //Debug.Log("true");
        output.text = "<color=white>15189.13z";
        SoundManager.PlaySound("correct");
    }
    public void Q3_03()
    {
        //Debug.Log("true");
        output.text = "<color=white>My name starts with A and I'm 30 years old";
        SoundManager.PlaySound("correct");
    }

    public void Q3_04()
    {
        //Debug.Log("true");
        output.text = "<color=white>My favorite number is: 27\nMy favorite letter is: K";
        SoundManager.PlaySound("correct");
    }

    public void Q3_05()
    {
        if ((input == "%d") && (input_b == "bob") && (input_c == "%c") && (input_d == "%c") && (input_e == "math") && (input_f == "geo"))
        {
            //Debug.Log("true");
            answer.text = "<color=green>Correct!";
            output.text = "<color=white>My brother Bob is 14 years old.\nHe got A on his math test, and C on his last geography test.\n";
            SoundManager.PlaySound("correct");
            OnUnlockedButton?.Invoke("variables", 1);
        }
        else if ((input == "%d") && (input_b == "bob") && (input_c == "%c") && (input_d == "%c") && (input_e == "geo") && (input_f == "math"))
        {
            //Debug.Log("true");
            answer.text = "<color=red>Wrong!\nThe code is functionally correct\nbut you swapped the scores.\n";
            output.text = "<color=white>My brother Bob is 14 years old.\nHe got C on his math test, and A on his last geography test.\n";
            SoundManager.PlaySound("correct");
        }
        else
        {
            //Debug.Log("false");
            SoundManager.PlaySound("wrong");
            if (input == "%d")
            {
                answer.text = "<color=green>1) Correct\n";
            }
            else
            {
                answer.text = "<color=red>1) Wrong\n";
            }
            if (input_b == "bob")
            {
                answer.text += "<color=green>2) Correct\n";
            }
            else
            {
                answer.text += "<color=red>2) Wrong\n";
            }
            if (input_c == "%c")
            {
                answer.text += "<color=green>3) Correct\n";
            }
            else
            {
                answer.text += "<color=red>3) Wrong\n";
            }
            if (input_d == "%c")
            {
                answer.text += "<color=green>4) Correct\n";
            }
            else
            {
                answer.text += "<color=red>4) Wrong\n";
            }
            if (input_e == "math")
            {
                answer.text += "<color=green>5) Correct\n";
            }
            else
            {
                answer.text += "<color=red>5) Wrong\n";
            }
            if (input_f == "geo")
            {
                answer.text += "<color=green>6) Correct\n";
            }
            else
            {
                answer.text += "<color=red>6) Wrong\n";
            }
        }

    }

    public void Q3_61()
    {
        //Debug.Log("true");
        output.text = "<color=white>55";
        SoundManager.PlaySound("correct");
    }

    public void Q3_62()
    {
        //Debug.Log("true");
        output.text = "<color=white>15";
        SoundManager.PlaySound("correct");
    }
    public void Q3_71()
    {
        //Debug.Log("true");
        output.text = "<color=white>32096";
        SoundManager.PlaySound("correct");
    }
    public void Q3_72()
    {
        //Debug.Log("true");
        output.text = "<color=white>33";
        SoundManager.PlaySound("correct");
    }

    public void Q3_81()
    {
        //Debug.Log("true");
        output.text = "<color=white>12, 6, 35";
        SoundManager.PlaySound("correct");
    }
    public void Q3_82()
    {
        //Debug.Log("true");
        output.text = "<color=white>71, 71, 71";
        SoundManager.PlaySound("correct");
    }

    public void Q3_9()
    {
        //Debug.Log("false");
        answer.text = "<color=white>You cannot change a const";
        output.text = "<color=red>Error!";
        SoundManager.PlaySound("wrong");
    }
    public void Q3_101()
    {
        //Debug.Log("true");
        output.text = "<color=white>A, B, C";
        SoundManager.PlaySound("correct");
    }
    public void Q3_102()
    {
        //Debug.Log("true");
        output.text = "<color=white>3.500000\n3.5\n3.500";
        SoundManager.PlaySound("correct");
    }
    public void Q3_103()
    {
        //Debug.Log("true");
        output.text = "<color=white>2.000000\n2.500000";
        SoundManager.PlaySound("correct");
    }

    public void Q4_1(int option)
    {
        switch (option)
        {
            case 1:
                //Debug.Log("false");
                answer.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 2:
                //Debug.Log("false");
                answer.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 3:
                //Debug.Log("true");
                answer.text = "<color=green>Correct! 5 * 3 = 15";
                SoundManager.PlaySound("correct");
                OnUnlockedMultiple?.Invoke("operators", 0,0);
                break;
            case 4:
                //Debug.Log("false");
                answer.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;

            default:
                break;
        }
    }

    public void Q4_2(int option)
    {
        switch (option)
        {
            case 1:
                //Debug.Log("true");
                a2.text = "<color=green>Correct! 15 + 1 = 16";
                SoundManager.PlaySound("correct");
                OnUnlockedMultiple?.Invoke("operators", 0, 1);
                break;
            case 2:
                //Debug.Log("false");
                a2.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 3:
                //Debug.Log("false");
                a2.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 4:
                //Debug.Log("false");
                a2.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;

            default:
                break;
        }
    }

    public void Q4_3(int option)
    {
        switch (option)
        {
            case 1:
                //Debug.Log("false");
                a3.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 2:
                //Debug.Log("true");
                a3.text = "<color=green>Correct! 10 % 4 = 2\n2 isn't bigger than 2";
                SoundManager.PlaySound("correct");
                OnUnlockedMultiple?.Invoke("operators", 0, 2);
                break;
            case 3:
                //Debug.Log("false");
                a3.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 4:
                //Debug.Log("false");
                a3.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;

            default:
                break;
        }
    }
    public void Q5()
    {
        //Debug.Log("true");
        output.text = "<color=white>1\n0";
        answer.text = "Notice that we use <color=red>%d<color=white> for the data type <color=red>bool<color=white> since it's value is returned as an integer,\r\n<color=red>1<color=white> for True, and <color=red>0<color=white> for False.\nIn <color=red>C<color=white>, any <color=red>nonzero<color=white> value, whether it's a number, character, or string, evaluates to <color=red>true<color=white> when assigned to a bool.";
        SoundManager.PlaySound("correct");
    }

    public void Q11_00()
    {
        //Debug.Log("true");
        output.text = "<color=white>10 is greater than 5";
        SoundManager.PlaySound("correct");
    }

    public void Q11_01()
    {
        //Debug.Log("true");
        output.text = "<color=white>x is greater than y";
        SoundManager.PlaySound("correct");
    }
    public void Q11_1()
    {
        //Debug.Log("true");
        output.text = "<color=white>y is greater than x";
        SoundManager.PlaySound("correct");
    }
    public void Q11_3()
    {
        //Debug.Log("true");
        output.text = "<color=white>x and y are equal";
        SoundManager.PlaySound("correct");
    }
    public void Q11_2()
    {
        //Debug.Log("true");
        output.text = "<color=white>You are old";
        SoundManager.PlaySound("correct");
    }

    public void Q11_21()
    {
        int num;
        if (int.TryParse(input, out num))
        {
            if (num < 10)
            {
                answer.text = "<color=green>Correct!";
                output.text = "<color=white>x";
                SoundManager.PlaySound("correct");
                OnUnlockedMultiple?.Invoke("ifelse", 0, 0);
            }
            else if (num > 10)
            {
                answer.text = "<color=red>Wrong!";
                output.text = "<color=white>y";
                SoundManager.PlaySound("wrong");
            }
            else
            {
                answer.text = "<color=red>Wrong!";
                output.text = "<color=white>z";
                SoundManager.PlaySound("wrong");
            }
        }
        else
        {
            answer.text = "<color=red>Wrong!";
            output.text = "<color=red>Error";
            SoundManager.PlaySound("wrong");
        }
    }
    public void Q11_22()
    {
        int num;
        if (int.TryParse(input_b, out num))
        {
            if (num < 10)
            {
                answer.text = "<color=red>Wrong!";
                output.text = "<color=white>x";
                SoundManager.PlaySound("wrong");
            }
            else if (num > 10)
            {
                answer.text = "<color=green>Correct!";
                output.text = "<color=white>y";
                SoundManager.PlaySound("correct");
                OnUnlockedMultiple?.Invoke("ifelse", 0, 1);
            }
            else
            {
                answer.text = "<color=red>Wrong!";
                output.text = "<color=white>z";
                SoundManager.PlaySound("wrong");
            }
        }
        else
        {
            answer.text = "<color=red>Wrong!";
            output.text = "<color=red>Error";
            SoundManager.PlaySound("wrong");
        }
    }

    public void Q11_23()
    {
        int num;
        if (int.TryParse(input_c, out num))
        {
            if (num < 10)
            {
                answer.text = "<color=red>Wrong!";
                output.text = "<color=white>x";
                SoundManager.PlaySound("wrong");
            }
            else if (num > 10)
            {
                answer.text = "<color=red>Wrong!";
                output.text = "<color=white>y";
                SoundManager.PlaySound("wrong");
            }
            else
            {
                answer.text = "<color=green>Correct!";
                output.text = "<color=white>z";
                SoundManager.PlaySound("correct");
                OnUnlockedMultiple?.Invoke("ifelse", 0, 2);
            }
        }
        else
        {
            answer.text = "<color=red>Wrong!";
            output.text = "<color=red>Error";
            SoundManager.PlaySound("wrong");
        }
    }
    public void Q12()
    {
        //Debug.Log("true");
        output.text = "<color=white>March";
        SoundManager.PlaySound("correct");
    }

    public void Q12_01()
    {
        int num;
        if (int.TryParse(input, out num))
        {
            if (Convert.ToInt32(input) == 2)
            {
                answer.text = "<color=green>Correct!";
                SoundManager.PlaySound("correct");
                OnUnlockedButton?.Invoke("switch", 0);
            }
            else
            {
                answer.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
            }
        }
        else
        {
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
    }

    public void Q13_0()
    {
        //Debug.Log("true");
        answer.text = "<color=white>The code in the <color=red>loop<color=white> will run, again and again, as long as the variable <color=red>i<color=white> is less than <color=red>5<color=white>. The moment the condition stops being true (when <color=red>i<color=white> = <color=red>5<color=white>), we will exit the loop.";
        output.text = "<color=white>0\n1\n2\n3\n4\n";
        SoundManager.PlaySound("correct");
    }

    public void Q13_1()
    {
        //Debug.Log("true");
        answer.text = "<color=white>Remember to increment your condition variable or else the loop will run forever (and could possibly crush your application)";
        output.text = "<color=white>0\n1\n2\n3\n4\n";
        SoundManager.PlaySound("correct");
    }

    public void Q13_2()
    {
        if (input != null)
        {
            input = input.Replace(" ", "");
        }
        if (input_b != null)
        {
            input_b = input_b.Replace(" ", "");
        }

        if (input == "5")
        {
            if ((input_b == "i>0") || (input_b == "i>=1"))
            {
                answer.text = "<color=green>Correct!";
                SoundManager.PlaySound("correct");
                OnUnlockedButton?.Invoke("loops", 1);
            }
            else
            {
                answer.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
            }
        }
        else
        {
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
    }

    public void Q13_3()
    {
        //Debug.Log("true");
        output.text = "<color=white>0\n1\n2\n3\n4\n";
        SoundManager.PlaySound("correct");
    }

    public void Q13_4()
    {
        //Debug.Log("true");
        output.text = "<color=white>Outer: 1\n Inner: 1\n Inner: 2\n Inner: 3\nOuter: 2\n Inner: 1\n Inner: 2\n Inner: 3\nOuter: 3\n Inner: 1\n Inner: 2\n Inner: 3\n";
        answer.text = "<color=white>Notice that the <color=red>inner loop<color=white> completed all of it's iterations per every single iteration of the <color=red>outer loop<color=white>";
        SoundManager.PlaySound("correct");
    }
    public void Q13_50()
    {
        //Debug.Log("true");
        output.text = "<color=white>0\n1\n2\n3\n4\n";
        answer.text = "<color=white>Exits the entire loop at 5";
        SoundManager.PlaySound("correct");
    }
    public void Q13_51()
    {
        //Debug.Log("true");
        output.text = "<color=white>0\n1\n2\n3\n4\n6\n7\n8";
        answer.text = "<color=white>Skips the iteration at 5, and continues to the next one iteration";
        SoundManager.PlaySound("correct");
    }
    public void Q14()
    {
        //Debug.Log("true");
        System.Random random = new System.Random();
        int r = random.Next(100000000, 1000000000);
        int m = random.Next(0, 2);
        if (m == 0)
        {
            output.text = "<color=white>" + r.ToString();
        }
        else
        {
            output.text = "<color=white>-" + r.ToString();
        }
        SoundManager.PlaySound("correct");
    }

    public void Q14_0()
    {
        //Debug.Log("true");
        output.text = "<color=white>8";
        answer.text = "<color=white>Index <color=red>2<color=white> is the 3rd number in our array.";
        SoundManager.PlaySound("correct");
    }

    public void Q14_1()
    {
        //Debug.Log("true");
        output.text = "<color=white>77";
        answer.text = "<color=white>Index <color=red>2<color=white> changed from 8 to 77.";
        SoundManager.PlaySound("correct");
    }
    public void Q14_3()
    {
        //Debug.Log("true");
        output.text = "<color=white>13 52 8 1011";
        SoundManager.PlaySound("correct");
    }
    public void Q14_4()
    {
        //Debug.Log("true");
        output.text = "<color=white>16";
        answer.text = "<color=white>Why is the number <color=red>16<color=white> and not <color=red>4<color=white>?\nThe <color=red>sizeof()<color=white> operator returns the size of a type in <color=red>bytes<color=white>.";
        SoundManager.PlaySound("correct");
    }
    public void Q14_5()
    {
        //Debug.Log("true");
        output.text = "<color=white>4";
        SoundManager.PlaySound("correct");
    }
    public void Q14_6()
    {
        if (input == null)
        {
            //Debug.Log("wrong" + input);
            output.text = "<color=white>";
            answer.text = "<color=red>Wrong!";
            SoundManager.PlaySound("wrong");
        }
        else
        {
            input = input.Replace(" ", "");
            if (input == "300")
            {
                //Debug.Log("true");
                output.text = "<color=white>300";
                answer.text = "<color=green>Correct!\n75<color=white>(the third element)<color=green> * 4<color=white>(size of int)<color=green> = 300";
                SoundManager.PlaySound("correct");
                OnUnlockedButton?.Invoke("arrays", 1);
            }
            else
            {
                //Debug.Log("wrong"+ input);
                output.text = "<color=white>";
                answer.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
            }
        }
    }
    public void Q14_80()
    {
        //Debug.Log("true");
        output.text = "<color=white>7";
        SoundManager.PlaySound("correct");
    }
    public void Q14_81()
    {
        //Debug.Log("true");
        output.text = "<color=white>9";
        SoundManager.PlaySound("correct");
    }
    public void Q14_9()
    {
        //Debug.Log("true");
        output.text = "<color=white>1 3 7\n4 0 5";
        SoundManager.PlaySound("correct");
    }

    public void Q14_100()
    {
        //Debug.Log("true");
        output.text = "<color=white>Hello World!";
        SoundManager.PlaySound("correct");
    }
    public void Q14_101()
    {
        //Debug.Log("true");
        output.text = "<color=white>Helxo World!";
        SoundManager.PlaySound("correct");
    }
    public void Q14_110()
    {
        //Debug.Log("true");
        output.text = "<color=white>H e l l o !";
        SoundManager.PlaySound("correct");
    }
    public void Q14_12()
    {
        //Debug.Log("true");
        output.text = "<color=white><size=28><color=white>He said \"awesome\" during the meeting.\nIt\'s okay.\nThe character \\ is called a backslash.";
        SoundManager.PlaySound("correct");
    }

    public void Q14_131()
    {
        //Debug.Log("true");
        output.text = "<color=white>8";
        SoundManager.PlaySound("correct");
    }
    public void Q14_132()
    {
        //Debug.Log("true");
        output.text = "<color=white>ABCDEFGHHello!";
        SoundManager.PlaySound("correct");
    }
    public void Q14_133()
    {
        //Debug.Log("true");
        output.text = "<color=white>ABCDEFGH";
        SoundManager.PlaySound("correct");
    }
    public void Q14_134()
    {
        //Debug.Log("true");
        output.text = "<color=white><size=28><color=white>-7";
        SoundManager.PlaySound("correct");
    }

    public void Q14_141()
    {
        if ((input_b == "arr[i]"))
        {
            switch (input)
            {
                case "0":
                    //Debug.Log("false");
                    output.text = "<color=white><size=32>The sum is: 0";
                    answer.text = "<color=red><size=32>Wrong!";
                    SoundManager.PlaySound("wrong");
                    break;
                case "1":
                    //Debug.Log("false");
                    output.text = "<color=white><size=32>The sum is: 1";
                    answer.text = "<color=red><size=32>Wrong!";
                    SoundManager.PlaySound("wrong");
                    break;
                case "2":
                    //Debug.Log("false");
                    output.text = "<color=white><size=32>The sum is: 3";
                    answer.text = "<color=red><size=32>Wrong!";
                    SoundManager.PlaySound("wrong");
                    break;
                case "3":
                    //Debug.Log("false");
                    output.text = "<color=white><size=32>The sum is: 6";
                    answer.text = "<color=red><size=32>Wrong!";
                    SoundManager.PlaySound("wrong");
                    break;
                case "4":
                    //Debug.Log("false");
                    output.text = "<color=white><size=32>The sum is: 10";
                    answer.text = "<color=red><size=32>Wrong!";
                    SoundManager.PlaySound("wrong");
                    break;
                case "5":
                    //Debug.Log("true");
                    output.text = "<color=white><size=32>The sum is: 15";
                    answer.text = "<color=green><size=32>Correct!\nWe will add the current element to the sum in every stage until we summed them all.";
                    SoundManager.PlaySound("correct");
                    OnUnlockedMultiple?.Invoke("arrays", 0, 0);
                    break;

                default:
                    //Debug.Log("false");
                    output.text = "<color=red><size=32>Error!";
                    answer.text = "<color=red><size=32>Wrong!";
                    SoundManager.PlaySound("wrong");
                    break;
            }
        }
        else if (float.TryParse(input_b, out float number))
        {
            if (int.TryParse(input, out int numx))
            {
                if (numx >= 0)
                {
                    //Debug.Log("false");
                    output.text = "<color=white><size=32>The sum is: " + numx * (int)number;
                    answer.text = "<color=red><size=32>Wrong!";
                    SoundManager.PlaySound("wrong");
                }
                else
                {
                    //Debug.Log("false");
                    output.text = "<color=red><size=32>Error!";
                    answer.text = "<color=red><size=32>Wrong!";
                    SoundManager.PlaySound("wrong");
                }

            }
            else
            {
                //Debug.Log("false");
                output.text = "<color=red><size=32>Error!";
                answer.text = "<color=red><size=32>Wrong!";
                SoundManager.PlaySound("wrong");
            }
        }
        else if (input_b == "sum")
        {
            if (int.TryParse(input, out int numx))
            {
                if (numx >= 0)
                {
                    //Debug.Log("false");
                    output.text = "<color=white><size=32>The sum is: 0";
                    answer.text = "<color=red><size=32>Wrong!";
                    SoundManager.PlaySound("wrong");
                }
                else
                {
                    //Debug.Log("false");
                    output.text = "<color=red><size=32>Error!";
                    answer.text = "<color=red><size=32>Wrong!";
                    SoundManager.PlaySound("wrong");
                }
            }
            else
            {
                //Debug.Log("false");
                output.text = "<color=red><size=32>Error!";
                answer.text = "<color=red><size=32>Wrong!";
                SoundManager.PlaySound("wrong");
            }
        }
        else if ((input_b == "arr[0]") || (input_b == "arr[1]") || (input_b == "arr[2]") || (input_b == "arr[3]") | (input_b == "arr[4]"))
        {
            if (int.TryParse(input, out int numa))
            {
                if (numa >= 0)
                {
                    //Debug.Log("false");
                    string b = input_b.Substring(4, input_b.Length - 5);
                    int.TryParse(b, out int numb);
                    output.text = "<color=white><size=32>The sum is: " + (numa  * (numb+1));
                    answer.text = "<color=red><size=32>Wrong!";
                    SoundManager.PlaySound("wrong");
                }
                else
                {
                    //Debug.Log("false");
                    output.text = "<color=red><size=32>Error!";
                    answer.text = "<color=red><size=32>Wrong!";
                    SoundManager.PlaySound("wrong");
                }
            }
            else
            {
                //Debug.Log("false");
                output.text = "<color=red><size=32>Error!";
                answer.text = "<color=red><size=32>Wrong!";
                SoundManager.PlaySound("wrong");
            }
        }
        else
        {
            //Debug.Log("false");
            output.text = "<color=red><size=32>Error!";
            answer.text = "<color=red><size=32>Wrong!";
            SoundManager.PlaySound("wrong");
        }
    }

    public void Q14_142(int ans)
    {
        switch (ans)
        {
            case 1:
                //Debug.Log("false");
                output.text = "<color=white><size=32>Odd elements:\n2\n4";
                answer.text = "<color=red><size=32>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 2:
                //Debug.Log("false");
                output.text = "<color=white><size=32>Odd elements:\n3\n4\n5";
                answer.text = "<color=red><size=32>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 3:
                //Debug.Log("correct");
                output.text = "<color=white><size=32>Odd elements:\n1\n3\n5";
                answer.text = "<color=green><size=32>Correct!\n<color=white>The <color=red>i<color=white>'s that divide by 2 without a reminder are <color=red>0<color=white>,<color=red>2<color=white>, and <color=red>4<color=white>, which are the indexes for the elements <color=red>1<color=white>,<color=red>3<color=white>, and <color=red>5<color=white>.";
                SoundManager.PlaySound("correct");
                OnUnlockedMultiple?.Invoke("arrays", 0, 1);
                break;
            case 4:
                //Debug.Log("false");
                output.text = "<color=white><size=32>Odd elements:\n1\n2";
                answer.text = "<color=red><size=32>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
        }
    }

    public void Q15_0()
    {
        ////Debug.Log("true");
        user_input.GetComponent<TMP_InputField>().text = "";
        output.text = "<color=white><color=white>Enter a number:";
        user_input.SetActive(true);
        user_input.GetComponent<TMP_InputField>().interactable = true;
        user_flag = 1;
        //SoundManager.PlaySound("correct");
    }

    public void Q15_1()
    {
        if (user_input.GetComponent<TMP_InputField>().text != null)
        {
            //Debug.Log(user_input.GetComponent<TMP_InputField>().text);
            int num;
            if (int.TryParse(user_input.GetComponent<TMP_InputField>().text, out num))
            {
                output.text = "<color=white><color=white>Enter a number:\nYour number is: " + num;
                SoundManager.PlaySound("correct");
            }
            else
            {
                output.text = "<color=white><color=white>Enter a number:\n<color=red>Error!";
                SoundManager.PlaySound("wrong");
            }
        }
        else
        {
            //Debug.Log(user_input.GetComponent<TMP_InputField>().text);
            output.text = "<color=white><color=white>Enter a number:\n<color=red>Error!";
            SoundManager.PlaySound("wrong");
        }
        user_input.GetComponent<TMP_InputField>().interactable = false;
    }

    public void Q15_2()
    {
        ////Debug.Log("true");
        user_input.GetComponent<TMP_InputField>().text = "";
        output.text = "<color=white>Enter a number and a character, then press enter:\n";
        user_input.SetActive(true);
        user_input.GetComponent<TMP_InputField>().interactable = true;
        user_flag = 1;
        //SoundManager.PlaySound("correct");
    }
    public void Q15_3()
    {
        if (saved == null)
        {
            if ((user_input.GetComponent<TMP_InputField>().text != null) && (user_input.GetComponent<TMP_InputField>().text.Replace(" ", "") != ""))
            {
                string word = user_input.GetComponent<TMP_InputField>().text.Trim();
                int fl = 0;
                if (word.IndexOf(' ') >= 0)
                {
                    word = Regex.Replace(word, @"\s+", " ").Trim();
                    string[] words = word.Split(new char[] { ' ' }, 2);
                    bool isInt = int.TryParse(words[0], out _);
                    if (isInt)
                    {
                        output.text = "<color=white>Enter a number and a character, then press enter:\nYour number is: " + words[0] + "\nYour character is: " + words[1][0];

                    }
                    else
                    {
                        System.Random random = new System.Random();
                        int r = random.Next(30000, 39999);
                        output.text = "<color=white>Enter a number and a character, then press enter:\nYour number is: " + r.ToString() + "\nYour character is: ?";
                    }
                }
                else
                {
                    string numberPart = "";
                    char letterPart = '\0';
                    foreach (char ch in word)
                    {
                        if (char.IsDigit(ch))
                        {
                            numberPart += ch;  // Build the number part
                        }
                        else
                        {
                            letterPart = ch;  // The first non-digit character becomes the letter part
                            break;  // We stop as soon as we find the first character
                        }
                    }
                    if (numberPart == "")
                    {
                        System.Random random = new System.Random();
                        int r = random.Next(30000, 39999);
                        output.text = "<color=white>Enter a number and a character, then press enter:\nYour number is: " + r.ToString() + "\nYour character is: ?";
                    }
                    else if (letterPart == '\0')
                    {
                        saved = numberPart;
                        fl = 1;
                        user_input.GetComponent<TMP_InputField>().text = "";
                    }
                    else
                    {
                        output.text = "<color=white>Enter a number and a character, then press enter:\nYour number is: " + numberPart + "\nYour character is: " + letterPart;
                    }
                }
                if (fl == 0)
                {
                    user_input.GetComponent<TMP_InputField>().interactable = false;
                    SoundManager.PlaySound("correct");
                }
            }
        }
        else
        {
            if ((user_input.GetComponent<TMP_InputField>().text != null) && (user_input.GetComponent<TMP_InputField>().text.Replace(" ", "") != ""))
            {
                output.text = "<color=white>Enter a number and a character, then press enter:\nYour number is: " + saved + "\nYour character is: " + user_input.GetComponent<TMP_InputField>().text.TrimStart()[0];
                saved = null;
                user_input.GetComponent<TMP_InputField>().interactable = false;
                SoundManager.PlaySound("correct");
            }
        }

    }

    public void Q15_4()
    {
        user_input.GetComponent<TMP_InputField>().text = "";
        user_input_2.GetComponent<TMP_InputField>().text = "";
        output.text = "<color=white>Enter your name:";
        user_input_2.SetActive(false);
        user_input.SetActive(true);
        user_input.GetComponent<TMP_InputField>().interactable = true;
        user_flag = 1;
    }
    public void Q15_5()
    {
        user_input.GetComponent<TMP_InputField>().text = "";
        user_input_2.GetComponent<TMP_InputField>().text = "";
        output.text = "<color=white>Enter your name:";
        user_input.SetActive(false);
        user_input_2.SetActive(true);
        user_input_2.GetComponent<TMP_InputField>().interactable = true;
        user_flag = 1;
    }

    public void Q15_6()
    {
        string name = user_input.GetComponent<TMP_InputField>().text.Trim();
        name = Regex.Replace(name, @"\s+", " ").Trim();
        string[] names = name.Split(new char[] { ' ' }, 2);
        output.text = "<color=white>Enter your name:\n"+ "Hello "+names[0]+", how are you ?";
        user_input.GetComponent<TMP_InputField>().interactable = false;
        SoundManager.PlaySound("correct");
    }
    public void Q15_7()
    {
        string name = user_input_2.GetComponent<TMP_InputField>().text.Trim();
        output.text = "<color=white>Enter your name:\n" + "Hello " + name + ", how are you ?";
        user_input.GetComponent<TMP_InputField>().interactable = false;
        SoundManager.PlaySound("correct");
    }

    public void Q15_8()
    {
        if ((input == "%") && (input_b == "&")&&(input_c == "%d") && (input_d == "num"))
        {
            user_input.GetComponent<TMP_InputField>().text = "";
            output.text = "<color=white><color=white>Enter a number:";
            user_input.SetActive(true);
            user_input.GetComponent<TMP_InputField>().interactable = true;
            user_flag = 1;
            //Debug.Log("correct");
            answer.text = "<color=green><size=32>Correct!";
            SoundManager.PlaySound("correct");
            OnUnlockedButton?.Invoke("userinput", 1);
        }
        else
        {
            user_flag = 0;
            user_input.SetActive(false);
            user_input.GetComponent<TMP_InputField>().interactable = false;
            //Debug.Log("false");
            output.text = "<color=red><size=32>";
            answer.text = "<color=red><size=32>Wrong!";
            SoundManager.PlaySound("wrong");
        }
    }
    public void Q15_81()
    {
        if (user_input.GetComponent<TMP_InputField>().text != null)
        {
            //Debug.Log(user_input.GetComponent<TMP_InputField>().text);
            int num;
            if (int.TryParse(user_input.GetComponent<TMP_InputField>().text, out num))
            {
                output.text = "<color=white><color=white>Enter a number:\nYour number is: " + num;
                SoundManager.PlaySound("correct");
            }
            else
            {
                output.text = "<color=white><color=white>Enter a number:\n<color=red>Error!";
                SoundManager.PlaySound("wrong");
            }
        }
        else
        {
            //Debug.Log(user_input.GetComponent<TMP_InputField>().text);
            output.text = "<color=white><color=white>Enter a number:\n<color=red>Error!";
            SoundManager.PlaySound("wrong");
        }
        user_input.GetComponent<TMP_InputField>().interactable = false;
    }

    public void Q16_0()
    {
        //Debug.Log("true");
        int byteCount = 4;
        byte[] randomBytes = new byte[byteCount];
        System.Random rand = new System.Random();
        rand.NextBytes(randomBytes);
        string add = "0x7ff"+BitConverter.ToString(randomBytes).Replace("-", "").ToLower();
        output.text = "<color=white>My age is: 30\nMemory Address: "+add;
        SoundManager.PlaySound("correct");
    }
    public void Q16_1()
    {
        //Debug.Log("true");
        int byteCount = 4;
        byte[] randomBytes = new byte[byteCount];
        System.Random rand = new System.Random();
        rand.NextBytes(randomBytes);
        string add = "0x7ff" + BitConverter.ToString(randomBytes).Replace("-", "").ToLower();
        output.text = "<color=white>30\n"+ add+"\n"+add;
        SoundManager.PlaySound("correct");
    }
    public void Q16_2()
    {
        //Debug.Log("true");
        int byteCount = 4;
        byte[] randomBytes = new byte[byteCount];
        System.Random rand = new System.Random();
        rand.NextBytes(randomBytes);
        string add = "0x7ff" + BitConverter.ToString(randomBytes).Replace("-", "").ToLower();
        output.text = "<color=white>"+add + "\n30";
        SoundManager.PlaySound("correct");
    }

    public void Q16_3()
    {
        //Debug.Log("true");
        int byteCount = 4;
        byte[] randomBytes = new byte[byteCount];
        System.Random rand = new System.Random();
        rand.NextBytes(randomBytes);
        output.text = "<color=white>";
        string baseN = BitConverter.ToString(randomBytes).Replace("-", "").ToLower();
        long num = Convert.ToInt64(baseN, 16);
        for (int i = 0; i < 4; i++)
        {
            long finalnum = num + (i * 4);
            string final = "0x7ff" + finalnum.ToString("x");
            output.text += final+"\n";
        }
        SoundManager.PlaySound("correct");
    }

    public void Q16_4()
    {
        //Debug.Log("true");
        output.text = "<color=white>25";
        SoundManager.PlaySound("correct");
    }
    public void Q16_5()
    {
        //Debug.Log("true");
        output.text = "<color=white>50\n75";
        SoundManager.PlaySound("correct");
    }
    public void Q16_6()
    {
        //Debug.Log("true");
        int byteCount = 4;
        byte[] randomBytes = new byte[byteCount];
        System.Random rand = new System.Random();
        rand.NextBytes(randomBytes);
        string add = "0x7ff" + BitConverter.ToString(randomBytes).Replace("-", "").ToLower();
        output.text = "<color=white>" + add + "\n" + add;
        SoundManager.PlaySound("correct");
    }
    public void Q16_7()
    {
        //Debug.Log("true");
        output.text = "<color=white>25\n50\n75\n100";
        SoundManager.PlaySound("correct");
    }
    public void Q16_8()
    {
        //Debug.Log("true");
        output.text = "<color=white>500\n333";
        SoundManager.PlaySound("correct");
    }
    public void Q16_9()
    {
        if (((input == "*") && (input_b == "nums") && (input_c == "")) || ((input_c == " ") && (input_d == "nums") && (input_e == "&nums")) || ((input_c == null) && (input_d == "nums") && (input_e == "&nums")))
        {
            //Debug.Log("correct");
            answer.text = "<color=green><size=32>Correct!";
            SoundManager.PlaySound("correct");
            OnUnlockedButton?.Invoke("memorya", 1);
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red><size=32>Wrong!";
            SoundManager.PlaySound("wrong");
        }
    }
    public void Q6_0()
    {
        //Debug.Log("true");
        output.text = "<color=white>I'm inside a function!";
        SoundManager.PlaySound("correct");
    }
    public void Q6_1()
    {
        //Debug.Log("true");
        output.text = "<color=white>The result is: 6\nThis was printed in the main\nThe result is: 6";
        SoundManager.PlaySound("correct");
    }
    public void Q6_2()
    {
        //Debug.Log("true");
        output.text = "<color=white>Hello Alex\nHello Bob\nHello Moshe";
        SoundManager.PlaySound("correct");
    }
    public void Q6_3()
    {
        //Debug.Log("true");
        output.text = "<color=white>Hello Jenny, you are 52 years old.\nHello Bill, you are 24 years old.\nHello Biden, you are 37 years old.";
        SoundManager.PlaySound("correct");
    }
    public void Q6_4()
    {
        //Debug.Log("true");
        output.text = "<color=white>8\n13\n0\n33\n7";
        SoundManager.PlaySound("correct");
    }
    public void Q6_5()
    {
        //Debug.Log("true");
        output.text = "<color=white>The result is: 8";
        SoundManager.PlaySound("correct");
    }
    public void Q6_6()
    {
        //Debug.Log("true");
        output.text = "<color=white>Result 1 is: 8\nResult 2 is: 30";
        SoundManager.PlaySound("correct");
    }
    public void Q6_7()
    {
        //Debug.Log("true");
        output.text = "<color=white>5";
        SoundManager.PlaySound("correct");
    }
    public void Q6_8()
    {
        //Debug.Log("wrong");
        output.text = "<color=red>Error!";
        SoundManager.PlaySound("wrong");
    }
    public void Q6_9()
    {
        //Debug.Log("true");
        output.text = "<color=white>5\n5";
        SoundManager.PlaySound("correct");
    }
    public void Q6_10()
    {
        //Debug.Log("true");
        output.text = "<color=white>9\n5";
        SoundManager.PlaySound("correct");
    }
    public void Q6_11()
    {
        //Debug.Log("true");
        output.text = "<color=white>I'm a function!";
        SoundManager.PlaySound("correct");
    }
    public void Q6_12()
    {
        //Debug.Log("true");
        output.text = "<color=white>Result: 9";
        SoundManager.PlaySound("correct");
    }
    public void Q6_13()
    {
        //Debug.Log("true");
        output.text = "<color=white>First function!\nSecond function!";
        SoundManager.PlaySound("correct");
    }
    public void Q6_14()
    {
        //Debug.Log("true");
        output.text = "<color=white>4.000000\n5.000000";
        SoundManager.PlaySound("correct");
    }
    public void Q6_15()
    {
        //Debug.Log("true");
        output.text = "<color=white>4.000000\n3.000000";
        SoundManager.PlaySound("correct");
    }
    public void Q6_16()
    {
        //Debug.Log("true");
        output.text = "<color=white>125.000000\n256.000000";
        SoundManager.PlaySound("correct");
    }
    public void Q6_17()
    {
        if ((input == "int") && (input_b == "multiply") && (input_c == "int") && ((input_d == "a*b")||(input_d == "a * b")||(input_d == "b*a")||(input_d == "b * a")))
        {
            //Debug.Log("correct");
            answer.text = "<color=green><size=32>Correct!";
            SoundManager.PlaySound("correct");
            OnUnlockedButton?.Invoke("functions", 0);
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red><size=32>Wrong!";
            SoundManager.PlaySound("wrong");
        }
    }
    public void Q7_0()
    {
        //Debug.Log("true");
        output.text = "<color=white>55";
        SoundManager.PlaySound("correct");
    }
    public void Q7_1()
    {
        if (((input == "x==0") || (input == "x == 0") || (input == "x < 1") || (input == "x<1")) && ((input_b == "x-1") || (input_b == "x - 1")))
        {
            //Debug.Log("correct");
            answer.text = "<color=green><size=32>Correct!";
            SoundManager.PlaySound("correct");
            OnUnlockedButton?.Invoke("recursion", 0);
        }
        else
        {
            //Debug.Log("false");
            answer.text = "<color=red><size=32>Wrong!";
            SoundManager.PlaySound("wrong");
        }
    }

    public void Q8_0()
    {
        //Debug.Log("correct");
        output.text = "<color=white><size=32>My number: 5\nMy letter: a";
        SoundManager.PlaySound("correct");
    }
    public void Q8_1()
    {
        //Debug.Log("correct");
        output.text = "<color=white><size=32>My string: Cool Text";
        SoundManager.PlaySound("correct");
    }
    public void Q8_2()
    {
        //Debug.Log("correct");
        output.text = "<color=white><size=32>5\na\nCool Text";
        SoundManager.PlaySound("correct");
    }
    public void Q9_1()
    {
        //Debug.Log("correct");
        output.text = "<color=white><size=32>1";
        SoundManager.PlaySound("correct");
    }
    public void Q17_1()
    {
        //Debug.Log("correct");
        output.text = "<color=white><size=32>Hello diary...";
        SoundManager.PlaySound("correct");
    }
    public void Q17_2()
    {
        //Debug.Log("correct");
        output.text = "<color=white><size=32>Hello diary...\nToday was sunny.";
        SoundManager.PlaySound("correct");
    }

    public void Q17_3(int ans)
    {
        switch (ans)
        {
            case 1:
                //Debug.Log("false");
                answer.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 2:
                //Debug.Log("false");
                answer.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 3:
                //Debug.Log("correct");
                answer.text = "<color=green>Correct!";
                SoundManager.PlaySound("correct");
                OnUnlockedMultiple?.Invoke("files", 0, 0);
                break;
            case 4:
                //Debug.Log("false");
                answer.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
        }
    }
    public void Q17_4(int ans)
    {
        switch (ans)
        {
            case 1:
                //Debug.Log("false");
                a2.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 2:
                //Debug.Log("false");
                a2.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 3:
                //Debug.Log("false");
                a2.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 4:
                //Debug.Log("correct");
                a2.text = "<color=green>Correct!";
                SoundManager.PlaySound("correct");
                OnUnlockedMultiple?.Invoke("files", 0, 1);
                break;
        }
    }
    public void Q18_1()
    {
        //Debug.Log("correct");
        output.text = "<color=white><size=32>4\n4\n8\n1";
        SoundManager.PlaySound("correct");
    }
    public void Q18_2()
    {
        //Debug.Log("correct");
        output.text = "<color=white><size=32>200";
        SoundManager.PlaySound("correct");
    }
    public void Q18_3()
    {
        //Debug.Log("correct");
        output.text = "<color=white><size=32>140";
        SoundManager.PlaySound("correct");
    }
    public void Q18_4()
    {
        //Debug.Log("correct");
        output.text = "<color=white><size=32>2 4 0";
        SoundManager.PlaySound("correct");
    }
    public void Q18_5()
    {
        //Debug.Log("correct");
        output.text = "<color=white><size=32>6 8 0";
        SoundManager.PlaySound("correct");
    }
    public void Q18_6()
    {
        //Debug.Log("correct");
        output.text = "<color=white><size=32>1684234849 = (a, b, c, d)";
        SoundManager.PlaySound("correct");
    }
    public void Q18_7()
    {
        System.Random rand = new System.Random();
        int x = rand.Next(0x10000, 0xFFFFF);
        string hex = x.ToString("X5").ToLower();
        //Debug.Log("correct");
        output.text = "<color=white><size=32>16 bytes allocated at address 0x"+ hex + "2a0\n24 bytes allocated at address 0x"+ hex +"2a0";
        SoundManager.PlaySound("correct");
    }
    public void Q18_8()
    {
        System.Random rand = new System.Random();
        int x = rand.Next(0x10000, 0xFFFFF);
        string hex = x.ToString("X5").ToLower();
        //Debug.Log("correct");
        output.text = "<color=white><size=32>8 bytes allocated at address 0x" + hex + "2a0";
        SoundManager.PlaySound("correct");
    }
    public void Q18_9(int option)
    {
        switch (option)
        {
            case 1:
                //Debug.Log("false");
                answer.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 2:
                //Debug.Log("false");
                answer.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 3:
                //Debug.Log("false");
                answer.text = "<color=red>Wrong!";
                SoundManager.PlaySound("wrong");
                break;
            case 4:
                //Debug.Log("true");
                answer.text = "<color=green>Correct!";
                SoundManager.PlaySound("correct");
                OnUnlockedButton?.Invoke("memorym", 0);
                break;

            default:
                break;
        }
    }
}

