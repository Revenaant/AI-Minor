using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Color passive = Color.white;
    public Color active = Color.green;

    private Image _image;
    private Button _button;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(onActive);
    }

    public void onActive()
    {
        _image.color = active;
    }

    public void onPassive()
    {
        _image.color = passive;
    }
}
