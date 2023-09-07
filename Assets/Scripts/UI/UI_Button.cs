using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class UI_Button : MonoBehaviour
{
    private Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, Object[]>();
    
    enum Buttons {
        PointButton
    }

    enum Texts
    {
        PointText,
        ScoreText,
    }

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        
        Get<TextMeshProUGUI>((int)Texts.ScoreText).text = "Bind test";
    }

    void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);  //{PointText, ScoreText}
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);
        
        for (int i = 0; i < names.Length; i++)
        {
            Debug.Log($"name : {names[i]}");
            objects[i] = Util.FindChild<T>(gameObject, names[i], true);
        }
    }

    T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    private int _score = 0;
    
    public void OnButtonClicked()
    {
        _score++;
        //_text.text = $"점수 : {_score}";
    }
}
