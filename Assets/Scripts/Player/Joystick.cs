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
            // ���̽�ƽ ���� ���� (2.6�� 2�� ������ ���� TouchArea�� ���� ������)

            TurnAngle(joystickVector);
            // Character���� ���̽�ƽ ���� �ѱ��


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
        // character�� �ٶ󺸰� �ִ� ����

        float angle = Vector3.Angle(currentJoystickVec, originJoystickVec);
        int sign = (Vector3.Cross(currentJoystickVec, originJoystickVec).z > 0) ? -1 : 1;
        // angle: ���� �ٶ󺸰� �ִ� ���Ϳ�, ���̽�ƽ ���� ���� ������ ����
        // sign: character�� �ٶ󺸴� ���� ��������, ����:+ ������:-

        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartCoroutine(RotateAngle(angle, sign));
        // �ڷ�ƾ�� �������̸� ���� ���� �ڷ�ƾ �ߴ� �� �ڷ�ƾ ���� 
        // �ڷ�ƾ�� �� ���� �����ϵ���.
        // => ȸ�� �߿� ���ο� ȸ���� ������ ���, ȸ�� ���̴� ���� ���߰� ���ο� ȸ���� ��.
    }

    IEnumerator RotateAngle(float angle, int sign)
    {
        float mod = angle % rotateSpeed; // ���� ���� ���
        for (float i = mod; i < angle; i += rotateSpeed)
        {
            character.transform.Rotate(0, 0, sign * rotateSpeed); // ĳ���� rotateSpeed��ŭ ȸ��
            yield return new WaitForSeconds(0.005f); // 0.01�� ���
        }
        character.transform.Rotate(0, 0, sign * mod); // ���� ���� ȸ��
    }
}
