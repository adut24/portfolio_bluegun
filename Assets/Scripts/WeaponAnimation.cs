using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject player;
    private float RotateSpeed = 15f;
    private float Radius = 1.8f;

    private Vector2 _centre;
    private float _angle = 0.0f;
    private Vector2 mousePos;
    private Vector2 mouseScreenPos;
    private Vector2 camVec;
    private float targetAngle;
    private float rotValue;
    private Vector3 v3Pos;


    private int shortest_direction(float angleA, float angleB)
    {
        float alpha, beta, gamma;

        alpha = angleA - angleB;
        beta = angleA - angleB + 2 * MathF.PI;
        gamma = angleA - angleB - 2 * MathF.PI;
        if (MathF.Abs(alpha) < MathF.Abs(beta) && MathF.Abs(alpha) < MathF.Abs(gamma))
            return alpha < 0 ? -1 : 1;
        else if (MathF.Abs(beta) < MathF.Abs(gamma))
            return beta < 0 ? -1 : 1;
        return gamma < 0 ? -1 : 1;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        v3Pos = cam.WorldToScreenPoint(player.transform.position);
        v3Pos = Input.mousePosition - v3Pos;
        _centre = player.transform.position;
        targetAngle = (2.5f * MathF.PI) - Mathf.Atan2 (v3Pos.y, v3Pos.x);
        targetAngle %= 2 * MathF.PI;
        if (targetAngle > (2 * MathF.PI)) targetAngle -= 2 * MathF.PI;
        rotValue = RotateSpeed * Time.deltaTime;
        Debug.LogFormat("Target angle: {0} || Cur Angle: {1}", targetAngle, _angle);
        if (MathF.Abs (targetAngle - _angle) < rotValue)
            _angle = targetAngle;
        else
            _angle += shortest_direction(targetAngle, _angle) * rotValue;
        if (_angle < 0) _angle += 2 * MathF.PI;
        _angle %= 2 * MathF.PI;
        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        gameObject.transform.position = _centre + offset;
        gameObject.transform.eulerAngles = Vector3.forward * (405 - (Mathf.Rad2Deg * targetAngle));
    }
}
