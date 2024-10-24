using UnityEngine;

public class DestroyableEnemy : MonoBehaviour
{
    private void Start()
    {
        LevelEventManager.SendEnemySpawned();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Destroyable") || collision.gameObject.GetComponent<Bird>() != null)
        {
            LevelEventManager.SendEnemyDestroyed(transform.position);
            Destroy(gameObject);
        }
    }
}
