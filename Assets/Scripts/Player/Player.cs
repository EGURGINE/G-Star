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
        // character가 바라보;고 있는 벡터

        float angle = Vector3.Angle(currentJoystickVec, originJoystickVec);
        int sign = (Vector3.Cross(currentJoystickVec, originJoystickVec).z > 0) ? -1 : 1;
        // angle: 현재 바라보고 있는 벡터와, 조이스틱 방향 벡터 사이의 각도
        // sign: character가 바라보는 방향 기준으로, 왼쪽:+ 오른쪽:-

        transform.Rotate(0, 0, sign * angle);

    }
}
