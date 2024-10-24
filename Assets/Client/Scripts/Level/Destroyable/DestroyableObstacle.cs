using UnityEngine;

public class DestroyableObstacle : MonoBehaviour
{
    [SerializeField] private DestroyableObstacleConfig _config;
    public enum ObstacleType
    {
        Default = 0,
        Fragile
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bird>() != null)
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= _config.BirdVelMagnitudeRequiredForDestruction)
            {
                LevelEventManager.SendObstacleDestroyed(_config.Type, transform.position);
                Bird collidedBird = collision.gameObject.GetComponent<Bird>();
                switch (_config.Type)
                {
                    case ObstacleType.Default:
                        collidedBird.GetComponent<Rigidbody2D>().velocity *= _config.DefaultBirdVelSlow;
                        break;
                    case ObstacleType.Fragile:
                        if (collidedBird._type == Bird.BirdType.Splitting)
                        {
                            collidedBird.GetComponent<Rigidbody2D>().velocity *= _config.AppropriateBirdVelSlow;
                        }
                        else
                        {
                            collidedBird.GetComponent<Rigidbody2D>().velocity *= _config.DefaultBirdVelSlow;
                        }
                        break;
                }

                if (AudioManager.Singleton != null)
                {
                    AudioManager.Singleton.Play("Obstacle_Destruction");
                }
                else
                {
                    Debug.Log("Audio manager is not created!");
                }
                Destroy(gameObject);
            }
        }
    }
}
