using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data 
{
    public const string IP_REQUEST= "https://ipv4.jsonip.com/";
    public const string MAIN_TIME_REQUEST = "https://timeapi.io/api/Time/current/ip?ipAddress=";
    public const string RESERV_TIME_REQUEST = "https://worldtimeapi.org/api/ip/";
    public const int UPLOAD_TIME = 60;

    public static float TimeScale = Time.fixedDeltaTime;
    public static Timer timer;
    public static AlarmMain alarm;
    public static string IP;
   

    public static Vector3Int SecToTime(int sec)
    {
        return new Vector3Int(sec / 3600, (sec / 60) % 60, sec % 60);
    }

    public static int SecondsSinceMidnight(int hour,int minute,int second)
    {
        return hour * 3600 + minute * 60 + second;
    }

    public static int SecondsSinceMidnight(Vector3Int time)
    {
        return time[0] * 3600 + time[1] * 60 + time[2];
    }

    public static int SecondsSinceMidnight(string timeString)
    {
        string[] _timeString = timeString.Split(':');
        int[] _timeInt = new int[_timeString.Length];
        for (int i = 0; i < _timeString.Length; i++)
        {
            if (!int.TryParse(_timeString[i], out _timeInt[i])) 
            { 
                Debug.Log("parse error");
            }           
        }
        return _timeInt[0] * 3600 + _timeInt[1] * 60 + _timeInt[2];
    }
}
