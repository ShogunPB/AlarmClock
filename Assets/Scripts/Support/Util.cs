using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntUnityEvent : UnityEvent<int>
{
}
[System.Serializable]
public class BoolUnityEvent : UnityEvent<bool>
{
}
[System.Serializable]
public class FloatUnityEvent : UnityEvent<float>
{
}

public static class Util 
{
    public static Vector3Int SecToTime(int sec)
    {
        return new Vector3Int(sec / 3600, (sec / 60) % 60, sec % 60);
    }

    public static int SecondsSinceMidnight(int hour, int minute, int second)
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

    public static bool CheckDayNight(int time)
    {
        return ((time > 21600) && (time < 64800)) ? false : true;
    }

}
