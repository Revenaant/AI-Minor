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

    public void SetStatus(BNodeStatus status)
    {
        switch(status)
        {
            case BNodeStatus.Running: SetColor(Color.yellow); break;
            case BNodeStatus.Success: SetColor(Color.green); break;
            case BNodeStatus.Failure: SetColor(Color.red); break;
        }
    }

    public void SetColor(Color color)
    {
        _image.color = color;
    }
}
