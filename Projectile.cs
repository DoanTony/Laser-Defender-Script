using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public float Power = 50;

    public float inflictDamage()
    {
        return Power;
    }

    public void hit()
    {
        Destroy(gameObject);
    }
}
