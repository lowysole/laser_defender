using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Serialized Fields
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [Header("Projectiles")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.2f;


    //Set-up parameters
    float[] cameraVector;
    Coroutine firingCoroutine;
    HpImage hpManager;
    [SerializeField] int health;

    //Audio variables
    AudioSource laserSound;
    AudioSource deadSound;

    // Start is called before the first frame update
    void Start()
    {

        SetUpMoveBoundaries();
        deadSound = GetComponent<AudioSource>();
        laserSound = laserPrefab.GetComponent<AudioSource>();
        hpManager = FindObjectOfType<HpImage>();
    }

    // Update is called once per frame
    void Update()
    {
        SetUpLife();
        Move();
        Fire();
    }

    private void SetUpLife()
    {

        health = hpManager.GetHP();
    }
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(
                0, projectileSpeed);
            AudioSource.PlayClipAtPoint(
                laserSound.clip,
                laserPrefab.transform.position,
                laserSound.volume);
            

            yield return new WaitForSeconds(projectileFiringPeriod);
            
        }
    }

    private void Move()
    {
        //X
        var deltaX = Input.GetAxis("Horizontal") * NormalizeMovement();
        var newXPos = transform.position.x + deltaX;
        var newXPosBounded = Mathf.Clamp(
            newXPos, cameraVector[0] + padding, cameraVector[1] - padding);
        //Y
        var deltaY = Input.GetAxis("Vertical") * NormalizeMovement();
        var newYPos = transform.position.y + deltaY;
        var newYPosBounded = Mathf.Clamp(
            newYPos, cameraVector[2] + padding, cameraVector[3] - padding);

        //NewPos
        transform.position = new Vector2(newXPosBounded,newYPosBounded);
    }

    private float NormalizeMovement()
    {
        return Time.deltaTime * moveSpeed;
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        cameraVector = GetViewportToWorldPointPos(gameCamera);

    }

    private float[] GetViewportToWorldPointPos(Camera gameCamera)
    {
        Vector3 xyMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 xyMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        //xMin, xMax, yMin, yMax
        float[] cameraVector = { xyMin[0], xyMax[0], xyMin[1], xyMax[1] };
        return cameraVector;

    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        DamageDealer damageDealer = obj.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        HpManager(health);
        AudioSource.PlayClipAtPoint(deadSound.clip,
            transform.position,
            deadSound.volume);
    }

    private void HpManager(int hp)
    {
        if (hp > 0)
        {
            hpManager.DecreaseHP();
        }
        else if(hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        FindObjectOfType<Level>().LoadGameOver();

    }
}