using TMPro;
using UnityEngine;
using UnityEngine.UIElements.InputSystem;

public class Cleaner : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] input;
    [SerializeField] private TMP_InputField[] output;
    [SerializeField] private TextMeshProUGUI[] answer;
    [SerializeField] private GameObject[] solution;
    [SerializeField] private TMP_InputField special = null;
    [SerializeField] private string container = null;

    private void Start()
    {
        LessonMenu.OnCleaner += Clean;
    }
    public void Clean(string id)
    {
        if (id == container)
        {
            foreach (TMP_InputField x in input)
            {
                x.text = "";
            }
            foreach (TMP_InputField y in output)
            {
                y.text = "";
            }
            foreach (TextMeshProUGUI z in answer)
            {
                z.text = "";
            }
            foreach (GameObject s in solution)
            {
                s.SetActive(false);
            }
            switch (id)
            {
                case "output":
                    special.text = "printf(Hello World!);";
                    break;
                case "switch":
                    special.text = "<color=white>Monday";
                    break;
                case "loops":
                    special.text = "<color=white>5\n4\n3\n2\n1";
                    break;
                case "memorya":
                    special.text = "<color=white>4\r\n0x7ffe866ea50c\r\n0x7ffe866ea50c";
                    break;
                case "functions":
                    special.text = "<color=white>3 * 4 = 12";
                    break;
                case "recursion":
                    special.text = "<color=white>5! = 120";
                    break;
                default:
                    break;
            }
            //Debug.Log("cleaned");
        }
    }
    private void OnDestroy()
    {
        LessonMenu.OnCleaner -= Clean;
    }
}
