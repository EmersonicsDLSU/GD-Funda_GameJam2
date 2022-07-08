using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class EnemyRandomLoc : MonoBehaviour
{
    //location GO's
    [SerializeField] private List <GameObject> enemyLocations;

    //current intruder's location
    [HideInInspector] public GameObject current_loc;

    //enemypool properties
    public EnemyPoolProperty enemyPool_property;

    public List<AudioClip> enemySFX;

    private bool playAtOnce = false;

    void Start()
    {
        //add the orig lcoations to the remainingList List
        for (int i = 0; i < enemyLocations.Count; i++)
        {
            remainingLocations.Add(enemyLocations[i]);
        }

        //assigns the orig values in the intruder's properties
        this.orig_intruder_damage = this.intruder_damage;
        this.orig_damage_speed = this.damage_speed;

        //for enemy object pool
        enemyPool_property.spawnName = this.gameObject.name;
        enemyPool_property.poolableLocation = this.GetComponent<Transform>();
        enemyPool_property.enemyPool = new ObjPools(enemyPool_property.enemySizeLimit, enemyPool_property.isFixedAllocation);
        enemyPool_property.enemyPool.Initialize(ref enemyPool_property.enemyGO, ref enemyPool_property.poolableLocation, enemyPool_property.spawnName);

        //first location destination of the intruder / enemy
        relocateIntruder();
    }

    // Update is called once per frame
    void Update()
    {
        DifficultyIncrement();
        intruderDamaging();
    }

    //Difficulty will increase per interval in seconds
    [SerializeField] private float increment_seconds_interval = 30.0f;
    //intruder incremental destructive properties
    [SerializeField] private float increment_damage = 5.0f;
    [SerializeField] private float increment_speed = 0.25f;
    private void DifficultyIncrement()
    {
        //Debug.LogError($"Time: {TimeManager.Instance.measuredTime}");
        //gets the multiplier based on the time interval for the rising difficulty
        int multiplier = (int)(TimeManager.Instance.measuredTime / this.increment_seconds_interval);
        //change the current intruder's properties with the additional multiplier
        this.intruder_damage = this.orig_intruder_damage + (multiplier * this.increment_damage);
        this.damage_speed = this.orig_damage_speed + (multiplier * this.increment_speed);
    }


    public float intruder_damage = 15.0f;
    private float orig_intruder_damage = 15.0f;
    [HideInInspector] public float current_damage;
    [HideInInspector] public bool isAtPlace = true;
    [SerializeField] private float damage_speed = 0.25f;
    private float orig_damage_speed = 15.0f;

    //Intruder damages the barrier
    private void intruderDamaging()
    {
        if(this.isAtPlace)
        {
            if(playAtOnce == false)
            {
                this.current_loc.GetComponent<BarrierStatus>().enemySounds.clip = clips[(int)Random.Range(0, clips.Length)];
                this.current_loc.GetComponent<BarrierStatus>().enemySounds.Play();
                playAtOnce = true;
                this.current_loc.GetComponent<BarrierStatus>().particleEffect.SetActive(true);
            }
            //access the stats of the location and its barrier object
            current_loc.GetComponent<BarrierStatus>().health -= intruder_damage * Time.deltaTime * damage_speed;
            if (current_loc.GetComponent<BarrierStatus>().health <= 0.0f)
            {
                current_loc.GetComponent<BarrierStatus>().health = 0.0f;
                //finishes the damaging phase
                current_damage = intruder_damage;
            }


            current_damage += intruder_damage * Time.deltaTime * damage_speed;
        }

        //if the intruder damage the barrier at its limit, relocate to another position
        //Debug.Log($"TotalDamage: {current_damage}");
        if (current_damage >= intruder_damage)
        {
            if(checkRemainingLocations())
            {
                //relocate and stop producing destroying barriers sound
                this.isAtPlace = false;
                //reset the current_damage counter
                current_damage = 0;
                //---temporary--- accessing the object property 
                /*
                 this.current_loc.GetComponent<BarrierStatus>().audioSrc.clip.stopSound();
                 */
                //call the transition
                enemyPool_property.enemyPool.ReleasePoolable(enemyPool_property.enemyPool.usedObjects.Count - 1);
                //stops the sound
                this.current_loc.GetComponent<BarrierStatus>().enemySounds.Stop();
                //stops the particle system
                this.current_loc.GetComponent<BarrierStatus>().particleEffect.SetActive(false);
                StartCoroutine(transitionDelay());
            }
            else
            {
                //Game Over
                GameManager.EndGame();
            }
        }
    }

    //Random relocation properties
    [SerializeField] private float relocate_interval = 5.0f;
    //Temporary dispel for the Intruder
    [HideInInspector] public float increment_relocate_invterval = 0.0f;
    IEnumerator transitionDelay()
    {
        //transition duration
        yield return new WaitForSeconds(relocate_interval + increment_relocate_invterval);
        relocateIntruder();
    }

    //---this is just an example / temporary---
    [SerializeField] private AudioClip[] clips;

    public enum Intruder_Sounds
    {
        KNOCK = 0, KICK, SHOOT, ETC
    }

    //Check if the player can still play
    private bool checkRemainingLocations()
    {
        //Checks all locations status, if they still have barriers, then they can be use again
        remainingLocations.Clear();
        foreach (var item in enemyLocations)
        {
            if (item.GetComponent<BarrierStatus>().health > 0.0f)
            {
                remainingLocations.Add(item);
            }
        }
        //if no more availabe locations, then the game is over
        if (remainingLocations.Count == 0)
        {
            //Debug.LogError($"All locations / barriers are destroyed");
            //Debug.LogError($"GAME OVER");
            return false;
        }
        return true;
    }

    private List<GameObject> remainingLocations = new List<GameObject>();

    //Intruder will now randomnly relocate to a new location on the house
    private void relocateIntruder()
    {
        //removes the dispel effect after relocating
        this.increment_relocate_invterval = 0.0f;
        //removes the previous location in the remaining locations
        if (remainingLocations.Contains(this.current_loc) && remainingLocations.Count > 1)
        {
            //then remove it so it will not be pick again
            remainingLocations.Remove(this.current_loc);
        }

        //intruder has found a new location, now he is in place
        this.isAtPlace = true;
        //picks a random location
        int randIndex = Random.Range(0, remainingLocations.Count);
        //sets a new currentloc value based from the random index value
        this.current_loc = remainingLocations[randIndex];

        //Debug.LogError($"Location Picked: {remainingLocations[randIndex].transform.tag}");

        //check if it has available pool objs
        if (enemyPool_property.enemyPool.HasObjectAvailable(1))
        {
            //spawns the enemy
            enemyPool_property.enemyPool.RequestPoolable();
            assignGOtoParent();
        }

        //Debug.Log($"Relocate to Location Index: {randIndex}");

        //---temporary--- accessing the object property 
        playAtOnce = false;
    }
         

    private void assignGOtoParent()
    {
        //Debug.LogError($"Pool Size: {enemyPool_property.enemyPool.usedObjects.Count}");
        //Debug.Log("Assign to Parent");
        enemyPool_property.enemyPool.usedObjects[
            enemyPool_property.enemyPool.usedObjects.Count - 1].transform.position =
            this.current_loc.transform.localPosition;
        enemyPool_property.enemyPool.usedObjects[
            enemyPool_property.enemyPool.usedObjects.Count - 1].transform.parent = this.current_loc.GetComponent<Transform>();
        enemyPool_property.enemyPool.usedObjects[
            enemyPool_property.enemyPool.usedObjects.Count - 1].SetActive(true);
        //Debug.LogError($"Set active to true");
    }
}
