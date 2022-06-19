using UnityEngine;

public class ArrowRotate : MonoBehaviour
{
    [SerializeField]
    float deltaAngle;


    private void Start()
    {
        Data.timer.ChangeSecond.AddListener(SetArrowAngle);
        Data.timer.TimerOn.AddListener(SetArrowAngle);
    }

    private void SetArrowAngle(int time)
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, time * deltaAngle);
    }

    private void OnDestroy()
    {
        Data.timer.ChangeSecond.RemoveListener(SetArrowAngle);
        Data.timer.TimerOn.RemoveListener(SetArrowAngle);
    }

}
