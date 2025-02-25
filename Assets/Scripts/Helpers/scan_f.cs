using UnityEngine;
using UnityEngine.UI;
public enum alphaValue
{
    SHRINKING,
    GROWING,
}
public class scan_f : MonoBehaviour
{
    public alphaValue currentAlphaValue;
    public float CommentminAlpha;
    public float CommentmaxAlpha;
    public float CommentCurrentAlpha;
    public Outline myOutline;


    void Start()
    {
        CommentminAlpha = 0.1f;
        CommentmaxAlpha = 0.6f;
        CommentCurrentAlpha = 1.0f;
        currentAlphaValue = alphaValue.SHRINKING;
    }

    void Update()
    {
        if (myOutline != null)
        {
            alphaComments();
        }
    }

    public void turnOff()
    {
        myOutline.enabled = false;

    }

    public void turnOn()
    {
        myOutline.enabled = true;

    }
    public void alphaComments()
    {
        if (currentAlphaValue == alphaValue.SHRINKING)
        {
            CommentCurrentAlpha = CommentCurrentAlpha - 0.002f;
            myOutline.effectColor = new Color(0, 255, 0, CommentCurrentAlpha);
            if (CommentCurrentAlpha <= CommentminAlpha)
            {
                currentAlphaValue = alphaValue.GROWING;
            }
        }
        else if (currentAlphaValue == alphaValue.GROWING)
        {
            CommentCurrentAlpha = CommentCurrentAlpha + 0.0015f;
            myOutline.effectColor = new Color(0, 255, 0, CommentCurrentAlpha);
            if (CommentCurrentAlpha >= CommentmaxAlpha)
            {
                currentAlphaValue = alphaValue.SHRINKING;
            }
        }
    }

}
