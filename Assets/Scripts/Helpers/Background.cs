using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    //background images
    public static Sprite A, B, C, C2, C3, D, E, F;
    public Image background;
    static Image myBg;

    // Start is called before the first frame update
    void Start()
    {
        A = Resources.Load<Sprite>("Backgrounds/A");
        B = Resources.Load<Sprite>("Backgrounds/B");
        C = Resources.Load<Sprite>("Backgrounds/C");
        D = Resources.Load<Sprite>("Backgrounds/D");

        //default need to change it to be saved before exit
        background.sprite = A;
        myBg= background;
        ////Debug.Log("initiated");
        if (PlayerPrefs.HasKey("background"))
        {
            LoadBackground();
        }
        else
        {
            PlayerPrefs.SetFloat("background", 1);
            LoadBackground();
        }
    }
    public void LoadBackground()
    {
        switch (PlayerPrefs.GetFloat("background"))
        {
            case 1:
                myBg.sprite = A;
                break;
            case 2:
                myBg.sprite = B;
                break;
            case 3:
                myBg.sprite = C;
                break;
            case 4:
                myBg.sprite = D;
                break;
            default:
                myBg.sprite = A;
                break;
        }
    }

    public void changeBackground(int bg)
    {
        switch (bg)
        {
            case 1:
                myBg.sprite = A;
                PlayerPrefs.SetFloat("background", 1);
                break;
            case 2:
                myBg.sprite = B;
                PlayerPrefs.SetFloat("background", 2);
                break;
            case 3:
                myBg.sprite = C;
                PlayerPrefs.SetFloat("background", 3);
                break;
            case 4:
                myBg.sprite = D;
                PlayerPrefs.SetFloat("background", 4);
                break;
            default:
                myBg.sprite = A;
                PlayerPrefs.SetFloat("background", 1);
                break;
        }
    }
}
