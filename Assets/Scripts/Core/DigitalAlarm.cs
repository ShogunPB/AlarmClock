using TMPro;
using UnityEngine;

public class DigitalAlarm : MonoBehaviour
{
    [SerializeField] GameObject Hour, Minute, Second;
    TextMeshProUGUI hourValue;
    TextMeshProUGUI minuteValue;
    TextMeshProUGUI secondValue;


    void Start()
    {
        hourValue = Hour.GetComponent<TextMeshProUGUI>();
        minuteValue = Minute.GetComponent<TextMeshProUGUI>();
        secondValue = Second.GetComponent<TextMeshProUGUI>();
        Data.alarm.ChangeSecond.AddListener(SetAlarmDigitalClock);
    }

    private void SetAlarmDigitalClock(int sec)
    {
        Vector3Int _time = Util.SecToTime(sec);

        hourValue.text = _time[0].ToString("D2");
        minuteValue.text = _time[1].ToString("D2");
        secondValue.text = _time[2].ToString("D2");
    }

    private void OnDestroy()
    {
        Data.timer.ChangeSecond.RemoveListener(SetAlarmDigitalClock);
    }
}
