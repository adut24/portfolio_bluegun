using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI textbox;
    // Start is called before the first frame update
    void Start()
    {
        textbox.text = "Score: " + Random.Range(5000, 9999);
    }
}
