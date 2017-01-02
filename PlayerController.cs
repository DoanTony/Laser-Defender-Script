using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float HealthPoint = 100f;

    public float ShipSpeed;
    public GameObject projectile;

    //Projectile properties
    public float ProjectileSpeed;
    public float FiringRate = 0.2f;


    private float padding = 1;
    private float xmin;
    private float xmax;
    private Vector3 fireOffset = new Vector3(0,1,0);
	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 mostLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 mostRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = mostLeft.x+ padding;
        xmax = mostRight.x - padding; ;
	}
	
	// Update is called once per frame
	void Update () {
        PlayerControl();
	}

    void PlayerControl ()
    {
           if(Input.GetKey(KeyCode.A))
           {
               gameObject.transform.position += Vector3.left * ShipSpeed * Time.deltaTime;
           }
           else if(Input.GetKey(KeyCode.D))
           {
               gameObject.transform.position += Vector3.right * ShipSpeed * Time.deltaTime;
   
           }
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                InvokeRepeating("Fire",0.00001f,FiringRate);
                  
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                CancelInvoke("Fire");
            }
           float newX = Mathf.Clamp(gameObject.transform.position.x, xmin, xmax);
           transform.position = new Vector3(newX, transform.position.y,  transform.position.z);
    }

    void Fire()
    {

        GameObject laser = Instantiate(projectile, transform.position + fireOffset, Quaternion.identity) as GameObject;
        Rigidbody2D laserBody = laser.GetComponent<Rigidbody2D>();
        laserBody.velocity = new Vector2(0,ProjectileSpeed);
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        Projectile laser = collide.GetComponent<Projectile>();
        if (laser.CompareTag("EnemyFire"))
        {
            laser.hit();
            HealthPoint -= laser.inflictDamage();
            if(HealthPoint<=0)
            {
                Destroy(gameObject);
            }
            Debug.Log("PLAYER GOT HIT");

        }

    }
}
