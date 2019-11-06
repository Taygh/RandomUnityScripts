using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Statue: This class enables the destruction, animation, and enemy spawning that accompanies
the statue obstacles found in "Boundless." */

public class Statue : MonoBehaviour
{
    //Does this statue have an enemy inside?
    [SerializeField]
    bool isEnemy;
    //Reference to enemy prefab to instantiate
    [SerializeField]
    GameObject enemyPrefab;
    //Values to define new collider after the statue is destroyed and time delay for spawning enemy
    [SerializeField]
    float rubbleColliderSizeX, rubbleColliderSizeY, rubbleColliderOffsetX, rubbleColliderOffsetY, enemySpawnDelay;
    //Reference to statue's collider component
    BoxCollider2D statueCollider;

    void Start()
    {
        //Atatch the breaking script to the Animator, set it to play once, enable to play on collision
        gameObject.GetComponent<Animator>().enabled = false;
        //Get this statue's collider
        statueCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    public void Break()
    {
        //Play animation of breaking
        gameObject.GetComponent<Animator>().enabled = true;

        //Set new collider size based on sprite of destroyed statue
        statueCollider.size = new Vector2(rubbleColliderSizeX, rubbleColliderSizeY);
        statueCollider.offset = new Vector2(rubbleColliderOffsetX, rubbleColliderOffsetY);

        //Spawn enemy if this statue contains one and there is a defined reference to an enemy prefab (null reference protection)
        if (isEnemy && enemyPrefab != null)
        {
            StartCoroutine(WaitAndSpawn(enemySpawnDelay));
        }
    }

    //Coroutine to delay enemy spawning if designer chooses to provide one
    private IEnumerator WaitAndSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
