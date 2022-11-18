using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ParticleSystem playerSpawnPc;
    public float playerSpd;
    public ShotArea shotArea;
    public SkinCheker SC;

    public bl_Joystick js;
    private void Update()
    {
        Vector3 dir = new Vector3(js.Horizontal,js.Vertical,0);
        dir.Normalize();
        transform.position += dir * playerSpd * Time.deltaTime;
        TurnAngle(dir);
    }

    private void TurnAngle(Vector3 currentJoystickVec)
    {
        Vector3 originJoystickVec = transform.up;
        // character�� �ٶ�;�� �ִ� ����

        float angle = Vector3.Angle(currentJoystickVec, originJoystickVec);
        int sign = (Vector3.Cross(currentJoystickVec, originJoystickVec).z > 0) ? -1 : 1;
        // angle: ���� �ٶ󺸰� �ִ� ���Ϳ�, ���̽�ƽ ���� ���� ������ ����
        // sign: character�� �ٶ󺸴� ���� ��������, ����:+ ������:-

        transform.Rotate(0, 0, sign * angle);

    }
}
