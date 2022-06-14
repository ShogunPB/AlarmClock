using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class WebData : MonoBehaviour
{
    [SerializeField] GameObject status_1;
    [SerializeField] GameObject status_2;
    int initSecondNumber;
    float initMillisecond;
    int nextUploadTime;
    TextMeshProUGUI stringStatus_1;
    TextMeshProUGUI stringStatus_2;

    void Start()
    {
        stringStatus_1=status_1.GetComponent<TextMeshProUGUI>();
        stringStatus_2=status_2.GetComponent<TextMeshProUGUI>();
        StartCoroutine(LoadWebData(Data.IP_REQUEST, IpHandler));
    }

    private void IpHandler(string text)
    {
        if (text == null)
        {
            NetError();
            return;
        }
        Data.IP = text.Split(',')[0].Split(':')[1].Trim('"');
        StartCoroutine(LoadWebData(Data.MAIN_TIME_REQUEST + Data.IP, TimeOneHandler));
    }

    private void TimeOneHandler(string text)
    {
        if (text == null)
        {
            StartCoroutine(LoadWebData(Data.RESERV_TIME_REQUEST + Data.IP, TimeTwoHandler));
            return;
        }
        TimeapiSchema _timeFromNet = JsonUtility.FromJson<TimeapiSchema>(text);
        initSecondNumber = Util.SecondsSinceMidnight(_timeFromNet.hour, _timeFromNet.minute, _timeFromNet.seconds);
        initMillisecond = _timeFromNet.milliSeconds / 1000f;
        nextUploadTime = (initSecondNumber + Data.UPLOAD_TIME > 86399) ? initSecondNumber + Data.UPLOAD_TIME - 86400 : initSecondNumber + Data.UPLOAD_TIME;
        stringStatus_1.text = "The time is updated from "+ Data.MAIN_TIME_REQUEST.Split('/')[2];
        Vector3Int _nextUploadTime = Util.SecToTime(nextUploadTime);
        Data.timer.NextUploadTime = nextUploadTime;
        stringStatus_2.text = $"Next update in {_nextUploadTime.x.ToString("D2")}:{_nextUploadTime.y.ToString("D2")}:{_nextUploadTime.z.ToString("D2")}.";
        Data.timer.Init(initSecondNumber, initMillisecond,this);
    }

    internal void Upload()
    {
        StartCoroutine(LoadWebData(Data.IP_REQUEST, IpHandler));
    }

    private void TimeTwoHandler(string text)
    {
        if (text == null)
        {
            NetError();
            return;
        }
        WorldTimeSchema _timeFromNet = JsonUtility.FromJson<WorldTimeSchema>(text);
        initSecondNumber = Util.SecondsSinceMidnight(_timeFromNet.datetime.Split('T')[1].Split('+')[0].Split('.')[0]);
        int _parseRes;
        if(int.TryParse(_timeFromNet.datetime.Split('T')[1].Split('+')[0].Split('.')[1],out _parseRes))
        {
            initMillisecond = _parseRes/1000f;
        }
        else
        {
            Debug.Log("parse error");
        }
        nextUploadTime = (initSecondNumber + Data.UPLOAD_TIME > 86399) ? initSecondNumber + Data.UPLOAD_TIME - 86400 : initSecondNumber + Data.UPLOAD_TIME;
        stringStatus_1.text = "The time is updated from " + Data.RESERV_TIME_REQUEST.Split('/')[2];
        Vector3Int _nextUploadTime = Util.SecToTime(nextUploadTime);
        Data.timer.NextUploadTime = nextUploadTime;
        stringStatus_2.text = $"Next update in {_nextUploadTime.x.ToString("D2")}:{_nextUploadTime.y.ToString("D2")}:{_nextUploadTime.z.ToString("D2")}.";
        Data.timer.Init(initSecondNumber, initMillisecond, this);
    }

    IEnumerator LoadWebData(string url, Action<string> handler)
    {
        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result!=UnityWebRequest.Result.ProtocolError && request.result != UnityWebRequest.Result.ConnectionError)
            {
                handler(request.downloadHandler.text);
            }
            else
            {
                Debug.Log($"error request {url}, {request.error}");

                handler(null);
            }
        }
    }

    private void TimeFromSystem() 
    {
        initSecondNumber = Util.SecondsSinceMidnight(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        initMillisecond = DateTime.Now.Millisecond/1000f;
        nextUploadTime = (initSecondNumber + 3600 > 86399) ? initSecondNumber + 3600 - 86400 : initSecondNumber + 3600;
    }

    private void NetError()
    {
        TimeFromSystem();
        stringStatus_1.text = "The time is updated from the System. Please check the connection.";
        Vector3Int _nextUploadTime = Util.SecToTime(nextUploadTime);
        stringStatus_2.text = $"Next update in {_nextUploadTime.x}:{_nextUploadTime.y}:{_nextUploadTime.z}.";
        Data.timer.Init(initSecondNumber, initSecondNumber,this);
    }
}
