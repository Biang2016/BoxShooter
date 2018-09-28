using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{

    // target impact on game
    public int scoreAmount = 0;
    public float timeAmount = 0.0f;

    // explosion when hit?
    public GameObject explosionPrefab;

    // when collided with another gameObject
    void OnCollisionEnter(Collision newCollision)
    {
        // exit if there is a game manager and the game is over
        if (GameManager.gm)
        {
            if (GameManager.gm.gameIsOver)
                return;
        }

        // only do stuff if hit by a projectile
        if (newCollision.gameObject.tag == "Projectile")
        {
            if (explosionPrefab)
            {
                // Instantiate an explosion effect at the gameObjects position and rotation
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }

            // if game manager exists, make adjustments based on target properties
            if (GameManager.gm)
            {
                GameManager.gm.targetHit(scoreAmount, timeAmount);
            }

            // destroy the projectile
            Destroy(newCollision.gameObject);

            // destroy self
            Destroy(gameObject);
        }
    }


    void OnParticleCollision(GameObject particleSystem)
    {
        if (GameManager.gm)
        {
            //if particles are from GoodBomb, get the positive scores and time, and destory the negative ones and BadBombs
            if (particleSystem.tag == "GoodBomb")
            {
                if (gameObject.tag != "BadBomb"&& gameObject.tag != "Negative")
                {
                    if (explosionPrefab) Instantiate(explosionPrefab, transform.position, transform.rotation);
                    GameManager.gm.targetHit(scoreAmount,timeAmount);
                }
                Destroy(gameObject);
            }
            else if (particleSystem.tag == "BadBomb")
            {
                if (gameObject.tag != "GoodBomb")
                {
                    if (explosionPrefab) Instantiate(explosionPrefab, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }
}
