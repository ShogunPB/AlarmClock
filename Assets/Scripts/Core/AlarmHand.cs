using UnityEngine;
using UnityEngine.EventSystems;



public class AlarmHand : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField]
    float secondCircle, deltaAngle;
    [SerializeField]
    public BoolUnityEvent DayNightChanged;
    [HideInInspector]
    public FloatUnityEvent AngleChanged;

    bool IsPressed;
    bool isNight;
    PointerEventData pointerEventData;
    Vector2 pivotPoint;
    float prevAngle, angle;
    int circleNum;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsPressed) return;
        IsPressed = true;
        pointerEventData = eventData;
        pivotPoint = GameObject.Find("PanelAlarm").GetComponent<RectTransform>().position;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
    }

    private void Start()
    {
        isNight = true;
        Data.alarm.ChangeSecond.AddListener(SetAngle);
    }
    private void Update()
    {
        if (IsPressed)
        {
            Vector2 _direction = pointerEventData.position - pivotPoint;
            
            float _angle=Vector2.SignedAngle(transform.TransformDirection(transform.up), _direction);
            if (Mathf.Abs(prevAngle - _angle) > 1f)
            {
                transform.localEulerAngles = new Vector3(0, 0, -_angle);
                if ((prevAngle *_angle <0) && ((Mathf.Abs(prevAngle) + Mathf.Abs(_angle))<180))
                { 
                    circleNum += 1; circleNum = Mathf.Abs(circleNum % 2);
                }
                angle = _angle+secondCircle * circleNum;
                isNight= ((angle>0)&&(angle<360)) ? false : true;
                AngleChanged?.Invoke(angle);
                DayNightChanged?.Invoke(isNight);
                prevAngle = _angle;
            }                    
        }
    }
    private void OnDestroy()
    {
        Data.alarm.ChangeSecond.RemoveListener(SetAngle);
    }

    private void SetAngle(int value)
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, value * deltaAngle);
        if (secondCircle>0)
        {
            isNight=Util.CheckDayNight(value);
            DayNightChanged?.Invoke(isNight);
        }
    }

    
}
