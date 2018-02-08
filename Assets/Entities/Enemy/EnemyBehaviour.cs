using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour 
{
    public GameObject projectile;
    public float projectileSpeed = 10f;
    public float health = 150f;
    public float shotsPerSeconds = 0.5f;
    public int scoreValue = 150;

    public AudioClip fireSound;
    public AudioClip deathSound;

    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void Update()
    {
        float probability = Time.deltaTime * shotsPerSeconds;
        if(Random.value < probability)
        {
            Fire();   
        }
    }

    void Fire()
    {
        GameObject missile = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    void Die()
    {
        Destroy(gameObject);
        scoreKeeper.Score(scoreValue);
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if(missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if(health <= 0)
            {
                Die();
            }
        }
    } 
}
