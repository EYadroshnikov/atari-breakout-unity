using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishText : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    void Start()
    {
        this.text.text = GameManager.Instance.score.ToString();
    }
}
