using UnityEngine;

public class RandAsteroidScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D asteroid_rb2d;
    public GameObject asteroid;

    //Preset ranges
    static private float maxSize = 2;
    [SerializeField] float maxSpeed = 10;
    static private float maxDirection = 360;
    void Start()
    {
        float sizeOfAsteroid = Random.Range(0.5f, maxSize);
        asteroid.transform.localScale = new Vector3(sizeOfAsteroid,sizeOfAsteroid,1);
        asteroid.transform.Rotate(0,0,Random.Range(0,maxDirection +1));
        asteroid_rb2d.AddForce(new Vector2(Random.Range(50f,maxSpeed), Random.Range(50f, maxSpeed)));
        //lol
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
