using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float HealthPoint;
    public GameObject EnemyLaser;
    public float ShootingSpeed;
    public float ShootingRate = 0.5f;


    void Update()
    {
        float probability = Time.deltaTime * ShootingRate;
        if(Random.value < probability)
        {
            Fire();
        }
       
    }

	void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile laser = collider.GetComponent<Projectile>();
        if (laser.CompareTag("PlayerFire")) 
        {
            laser.hit();
            HealthPoint -= laser.inflictDamage();
            if(HealthPoint  <= 0f)
            {
                Destroy(gameObject);
            }
        }
       
    }

    void Fire()
    {
        GameObject Laser = Instantiate(EnemyLaser, gameObject.transform.position, Quaternion.identity) as GameObject;
        Rigidbody2D LaserRigidBody = Laser.GetComponent<Rigidbody2D>();
        LaserRigidBody.velocity = new Vector2(0,-ShootingSpeed);
    }


}
