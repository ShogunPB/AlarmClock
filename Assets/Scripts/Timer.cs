using System;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class IntUnityEvent : UnityEvent<int>
{
}

public class Timer : MonoBehaviour
{
    [SerializeField]
    GameObject alarmPref, alarmSpawn;
   
    public int CurrentTickNumber;
    public float CurrentTickStep;
    public int AlarmTime;
    public int NextUploadTime;
    public UnityEvent Tick = new UnityEvent ();
    public IntUnityEvent ChangeSecond = new IntUnityEvent();
    public IntUnityEvent TimerOn = new IntUnityEvent();

    bool IsInitialized;
    WebData uploader;

    void Awake()
    {
        Data.timer = this;
    }
    void FixedUpdate()
    {
        if (!IsInitialized) return;
        CurrentTickStep += Data.TimeScale;
        if (CurrentTickStep > 1)
        {
            CurrentTickStep = 0;
             ApplyTick();
        }
        if (Tick != null) Tick.Invoke();
    }

    private void ApplyTick()
    {
        CurrentTickNumber++;
        if (CurrentTickNumber > 86399) CurrentTickNumber = 0;
        if (ChangeSecond != null) ChangeSecond.Invoke(CurrentTickNumber);
        if (CurrentTickNumber == AlarmTime) Instantiate(alarmPref, alarmSpawn.transform);
        if (CurrentTickNumber == NextUploadTime) uploader.Upload();
        
    }
    public void Init(int initSecondNumber, float initMillisecond, WebData webData)
    {
        uploader = webData;
        CurrentTickNumber = initSecondNumber;
        CurrentTickStep = initMillisecond;
        IsInitialized = true;
        if (TimerOn != null) TimerOn.Invoke(CurrentTickNumber);
    }
}
