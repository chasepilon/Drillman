using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PE2D;

public class GemCollectable : MonoBehaviour
{
    public int value;
    public float rSpeed;
    public bool isFinalGem;

    public float speedOffset = .01f;
    public float lengthMultiplier = 40f;
    public int numToSpawn = 200;
    public WrapAroundType wrapAround;

    Vector2 rotationSpeed;
    Rigidbody body;
    bool isAlive;

    private void Start()
    {
        isAlive = true;
        body = GetComponent<Rigidbody>();
        rotationSpeed = new Vector2(0, rSpeed);
    }
    private void FixedUpdate()
    {
        body.AddTorque(rotationSpeed);
    }

    private void Update()
    {
        if (!isAlive)
            Destroy(gameObject);
    }

    public int CollectGem()
    {
        isAlive = false;
        if (isFinalGem)
        {
            SpawnExplosion(new Vector2(transform.position.x, transform.position.y));
            GameManager.Instance.GameOver();
            return 0;
        }

        SpawnExplosion(new Vector2(transform.position.x, transform.position.y));
        return value;
    }

    private void SpawnExplosion(Vector2 position)
    {
        float hue1 = Random.Range(0, 6);
        float hue2 = (hue1 + Random.Range(0, 2)) % 6f;
        Color colour1 = StaticExtensions.Color.FromHSV(hue1, 0.5f, 1);
        Color colour2 = StaticExtensions.Color.FromHSV(hue2, 0.5f, 1);

        for (int i = 0; i < numToSpawn; i++)
        {
            float speed = (18f * (1f - 1 / Random.Range(1f, 10f))) * speedOffset;

            var state = new ParticleBuilder()
            {
                velocity = StaticExtensions.Random.RandomVector2(speed, speed),
                wrapAroundType = wrapAround,
                lengthMultiplier = lengthMultiplier,
                velocityDampModifier = 0.94f,
                removeWhenAlphaReachesThreshold = true
            };

            var colour = Color.Lerp(colour1, colour2, Random.Range(0, 1));

            float duration = 320f;
            var initialScale = new Vector2(2f, 1f);


            ParticleFactory.instance.CreateParticle(position, colour, duration, initialScale, state);
        }
    }
}
