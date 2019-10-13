using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{
    public int numberEnemies;
    public float fractionOfGrappleEnemies;

    public GameObject enemy;

    public int worldWidth = 10;
    public int worldHeight = 10;
    //private List<Vector2> spawnPositions;

    public List<GameObject> currentEnemyList;
    public List<GameObject> targetEnemyList;
    
    public float spawnSpeed = 0;
    public int max_targets = 10;

    void OnDrawGizmosSelected()
    {
        // Draws a blue line from this transform to the target
        Gizmos.color = Color.blue;

        Vector3 bottomL = this.transform.position;
        Vector3 topL = new Vector3(bottomL.x, bottomL.y + worldHeight, bottomL.z);
        Vector3 topR = new Vector3(topL.x + worldWidth, topL.y, topL.z);
        Vector3 bottomR = new Vector3(topR.x, topR.y - worldHeight, topR.z);
        
        Gizmos.DrawLine(bottomL, bottomR);
        Gizmos.DrawLine(topL, topR);
        Gizmos.DrawLine(bottomR, topR);
        Gizmos.DrawLine(bottomL, topL);
    }
    
    
    
    void Start()
    {
        StartCoroutine(CreateWorld());
    }

    IEnumerator CreateWorld()
    {
//        for (int x = 0; x < worldWidth; x++)
//        {
//            for (int y = 0; y < worldHeight; y++)
//            {
//                Vector2 myVec = new Vector2(x,y);
//                spawnPositions.Add(myVec);
//            }
//        }
        
        for (int i = 0; i < worldHeight*worldWidth; i++)
        {
            yield return new WaitForSeconds(spawnSpeed);
            GameObject currentEnemy =Instantiate(enemy, Vector2.zero, enemy.transform
            .rotation) as GameObject;
            currentEnemy.transform.parent = transform;
            currentEnemy.transform.localPosition = new Vector2(
                Random.Range(0, worldWidth),
                Random.Range(0, worldHeight)
                );

            
            int enemyType = Random.Range(0, 2);
            if (enemyType==0 && targetEnemyList.Count < max_targets)
            {
                targetEnemyList.Add(currentEnemy);
            }
            else
            {
                enemyType=1;
            }
            print("Adding new enemy of type " + enemyType);
            currentEnemy.GetComponent<EnemyController>().enemyType = enemyType;
            currentEnemyList.Add(currentEnemy);

            //print(currentEnemyList.Count);
        }

        
    }

    public void resetList()
    {
        for(var i = currentEnemyList.Count - 1; i > -1; i--)
        {
            if (currentEnemyList[i] == null)
                currentEnemyList.RemoveAt(i);
        }
        for(var i = targetEnemyList.Count - 1; i > -1; i--)
        {
            if (targetEnemyList[i] == null)
                targetEnemyList.RemoveAt(i);
        }
        
    }


    // Update is called once per frame
    void Update()
    {
    }
}