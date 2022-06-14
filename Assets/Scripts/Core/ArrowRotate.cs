using UnityEngine;

public class ArrowRotate : MonoBehaviour
{
    [SerializeField]
    float deltaAngle;
    float timeScale;


    private void Start()
    {
        timeScale = Data.TimeScale;
        Data.timer.Tick.AddListener(RotateArrow);
        Data.timer.TimerOn.AddListener(SetArrowAngle);
    }

    private void SetArrowAngle(int time)
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, time * deltaAngle);
    }

    private void RotateArrow()
    {
        transform.localRotation *= Quaternion.Euler(0f, 0f, timeScale * deltaAngle);
    }
    private void OnDestroy()
    {
        Data.timer.Tick.RemoveListener(RotateArrow);
    }

}
