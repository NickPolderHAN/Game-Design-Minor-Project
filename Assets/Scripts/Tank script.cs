using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tankscript : MonoBehaviour
{
    private Ray _ray;
    private Ray _shotRay;
    private RaycastHit _hit;
    private int _speed;
    
    // Start is called before the first frame update
    void Start()
    {
        shoot("ground");
    }

    public void shoot(String laserType)
    {
        if (laserType == "ground")
        {
            _ray = new Ray(transform.position, -transform.up);
            CheckGroundColliders();
        }
        else if (laserType == "shot fired")
        {
            _shotRay = new Ray(transform.position, transform.right);
            CheckShotColliders();
        }
    }

    public void OnShoot()
    {
        shoot("shot fired");
    }

    public void OnRotateleft()
    {
        // Create a rotation increment around the Y-axis (upward direction)
        Quaternion rotation = Quaternion.Euler(0, -1, 0);

        // Apply the rotation to the current rotation
        transform.rotation *= rotation;
    }
    
    public void OnRotateright()
    {
        // Create a rotation increment around the Y-axis (upward direction)
        Quaternion rotation = Quaternion.Euler(0, 2, 0);

        // Apply the rotation to the current rotation
        transform.rotation *= rotation;
    }
    
    public void OnMoveforward()
    {
        transform.position += transform.right;
        shoot("ground");

    }
    
    public void OnMovebackwards()
    {
        transform.position += -transform.right;
        shoot("ground");
    }
    
    void CheckGroundColliders()
    {
        if (Physics.Raycast(_ray, out _hit))
        {
            transform.position = new Vector3(
                transform.position.x, _hit.collider.gameObject.transform.position.y + 1,
                transform.position.z);
        }
    }
    void CheckShotColliders()
    {
        if (Physics.Raycast(_shotRay, out _hit))
        {
            if (!_hit.collider.gameObject.name.Contains("plane"))
            {
                // Find the GameObject by tag, name, or use a reference
                GameObject objectToChangeMaterial = _hit.collider.gameObject;

                // Access the renderer component
                Renderer objectRenderer = objectToChangeMaterial.GetComponent<Renderer>();

                // Change the material
                Material loadedMaterial = Resources.Load<Material>("Assets/Materials/Tank skin.mat");
                objectRenderer.material = loadedMaterial;
            }
        }
    }
}
