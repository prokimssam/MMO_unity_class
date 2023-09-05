using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Button : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private int _score = 0;
    
    public void OnButtonClicked()
    {
        _score++;
        _text.text = $"점수 : {_score}";

    }
}
