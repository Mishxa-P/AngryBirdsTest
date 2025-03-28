using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BirdsLauncher : MonoBehaviour
{
    [Header("LauncherConfig")]
    [SerializeField] private BirdsLauncherConfig _config;
    [Header("BirdSpawner")]
    [SerializeField] private BirdsSpawner _birdsSpawner;
    [Header("Clamps")]
    [SerializeField] private BirdsLauncherClamp _leftClamp;
    [SerializeField] private BirdsLauncherClamp _rightClamp;

    private LineRenderer _lineRenderer;
    private GameObject _activeBird;

    private Vector3 _launcherStartingPoint;
    private Vector2 _throwVel;
    private bool _isDisabled = true;

    private void Start()
    {
        _launcherStartingPoint = transform.position;
        _lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(ActivateLauncherWidhDelay());
    }
    public void UpdateVelocity(Vector3 pullPosition)
    {
        if (_activeBird == null || _isDisabled)
        {
            return;
        }

        float distance = Vector3.Distance(pullPosition, _launcherStartingPoint);
        if (distance > _config.MaxPullRadius)
        {
            Vector3 fromLauncherToPull = pullPosition - _launcherStartingPoint;
            fromLauncherToPull *= _config.MaxPullRadius / distance;
            pullPosition = _launcherStartingPoint + fromLauncherToPull;
        }
        float angle = Vector2.Angle(_launcherStartingPoint - pullPosition, Vector2.up);
        if (transform.position.x < pullPosition.x)
        {
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, -angle);
        }
        _activeBird.transform.position = pullPosition;
        _throwVel = (_launcherStartingPoint - pullPosition) * _config.Power;
    }
    public void UpdateTrajectory(Vector3 pullPosition)
    {
        _leftClamp.DrawCatapultRope(pullPosition);
        _rightClamp.DrawCatapultRope(pullPosition);

        _lineRenderer.positionCount = _config.LinePositionsCount;
        Vector3[] linePositions = new Vector3[_lineRenderer.positionCount];
        float localDeltaTime = _config.LineDeltaTime;
        float localTime = 0;
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            float newX = KinematikEquation(0, _throwVel.x, pullPosition.x, localTime);
            float newY = KinematikEquation(Physics2D.gravity.y, _throwVel.y, pullPosition.y, localTime);
            linePositions[i] = new Vector3(newX, newY, pullPosition.z);
            localTime += localDeltaTime;
        }
        _lineRenderer.SetPositions(linePositions);
    }
    public void Launch()
    {
        if (_activeBird == null || _isDisabled)
        {
            return;
        }

        _activeBird.GetComponent<Rigidbody2D>().isKinematic = false;
        _activeBird.GetComponent<Rigidbody2D>().velocity = _throwVel;
        _leftClamp.RemoveCatapultRope();
        _rightClamp.RemoveCatapultRope();
        if (AudioManager.Singleton != null)
        {
            AudioManager.Singleton.Play("Bird_Launched");
        }
        else
        {
            Debug.Log("Audio manager is not created!");
        }
        DisableLauncher();
    }
    public IEnumerator ActivateLauncherWidhDelay()
    {
        yield return new WaitForSeconds(_config.FirstTimeActivationDelay);
        ActivateLauncher();
    }
    public void ActivateLauncher()
    {
        _isDisabled = false;
        _activeBird = _birdsSpawner.GetNextBird();
        if (_activeBird != null)
        {
            _activeBird.transform.position = transform.position;
            if (CameraManager.Singleton != null)
            {
                CameraManager.Singleton.SetFollowTarget(_activeBird.transform);
            }
            else
            {
                Debug.Log("CameraManager is not created!");
            }
        }
        Debug.Log("BirdLauncherActivated");
    }
    public void UseActiveBirdSkill()
    {
        if (_activeBird != null && _isDisabled)
        {
            _activeBird.GetComponent<Bird>().UseBirdSkill();
        }
    }
    public void DisableLauncher()
    {
        _isDisabled = true;
    }
    private float KinematikEquation(float acceleration, float vel, float pos, float time)
    {
        return 0.5f * acceleration * time * time + vel * time + pos;
    }
}
