using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float drillRange;

    public float speed;
    public float drillSpeed;

    Rigidbody body;
    Vector3 rotationSpeed;
    bool isDrilling;
    AudioSource breakSFX;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        isDrilling = false;
        rotationSpeed = new Vector3(0, drillSpeed, 0);
        breakSFX = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal");

        Vector3 newPos = new Vector3(moveH * speed, 0, 0);
        body.AddForce(newPos);

        if (isDrilling && GameManager.Instance.fuelRemaining > 0)
        {
            body.AddTorque(rotationSpeed);
            CheckIfOverBreakableBlock();
        }
        else
        {
            body.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isDrilling = true;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            isDrilling = false;
        }
    }

    void CheckIfOverBreakableBlock()
    {
        //Creates the start and end points for the lineCast
        Vector3 lineOrigin = transform.position;
        Vector3 lineTarget = lineOrigin;
        lineTarget.y -= drillRange;

        RaycastHit hit;
        Debug.DrawLine(lineOrigin, lineTarget, Color.red);

        if (Physics.Linecast(lineOrigin, lineTarget, out hit))
        {
            BreakableCube breakable = hit.collider.GetComponent<BreakableCube>();

            if (hit.collider != null && breakable != null)
            {
                breakable.DrillThroughCube();
                GameManager.Instance.DecreaseFuelRemaining();
                if (!breakSFX.isPlaying)
                    breakSFX.Play();
            }
            else
            {
                UnbreakableCube unbreakable = hit.collider.GetComponent<UnbreakableCube>();

                if (hit.collider != null && unbreakable != null)
                {
                    unbreakable.FailDrillingThroughCube();
                }
            }
        }
    }
}
