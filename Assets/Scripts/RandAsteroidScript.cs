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
        //Gives the asteroid a random size then applys that size
        float sizeOfAsteroid = Random.Range(0.5f, maxSize);
        asteroid.transform.localScale = new Vector3(sizeOfAsteroid,sizeOfAsteroid,1);

        //Gives the asteroid a random rotation
        asteroid.transform.Rotate(0,0,Random.Range(0,maxDirection +1));

        //This adds a force to the asteroid so that it gets propeled somewhere 
        asteroid_rb2d.AddForce(new Vector2(Random.Range(50f,maxSpeed), Random.Range(50f, maxSpeed)));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
