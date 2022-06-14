using UnityEngine;

public static class Data 
{
    public const string IP_REQUEST= "https://ipv4.jsonip.com/";
    public const string MAIN_TIME_REQUEST = "https://timeapi.io/api/Time/current/ip?ipAddress=";
    public const string RESERV_TIME_REQUEST = "https://worldtimeapi.org/api/ip/";
    public const int UPLOAD_TIME = 3600;

    public static float TimeScale = Time.fixedDeltaTime;
    public static Timer timer;
    public static AlarmMain alarm;
    public static string IP;
}
