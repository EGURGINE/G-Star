using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    GameObject character => GameManager.Instance.player.gameObject;
    RectTransform touchArea => this.GetComponent<RectTransform>();
    int isTouch = 0;
    Vector2 joystickVector;
    private Coroutine runningCoroutine;
    private float rotateSpeed = 1000f;
    private bool isDragging;
    RectTransform panelPos => this.GetComponent<RectTransform>();

    private void Start()
    {
        isDragging = true;
    }
    private void FixedUpdate()
    {
        character.GetComponent<Rigidbody2D>().velocity = character.transform.up * GameManager.Instance.player.playerSpd * isTouch;
    }
    private void Update()
    {
        if (isDragging)
        {
            panelPos.anchoredPosition = Input.mousePosition;

        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(touchArea,
            eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
        {
            joystickVector = new Vector2(localPoint.x * 2.6f, localPoint.y * 2f);
            // 조이스틱 벡터 조절 (2.6과 2를 곱해준 것은 TouchArea의 비율 때문임)

            TurnAngle(joystickVector);
            // Character에게 조이스틱 방향 넘기기


        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = 1;
        isDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = 0;
        isDragging = true;

    }

    private void TurnAngle(Vector3 currentJoystickVec)
    {
        Vector3 originJoystickVec = character.transform.up;
        // character가 바라보고 있는 벡터

        float angle = Vector3.Angle(currentJoystickVec, originJoystickVec);
        int sign = (Vector3.Cross(currentJoystickVec, originJoystickVec).z > 0) ? -1 : 1;
        // angle: 현재 바라보고 있는 벡터와, 조이스틱 방향 벡터 사이의 각도
        // sign: character가 바라보는 방향 기준으로, 왼쪽:+ 오른쪽:-

        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartCoroutine(RotateAngle(angle, sign));
        // 코루틴이 실행중이면 실행 중인 코루틴 중단 후 코루틴 실행 
        // 코루틴이 한 개만 존재하도록.
        // => 회전 중에 새로운 회전이 들어왔을 경우, 회전 중이던 것을 멈추고 새로운 회전을 함.
    }

    IEnumerator RotateAngle(float angle, int sign)
    {
        float mod = angle % rotateSpeed; // 남은 각도 계산
        for (float i = mod; i < angle; i += rotateSpeed)
        {
            character.transform.Rotate(0, 0, sign * rotateSpeed); // 캐릭터 rotateSpeed만큼 회전
            yield return new WaitForSeconds(0.005f); // 0.01초 대기
        }
        character.transform.Rotate(0, 0, sign * mod); // 남은 각도 회전
    }
}
