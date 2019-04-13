using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    public TextMeshPro m_Text;
    public string score = "Score:";
    private string temp;
    private int nextUpdate = 1;
    private int scorenum = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_Text.text = score;
    }

    // Update is called once per frame
    void Update()
    {
        temp = gameObject.GetComponent<DefaultTrackableEventHandler>().nameCheker;
        if (temp == "classmate")
        {
            if (Time.time >= nextUpdate)
            {
                // Change the next update (current second+1)
                nextUpdate = Mathf.FloorToInt(Time.time) + 1;
                // Call your fonction
                UpdateEverySecond();
            }
            m_Text.text = score;
        }

    }

    void UpdateEverySecond()
    {
        scorenum++;
        score = scorenum.ToString();
    }
}
