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
        image=GetComponent<Image>();
        SetDayOrNight(isNight);
    }
    public void SetDayOrNight(bool value)
    {
        isNight = value;
        image.sprite = (isNight) ? night : day;
    }
}

