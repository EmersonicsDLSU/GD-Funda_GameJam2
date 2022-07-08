using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    //World Space UI GO
    GameObject Indicator;

    //mainPlayer character GO
    GameObject target;

    Renderer rd;

    // Start is called before the first frame update
    void Start()
    {
        rd = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void checkVisibilityInCameraView()
    {
        if(rd.isVisible == false)
        {
            //checks if the UI indicator is not displayed; if not then display
            if(Indicator.activeSelf == false)
            {
                Indicator.SetActive(true);
            }
            //////////
            //sample test

            Vector3 playerPos = target.transform.position;
            //enemyPos not given
            Vector3 enemyPos = this.transform.position;
            float mag = Vector3.Distance(playerPos, enemyPos);
            Vector3 dir = (playerPos - enemyPos).normalized;

            //////////

            Vector2 direction = target.transform.position - transform.position;

            //RaycastHit rayy = Physics.Raycast(transform.position, direction);

            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, Mathf.Infinity);

            if(ray.collider != null)
            {
                Indicator.transform.position = ray.point;
            }
            else
            {
                if(Indicator.activeSelf == true)
                {
                    Indicator.SetActive(false);
                }
            }
        }
    }
}
