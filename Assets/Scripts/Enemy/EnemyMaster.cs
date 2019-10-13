using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{
    public enum EnemyType
    {
        GRAPPLING,
        STATIC,
        BALLISTIC,
        ZOMBIE,
        FOLLOWER,
        SHOOTER
    };

    public int numberEnemies;

    public GameObject enemy;

    private GameController gameController;

    public int worldWidth = 10;
    public int worldHeight = 10;
    private int numEnemies;

    public List<GameObject> currentEnemyList;
    
    public float spawnSpeed = 1;
    public int maxTargets = 10;

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
        numberEnemies = worldHeight * worldWidth;
        gameController = FindObjectOfType<GameController>();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberEnemies; i++)
        {
            yield return new WaitForSeconds(spawnSpeed);
            if (!gameController.isGameOver)
            {
                InstantiateEnemy();
            }

        }
    }
    
    /// <summary>
    /// Instantiates an enemy 
    /// </summary>
    void InstantiateEnemy()
    {
        GameObject currentEnemy = Instantiate(
                original: enemy,
                position: Vector2.zero,
                rotation: enemy.transform.rotation)
            ;
        currentEnemy.transform.parent = transform;
        currentEnemy.transform.localPosition = new Vector2(
            Random.Range(0, worldWidth),
            Random.Range(0, worldHeight)
        );
        
        AddEnemyToList(currentEnemy);
    }

    private void AddEnemyToList(GameObject newEnemy)
    {
        EnemyType enemyType = newEnemy.GetComponent<EnemyController>().type;
        currentEnemyList.Add(newEnemy);
    }


    public void CleanEnemyListsOfDeadEnemies()
    {
        for (var i = currentEnemyList.Count - 1; i > -1; i--)
        {
            if (currentEnemyList[i] == null)
                currentEnemyList.RemoveAt(i);
        }
    }
}