using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject EnemyPrefabs;
    public float width = 10f;
    public float height = 5f;
    public float SpawnDelay = 0.5f;

    private float formationSpeed = 2f;
    private float Xmin;
    private float Xmax;
    private Vector3 fireOffset = new Vector3(0, -1, 0);
    
    public bool movingRight = false;

	// Use this for initialization
	void Start () {
        Vector3 LeftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 RightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        Xmin = LeftBoundary.x;
        Xmax = RightBoundary.x;
        SpawnUntilFull();
	}
	
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(width,height));
    }

	// Update is called once per frame
	void Update () {
        if(movingRight)
        {
            transform.position += Vector3.right * formationSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * formationSpeed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f*width);
        float leftEdgeOfFormation = transform.position.x - (0.5f*width);
        if(leftEdgeOfFormation < Xmin)
        {
            movingRight = true; 
        }
        else if (rightEdgeOfFormation > Xmax)
        {
            movingRight = false; 
        }

        if(allEnemyDead())
        {
            SpawnUntilFull();
        }
       
	}

    void SpawnEnemy()
    {
        if(allEnemyDead() == true)
        {
            foreach (Transform child in transform)
            {
                GameObject Enemy = Instantiate(EnemyPrefabs, child.transform.position + fireOffset, Quaternion.identity) as GameObject;
                Enemy.transform.parent = child;
            }
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if(freePosition)
        {
            GameObject Enemy = Instantiate(EnemyPrefabs, freePosition.transform.position, Quaternion.identity) as GameObject;
            Enemy.transform.parent = freePosition;
        }
        if(NextFreePosition())
        {
             Invoke("SpawnUntilFull", SpawnDelay);
        }
       
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPositionObject in transform)
        {
            if(childPositionObject.childCount == 0)
            {
                return childPositionObject;
            }
        }
        return null;
    }


    bool allEnemyDead()
    {
        foreach (Transform childPositionObject in transform)
        {
            if (childPositionObject.childCount > 0)
            {
                return false;
            } 
        }
        return true;
    }
}
