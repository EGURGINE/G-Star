using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private GameObject character => GameManager.Instance.player.gameObject;
    private float spd => GameManager.Instance.player.playerSpd;
    private int isTouch = 0;

    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 joystickVector;

    private void Update()
    {
        character.GetComponent<Rigidbody2D>().velocity = character.transform.up * spd * isTouch;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.isTutorial && GameManager.Instance.tutorialNum == 0) GameManager.Instance.Tutorial();

        endPos = eventData.position;

        joystickVector = endPos - startPos;

        // Character에게 조이스틱 방향 넘기기
        TurnAngle(joystickVector);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = 1;
        startPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickVector = Vector2.zero;
        isTouch = 0;
    }

    private void TurnAngle(Vector3 currentJoystickVec)
    {
        Vector3 originJoystickVec = character.transform.up;
        // character가 바라보;고 있는 벡터

        float angle = Vector3.Angle(currentJoystickVec, originJoystickVec);
        int sign = (Vector3.Cross(currentJoystickVec, originJoystickVec).z > 0) ? -1 : 1;
        // angle: 현재 바라보고 있는 벡터와, 조이스틱 방향 벡터 사이의 각도
        // sign: character가 바라보는 방향 기준으로, 왼쪽:+ 오른쪽:-

        character.transform.Rotate(0, 0, sign * angle);

    }
}
