using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    private GameObject character => GameManager.Instance.player.gameObject;

    private int isTouch = 0;

    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 joystickVector;

    private Coroutine runningCoroutine;
    private float rotateSpeed = 1000f;

 
    private void FixedUpdate()
    {
        character.GetComponent<Rigidbody2D>().velocity = character.transform.up * GameManager.Instance.player.playerSpd * isTouch;
    }
    public void OnDrag(PointerEventData eventData)
    {
        endPos = eventData.position;

        joystickVector = new Vector2((endPos.x - startPos.x), (endPos.y - startPos.y));

        TurnAngle(joystickVector);
        // Character���� ���̽�ƽ ���� �ѱ��
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = 1;
        startPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = 0;
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
