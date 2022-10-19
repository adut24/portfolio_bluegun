using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
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
    private bool shootPause = false;
    private Coroutine shootCoroutine;

    /* ----- WEAPON PARAMETERS ---- */
    public float shootDelay = 1.0f;
    public float shootSpeed = 5.0f;
    public float size = 2.0f;
    public int spread = 15;
    public int damage = 10;
    public int projectileNumber = 1;
    public Projectile projectilePrefab;
    public float projectileOffset = 0.5f;
    public float projectileDuration = 0.2f;
    public int  pierce = 0;


    private int ShortestDirection(float angleA, float angleB)
    {
        float alpha, beta, gamma;

        alpha = angleA - angleB;
        beta = angleA - angleB + 2 * Mathf.PI;
        gamma = angleA - angleB - 2 * Mathf.PI;
        if (Mathf.Abs(alpha) < Mathf.Abs(beta) && Mathf.Abs(alpha) < Mathf.Abs(gamma))
            return alpha < 0 ? -1 : 1;
        else if (Mathf.Abs(beta) < Mathf.Abs(gamma))
            return beta < 0 ? -1 : 1;
        return gamma < 0 ? -1 : 1;
    }

    private IEnumerator resumeShoot()
    {
        shootPause = true;
        yield return new WaitForSeconds(shootDelay);
        shootPause = false;
    }

    private void launchProjectile(Vector3 startPosition, Vector3 direction)
    {
        for (int i = 0; i < projectileNumber; i++)
        {
            Projectile proj = Instantiate(projectilePrefab, startPosition, transform.rotation);
            proj.direction = direction;
            proj.speed = shootSpeed;
            proj.GetComponent<Transform>().localScale = new Vector3(size, size, 1);
            proj.damage = damage;
            proj.duration = projectileDuration;
            proj.pierce = pierce;

            if (spread > 0)
            {
                float variance = UnityEngine.Random.Range(-spread / 2, spread / 2);
                variance *= Mathf.Deg2Rad;
                Vector2 start;
                float varAngle = 2.5f * Mathf.PI - Mathf.Atan2 (direction.y, direction.x) + variance;
                proj.direction = new Vector3(Mathf.Sin(varAngle), Mathf.Cos(varAngle), 1);
            }
        }
        /*
        * ---- Spread feature. WIP ----
            Projectile proj = projectileObj.GetComponent<Projectile>();
            float angle = (2.5f * Mathf.PI) - Mathf.Atan2 (direction.y, direction.x);
            float variance = UnityEngine.Random.Range(-spread / 2, spread / 2) * Mathf.Deg2Rad;
            Vector3 start;
            Vector3 end;
            start = Quaternion.AngleAxis(angle - spread / 2, Vector3.forward) * direction;
            end = Quaternion.AngleAxis(angle + spread / 2, Vector3.forward) * direction;
            Debug.DrawLine(transform.position, start, Color.red, 10, true);
            Debug.DrawLine(transform.position, end, Color.red, 10, true);
            angle += variance;
            angle %= 2 * Mathf.PI;
            if (angle < 0)
                angle += 2 * Mathf.PI;
            Debug.Log("Angle after:" + angle);
            Debug.Log("Angle var:" + variance);
            Debug.Log("Direction:" + direction);

            proj.direction = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward) * direction;
            Debug.Log("proj direction:" + proj.direction);
        */
    }

    private void ShootWeapon()
    {
        if (shootPause == false)
        {
            Vector3 startPosition;
            Vector3 direction;

            direction = Vector3.Normalize(v3Pos);
            startPosition = transform.position + direction * projectileOffset;
            launchProjectile(startPosition, direction);
            shootCoroutine = StartCoroutine(resumeShoot());
            AudioSource source = GetComponent<AudioSource>();
            if (source != null)
                source.PlayOneShot(source.clip, 1f);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        v3Pos = cam.WorldToScreenPoint(player.transform.position);
        v3Pos = Input.mousePosition - v3Pos;
        _centre = player.transform.position;
        targetAngle = (2.5f * Mathf.PI) - Mathf.Atan2 (v3Pos.y, v3Pos.x);
        targetAngle %= 2 * Mathf.PI;
        if (targetAngle > (2 * Mathf.PI)) targetAngle -= 2 * Mathf.PI;
        rotValue = RotateSpeed * Time.deltaTime;
        if (Mathf.Abs (targetAngle - _angle) < rotValue)
            _angle = targetAngle;
        else
            _angle += ShortestDirection(targetAngle, _angle) * rotValue;
        if (_angle < 0) _angle += 2 * Mathf.PI;
        _angle %= 2 * Mathf.PI;
        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        gameObject.transform.position = _centre + offset;
        gameObject.transform.eulerAngles = Vector3.forward * (405 - (Mathf.Rad2Deg * targetAngle));
        if (Input.GetMouseButton(0))
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
