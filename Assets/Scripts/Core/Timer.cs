
using UnityEngine;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{   
    public int CurrentTickNumber;
    public float CurrentTickStep;
    public int AlarmTime;
    public int NextUploadTime;
    public BoolUnityEvent ChangeDayNight=new BoolUnityEvent();
    //[HideInInspector]
    //public UnityEvent Tick = new UnityEvent ();
    [HideInInspector]
    public IntUnityEvent ChangeSecond = new IntUnityEvent();
    [HideInInspector]
    public IntUnityEvent TimerOn = new IntUnityEvent();

    [SerializeField]
    GameObject alarmPref, alarmSpawn;

    bool IsInitialized;
    WebData uploader;
    GameObject alarmGO;
    AudioSource audioSource;

    public void Init(int initSecondNumber, float initMillisecond, WebData webData)
    {
        audioSource = GetComponent<AudioSource>();
        uploader = webData;
        CurrentTickNumber = initSecondNumber;
        CurrentTickStep = initMillisecond;
        IsInitialized = true;
        if (TimerOn != null) TimerOn.Invoke(CurrentTickNumber);
    }

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
            audioSource.Play();
        }
    }

    private void ApplyTick()
    {
        CurrentTickNumber++;
        if (CurrentTickNumber > 86399) CurrentTickNumber = 0;
        if (ChangeSecond != null) ChangeSecond.Invoke(CurrentTickNumber);
        if (CurrentTickNumber == AlarmTime)
            if(GameObject.Find("Alarm")==null)
            {
                alarmGO = Instantiate(alarmPref, alarmSpawn.transform);
                alarmGO.name = "Alarm";
            }                
        if (CurrentTickNumber == NextUploadTime) uploader.Upload();
        ChangeDayNight?.Invoke(Util.CheckDayNight(CurrentTickNumber));
    }
    
    
}
