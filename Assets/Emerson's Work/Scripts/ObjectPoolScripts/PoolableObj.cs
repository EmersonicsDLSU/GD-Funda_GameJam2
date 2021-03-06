using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObj : APoolable
{
    private Rigidbody2D objRb;
    // Start is called before the first frame update
    void Start()
    {
        //this.initialize();
    }
    
    public override void initialize()
    {
        /*
        EnemyStatistics enemyStats = this.GetComponentInChildren<EnemyStatistics>();
        enemyStats.spawnerSource = this.poolRef.spawnName;
        */
        this.transform.SetParent(this.poolRef.poolableLocation);
    }

    public override void onRelease()
    {

    }

    public override void onActivate()
    {
        //random spawn location
        this.transform.position = this.poolRef.poolableLocation.position +
            (transform.right * Random.Range(0.0f, 0.0f));
        this.transform.parent = this.poolRef.poolableLocation;
    }

    // Update is called once per frame
    void Update()
    {
        //continously will look / face the player
        rotateFacingThePlayer();
        //Transform trans = this.transform.GetChild(0).GetComponent<Transform>();
        //trans.Rotate(new Vector3(0, 69, 0));
    }

    //rotation speed when looking at the player
    [SerializeField] private float rotLookSpeed = 0.2f;

    private void rotateFacingThePlayer()
    {
        //Debug.Log($"Rotating: {null}");
        Transform trans = this.transform.GetChild(0).GetComponent<Transform>();
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Vector3 normalize = (playerPos - this.transform.position).normalized;
        float angle = Mathf.Atan2(normalize.y, normalize.x) * Mathf.Rad2Deg + 90.0f;
        trans.rotation = Quaternion.Slerp(trans.rotation, Quaternion.Euler(0.0f, angle, 0.0f), rotLookSpeed);
    }

    public GameObject createCopy(ObjPools pool)
    {
        //duplicate the origin copy
        GameObject go = GameObject.Instantiate(this.gameObject, 
            pool.poolableLocation.position, 
            Quaternion.identity) as GameObject;

        //awake
        go.GetComponent<PoolableObj>().poolRef = pool;
        go.GetComponent<PoolableObj>().initialize();

        return go;
    }
}

/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObj : APoolable
{
    private Rigidbody objRb;
    private const float Y_BOUNDARY = -7.95f;
    // Start is called before the first frame update
    void Start()
    {
        //this.initialize();
    }
    
    public override void initialize()
    {
        EnemyStatistics enemyStats = GetComponent<EnemyStatistics>();

        this.poolRef = GameObject.Find(enemyStats.spawnerSource).GetComponent<EnemySpawn>().enemyPool;
        objRb = this.GetComponent<Rigidbody>();
    }

    public override void onRelease()
    {

    }

    public override void onActivate()
    {
        //reset velocity
        objRb = this.GetComponent<Rigidbody>();
        this.objRb.ResetInertiaTensor();
        this.objRb.ResetCenterOfMass();
        this.objRb.velocity = transform.up * 0;
        //random spawn location
        this.transform.position = this.poolRef.poolableLocation.position +
            (transform.right * Random.Range(-1.0f, 1.0f));
        this.transform.parent = this.poolRef.poolableLocation;
    }

    // Update is called once per frame
    void Update()
    {
        if (objRb != null)
        {
            if (this.transform.position.y <= Y_BOUNDARY)
            {
                int index = this.poolRef.usedObjects.IndexOf(this.gameObject);
                this.poolRef.ReleasePoolable(index);
            }
        }
    }

    public GameObject createCopy(string spwnName)
    {
        //duplicate the origin copy
        GameObject go = GameObject.Instantiate(this.gameObject, this.transform.position, Quaternion.identity) as GameObject;
        EnemyStatistics enemyStats = go.GetComponent<EnemyStatistics>();
        enemyStats.spawnerSource = spwnName;

        initialize();

        go.transform.SetParent(this.poolRef.poolableLocation);
        return go;
    }
}

 */