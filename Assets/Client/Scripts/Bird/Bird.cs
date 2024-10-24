using UnityEngine;

public class Bird : MonoBehaviour
{
    public enum BirdType
    {
        Default = 0,
        Splitting,
        Accelerating
    }
    public BirdType _type;
    public bool _disabled = false;

    private Rigidbody2D _rb;
    private bool _usedSkill = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Destroyable") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (!_disabled)
            {
                LevelEventManager.SendActiveBirdDisabled();
                _disabled = true;
            }   
        } 
    }
    public void UseBirdSkill()
    {
        if (!_disabled && !_usedSkill)
        {
            switch (_type)
            {
                case BirdType.Default:
                    break;
                case BirdType.Splitting:
                    GameObject newBird = Instantiate(gameObject, transform.position + new Vector3(0.0f, -1.75f, 0.0f), Quaternion.identity);
                    newBird.GetComponent<Rigidbody2D>().velocity = _rb.velocity;
                    newBird.GetComponent<Bird>()._disabled = true;
                    newBird = Instantiate(gameObject, transform.position + new Vector3(0.0f, 1.75f, 0.0f), Quaternion.identity);
                    newBird.GetComponent<Rigidbody2D>().velocity = _rb.velocity;
                    newBird.GetComponent<Bird>()._disabled = true;
                    if (AudioManager.Singleton != null)
                    {
                        AudioManager.Singleton.Play("Bird_Splitted");
                    }
                    else
                    {
                        Debug.Log("Audio manager is not created!");
                    }
                    break;
                case BirdType.Accelerating:
                    _rb.velocity *= 1.5f;
                    if (AudioManager.Singleton != null)
                    {
                        AudioManager.Singleton.Play("Bird_Accelerated");
                    }
                    else
                    {
                        Debug.Log("Audio manager is not created!");
                    }
                    break;
            }
            _usedSkill = true;
        }
    }
}
