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
    private Vector2 playerPos;
    private float targetAngle;
    private float rotValue;
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        mouseScreenPos = cam.ScreenToViewportPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        playerPos = new Vector2(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y);
        Debug.LogFormat("Player pos: {0} | Mouse pos: {1}", playerPos, mousePos);
        //Vector2 direction = playerPos - mousePos;

        _centre = player.GetComponent<Transform>().position;
        targetAngle = Vector2.Angle(mousePos, _centre);
        if (mouseScreenPos.x < 0.5)
            targetAngle = 360 - targetAngle;
        targetAngle *= Mathf.Deg2Rad;
        rotValue = RotateSpeed * Time.deltaTime;

        Debug.LogFormat("Target angle: {0} || Cur Angle: {1}", targetAngle, _angle);
        if (_angle < targetAngle && targetAngle - _angle > rotValue)
        {
            Debug.Log("A");
            _angle += rotValue;
        }
        else if (_angle > targetAngle && _angle - targetAngle > rotValue)
        {
            Debug.Log("B");
            _angle -= rotValue;
        }
        else
        {
            Debug.Log("C");
            _angle = targetAngle;
        }
        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        gameObject.GetComponent<Transform>().position = _centre + offset;
    }
}
