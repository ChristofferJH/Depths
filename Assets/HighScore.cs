using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighScore : MonoBehaviour
{
    void Start()
    {
        float hs = PlayerPrefs.GetFloat("highscore", 0f);
        GetComponent<TMP_Text>().text = "Highscore depth: " + Mathf.FloorToInt(hs).ToString();
    }


}
