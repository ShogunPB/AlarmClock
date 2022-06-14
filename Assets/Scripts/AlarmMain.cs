using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmMain : MonoBehaviour
{
    public IntUnityEvent ChangeSecond = new IntUnityEvent();

    [SerializeField] GameObject alarmHourHand, alarmMinuteHand, alarmSecondHand,digitalAlarm,dayNight;
    float angleHour, angleMinute, angleSecond;
   

    public void ButtonChangeValue(int value)
    {
        int _alarmTime = Data.timer.AlarmTime;
        _alarmTime += value;
        if (_alarmTime > 86359) _alarmTime -= 86400;
        if (_alarmTime < 0) _alarmTime += 86400;
        ChangeSecond?.Invoke(_alarmTime);
        Data.timer.AlarmTime =_alarmTime;
    }

    void Awake()
    {
        Data.alarm = this;
    }
    void Start()
    {
        alarmHourHand.GetComponent<AlarmHand>().AngleChanged.AddListener(HourAngleChange);
        alarmMinuteHand.GetComponent<AlarmHand>().AngleChanged.AddListener(MinuteAngleChange);
        alarmSecondHand.GetComponent<AlarmHand>().AngleChanged.AddListener(SecondAngleChange);
    }

    private void SecondAngleChange(float arg0)
    {
        float _angleReal = (arg0 < 0) ? Mathf.Abs(arg0) : 360 - arg0;
        angleSecond = _angleReal;
        Data.timer.AlarmTime = SetAlarmTimeByAngle(angleHour, angleMinute, angleSecond);
    }
    private void MinuteAngleChange(float arg0)
    {
        float _angleReal = (arg0 < 0) ? Mathf.Abs(arg0) : 360 - arg0;
        angleMinute = _angleReal;
        Data.timer.AlarmTime = SetAlarmTimeByAngle(angleHour, angleMinute, angleSecond);
    }
    private void HourAngleChange(float arg0)
    {
        float _abs = Mathf.Abs(arg0); float _angleReal;
        if (_abs < 180)
        {
            _angleReal = (arg0 < 0) ? _abs : 360 - arg0;
        }
        else
        {
            _angleReal = (arg0 < 360) ? 720 - arg0 : 1080 - arg0;
        }
        angleHour = _angleReal;
        Data.timer.AlarmTime = SetAlarmTimeByAngle(angleHour, angleMinute, angleSecond);
    }
    private int SetAlarmTimeByAngle(float angleHour, float angleMinute, float angleSecond)
    {
        int _hour = (int) angleHour / 30;
        int _minute = (int) angleMinute / 6;
        int _second = (int) angleSecond / 6;
        int _alarmTime=Data.SecondsSinceMidnight(_hour,_minute, _second);
        ChangeSecond?.Invoke(_alarmTime);
        return _alarmTime;
    }



}
