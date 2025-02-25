using UnityEngine;

public class mover : MonoBehaviour
{
    public GameObject back,easy,medium,hard, play;
    private int flag;
    public void move()
    {
        Vector3 currentPosition = back.transform.localPosition;
        back.transform.localPosition = new Vector3(currentPosition.x, -244f, currentPosition.z);
        flag = 1;
    }

    public void remove()
    {
        Vector3 currentPosition = back.transform.position;
        back.transform.localPosition = new Vector3(0, -128f, currentPosition.z);
        easy.SetActive(false);
        medium.SetActive(false);
        hard.SetActive(false);
        flag = 0;
    }
    public void removeoneback()
    {
        if (flag == 1)
        {
            remove();
            play.SetActive(true);
        }
    }
}
