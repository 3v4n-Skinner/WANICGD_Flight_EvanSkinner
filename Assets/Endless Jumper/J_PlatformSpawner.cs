using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
/* Class: J_PlatformSpawner
 * Original Author: Zev S.
 * Contributers: [Your Name]
 * Created: 11/26/24
 * Last Modified: 12/2/24
 * 
 * Purpose: Spawn Platforms for endless jumper game
 */
public class J_PlatformSpawner : MonoBehaviour
{

    ///Singleton refrence for Platformspawner
    public static J_PlatformSpawner Instance;

    [Tooltip("X is min spawnpoint, Y is max spawnpoint")]
    [SerializeField] Vector2 HorizontalBounds;
    
    [Tooltip("Maximum amount of platforms before they start despawning")]
    [SerializeField] int MaxPlatforms = 10;
    [Tooltip("Platforms spawned at start")]
    [SerializeField] int StartPlatforms = 5;

    [Tooltip("Array of levels")]
    [SerializeField] PlatformLevel[] Levels;
    //Level currently spawning
    int CurrentLevel;

    [Tooltip("Random seed used to generate platforms")]
    [SerializeField] int Seed;

    //Refrence to newest platform, used for postioning
    Transform topPlatform;
    //Queue of current platforms, used in despawning.
    Queue<GameObject> platforms  = new Queue<GameObject>();
    
    //Gets refrences
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //Seeds randomness
        Random.InitState(Seed);

        //Spawns starter platforms
        for (int i = 0; i < StartPlatforms; i++) {
            SpawnPlatform();
        }
        
    }

    /// <summary>
    /// Spawns individual platform
    /// </summary>
    public static void SpawnPlatform()
    {

        Instance.GetLevel(UI_Score.Instance.GetScore);

        //Creates platform from lottery
        Transform plat = Instantiate(ActiveLevel.GetPlatform(), Instance.transform).transform;
        Instance.platforms.Enqueue(plat.gameObject);

        //Calculates position of platform
        float yMod = Random.Range(ActiveLevel.HeightRange.x,ActiveLevel.HeightRange.y);
        float distanceMod = Random.Range(ActiveLevel.DistanceRange.x,ActiveLevel.DistanceRange.y);
        if(yMod> distanceMod)
        {
            distanceMod = yMod;
        }
        float xMod = Mathf.Sqrt(Mathf.Pow(distanceMod,2)-Mathf.Pow(yMod,2));
        
        //Places platform relative to previous platform
        if (Instance.topPlatform != null)
        {
            if (Instance.topPlatform.position.x + xMod > Instance.HorizontalBounds.y)
            {
                plat.position = Instance.topPlatform.position + new Vector3(-xMod, yMod);
            }
            else
            {
                plat.position = Instance.topPlatform.position + new Vector3(xMod, yMod);
            }
        }

        Instance.topPlatform = plat.transform;

        //Destroys extra platforms
        if(Instance.platforms.Count >= Instance.MaxPlatforms)
        {
            Destroy(Instance.platforms.Dequeue());
        }
    }

    //Returns current level
    public static PlatformLevel ActiveLevel => Instance.Levels[Instance.CurrentLevel];
   
    /// <summary>
    /// Caluclates current level based on set thresholds
    /// </summary>
    /// <param name="value">value to check against thresholds</param>
    /// <returns></returns>
    private int GetLevel(int value)
    {
        for (int i = Levels.Length - 1; i >= 0; i--)
        {
            if (Levels[i].Threshold <= value)
            {
                CurrentLevel = i;
                return i;
            }
        }
        throw new System.Exception($"No valid level {value}");
    }
}

/// <summary>
/// Data for individual level
/// </summary>
[System.Serializable]
public struct PlatformLevel
{
    [Tooltip("Value at which level starts")]
    public  int Threshold;

    [Tooltip("Lottery for levels")]
    public Lottery<GameObject> Platforms;

    /// <summary>
    /// Returns random platform from level
    /// </summary>
    /// <returns></returns>
    public GameObject GetPlatform()
    {
        return Platforms.Draw();
    }

    [Tooltip("Minimum and maximum distance platform can spawn above prior platform")]
    public Vector2 HeightRange;
    [Tooltip("Minimum and maximum distance platform can spawn from prior platform")]
    public Vector2 DistanceRange;

}

