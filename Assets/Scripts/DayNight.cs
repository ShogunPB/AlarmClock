using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNight : MonoBehaviour
{
    [SerializeField]
    Sprite day, night;
    Image image;
    public bool isNight;
    private void Start()
    {
        isNight = true;
        image=GetComponent<Image>();
        transform.Find("HourHand").GetComponent<AlarmHand>().DayNightChanged.AddListener(SetDayOrNight);
        SetDayOrNight(isNight);
    }
    private void SetDayOrNight(bool value)
    {
        isNight = value;
        image.sprite = (isNight) ? night : day;
    }
    private void OnDestroy()
    {
        transform.Find("HourHand").GetComponent<AlarmHand>().DayNightChanged.RemoveListener(SetDayOrNight);
    }
}

