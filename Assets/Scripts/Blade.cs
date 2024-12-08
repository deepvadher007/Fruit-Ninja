using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private bool Slicing;
    private Collider bladeCollider;
    public TrailRenderer bladeTrail;

    public Vector3 direction {  get; private set; }
    public float sliceForce = 5f; 
    public float minSliceVelocity = 0.01f;

    private void Awake()
    {
        bladeCollider = GetComponent<Collider>();
        mainCamera = Camera.main;
        bladeTrail =GetComponentInChildren<TrailRenderer>();
    }

    public void OnEnable()
    {
        StopSlicing(); 
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing(); 
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();; 
        }
        else if(Slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        transform.position = newPosition;
        
        Slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        Slicing = false;

        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition  = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }
}
