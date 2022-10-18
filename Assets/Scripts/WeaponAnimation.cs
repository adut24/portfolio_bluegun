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
    private float targetAngle;
    private float rotValue;
    private Vector3 v3Pos;

    public float shootDelay = 1.0f;
    public float shootSpeed = 5.0f;
    public float size = 3.0f;
    public int projectileNumber = 1;
    public Projectile projectilePrefab;
    public float projectileOffset = 0.5f;
    private bool shootPause = false;
    private Coroutine shootCoroutine;

    private int ShortestDirection(float angleA, float angleB)
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

    private IEnumerator resumeShoot()
    {
        shootPause = true;
        yield return new WaitForSeconds(shootDelay);
        shootPause = false;
    }
    private void ShootWeapon()
    {
        if (shootPause == false)
        {
            Debug.Log("hello");
            Vector3 startPosition;
            Vector3 direction;

            direction = Vector3.Normalize(v3Pos);
            startPosition = transform.position + direction * projectileOffset;
            Projectile proj = Instantiate(projectilePrefab, startPosition, transform.rotation);
            // Projectile proj = projectileObj.GetComponent<Projectile>();
            proj.direction = direction;
            proj.speed = shootSpeed;
            shootCoroutine = StartCoroutine(resumeShoot());
        }
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
        if (MathF.Abs (targetAngle - _angle) < rotValue)
            _angle = targetAngle;
        else
            _angle += ShortestDirection(targetAngle, _angle) * rotValue;
        if (_angle < 0) _angle += 2 * MathF.PI;
        _angle %= 2 * MathF.PI;
        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        gameObject.transform.position = _centre + offset;
        gameObject.transform.eulerAngles = Vector3.forward * (405 - (Mathf.Rad2Deg * targetAngle));
        if (Input.GetMouseButtonDown(0))
            ShootWeapon();
    }


    public Vector3 PlayerDirection()
    {
        return v3Pos;
    }

    public float PlayerLookAngle()
    {
        return targetAngle;
    }

}
