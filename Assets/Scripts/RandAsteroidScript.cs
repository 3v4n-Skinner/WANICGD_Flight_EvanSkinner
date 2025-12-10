using UnityEngine;

public class RandAsteroidScript : MonoBehaviour
{
    //important vars for the asteroid
    public Rigidbody2D asteroid_rb2d;
    public GameObject asteroid;

    //Preset ranges
    static private float maxSize = 2;
    [SerializeField] float maxSpeed = 10;
    static private float maxDirection = 360;
    void Start()
    {
        //Gives the player random things like size and velocity and where they will shoot
        float sizeOfAsteroid = Random.Range(0.5f, maxSize);
        asteroid.transform.localScale = new Vector3(sizeOfAsteroid,sizeOfAsteroid,1);
        asteroid.transform.Rotate(0,0,Random.Range(0,maxDirection +1));
        asteroid_rb2d.AddForce(new Vector2(Random.Range(50f,maxSpeed), Random.Range(50f, maxSpeed)));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
