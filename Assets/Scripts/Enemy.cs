using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Seralized parameters
    [SerializeField] float health = 100f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 6f;
    [SerializeField] float minTimeBtwShot = 0.3f;
    [SerializeField] float maxTimeBtwShot = 2f;
    [SerializeField] GameObject explotionFX;
    [SerializeField] int scoreEnemy = 10;

    float shotCounter;

    //Audio variables
    AudioSource laserSound;
    AudioSource deadSound;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBtwShot, maxTimeBtwShot);
        deadSound = GetComponent<AudioSource>();
        laserSound = laserPrefab.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CowndownAndShoot();
    }

    private void CowndownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(
                minTimeBtwShot, maxTimeBtwShot);
        }
    }

    private void Fire()
    {
        AudioSource.PlayClipAtPoint(laserSound.clip,
            transform.position,
            laserSound.volume);
        GameObject laser = Instantiate(
            laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(
            0, -projectileSpeed);

    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        DamageDealer damageDealer = obj.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deadSound.clip,
            transform.position,
            deadSound.volume);
        GameObject particle = Instantiate(
            explotionFX,
            transform.position,
            Quaternion.identity) as GameObject;
        FindObjectOfType<GameSession>().AddScore(scoreEnemy);
        Destroy(particle, 1);
        Destroy(gameObject);
    }
}
