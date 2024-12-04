using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    public Text achievementText;

    public int maxNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        achievementText.text = AchievementSystem.achievementCount.ToString() + "/" + maxNum.ToString();
    }
}
