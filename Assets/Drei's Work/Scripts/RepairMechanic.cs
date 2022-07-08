using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairMechanic : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    [SerializeField] Camera cameraobj;

    public Text repairingText;
    public Text repairProgressText;
    public Slider repairSlider;
    private float repairTicks = 0.0f;

    private GameObject barrierDoor;
    private GameObject barrierCr;
    private GameObject barrierDressing;
    private GameObject barrierBedroom;

    [Space]

    [Header("Barrier Audio Source")]
    public AudioSource barrierDoorRepair;
    public AudioSource barrierCrRepair;
    public AudioSource barrierDressingRepair;
    public AudioSource barrierBedroomRepair;

    private bool canRepair = true;

    // Start is called before the first frame update
    void Start()
    {
        barrierDoor = GameObject.FindGameObjectWithTag("BARRIER DOOR");
        barrierCr = GameObject.FindGameObjectWithTag("BARRIER CR");
        barrierDressing = GameObject.FindGameObjectWithTag("BARRIER DRESSING");
        barrierBedroom = GameObject.FindGameObjectWithTag("BARRIER BEDROOM");
        repairingText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(cameraobj.transform.position, this.transform.forward * 5, Color.magenta);
        if(Physics.Raycast(cameraobj.transform.position, cameraobj.transform.forward, out hit, 5))
        {
            if(Input.GetMouseButton(0))
            {
                

                if(hit.transform.tag == "REPAIR CUBE DOOR")
                {
                    if(barrierDoor.GetComponent<BarrierStatus>().health < 100 && canRepair == true && barrierDoor.GetComponent<BarrierStatus>().health > 0)
                    {
                        repairingText.gameObject.SetActive(true);
                        repairProgressText.gameObject.SetActive(true);
                        repairSlider.gameObject.SetActive(true);
                        repairTicks += Time.deltaTime;
                        repairingText.text = "Repairing Barricade: ";
                        float progress = repairTicks / 5.0f;
                        repairProgressText.text = (progress * 100).ToString("0.00") + "%";
                        repairSlider.value = progress;

                        if (barrierDoorRepair.isPlaying == false)
                            barrierDoorRepair.Play();

                        if (repairTicks >= 5.0f)
                        {
                            barrierDoor.GetComponent<BarrierStatus>().health += 40.0f;

                            if (barrierDoor.GetComponent<BarrierStatus>().health >= 100.0f)
                                barrierDoor.GetComponent<BarrierStatus>().health = 100.0f;

                            repairTicks = 0.0f;
                            canRepair = false;
                            repairSlider.value = 0.0f;
                            repairingText.gameObject.SetActive(false);
                            repairProgressText.gameObject.SetActive(false);
                            repairSlider.gameObject.SetActive(false);
                            barrierDoorRepair.Stop();
                        }
                    }  
                }
                else if (hit.transform.tag == "REPAIR CUBE DRESSING")
                {
                    if (barrierDressing.GetComponent<BarrierStatus>().health < 100 && canRepair == true && barrierDressing.GetComponent<BarrierStatus>().health > 0)
                    {
                        repairingText.gameObject.SetActive(true);
                        repairProgressText.gameObject.SetActive(true);
                        repairSlider.gameObject.SetActive(true);
                        repairTicks += Time.deltaTime;
                        repairingText.text = "Repairing Barricade: ";
                        float progress = repairTicks / 5.0f;
                        repairProgressText.text = (progress * 100).ToString("0.00") + "%";
                        repairSlider.value = progress;

                        if (barrierDressingRepair.isPlaying == false)
                            barrierDressingRepair.Play();

                        if (repairTicks >= 5.0f)
                        {
                            barrierDressing.GetComponent<BarrierStatus>().health += 40.0f;

                            if (barrierDressing.GetComponent<BarrierStatus>().health >= 100.0f)
                                barrierDressing.GetComponent<BarrierStatus>().health = 100.0f;

                            repairTicks = 0.0f;
                            canRepair = false;
                            repairSlider.value = 0.0f;
                            repairingText.gameObject.SetActive(false);
                            repairProgressText.gameObject.SetActive(false);
                            repairSlider.gameObject.SetActive(false);
                            barrierDressingRepair.Stop();
                        }
                    }
                }
                else if (hit.transform.tag == "REPAIR CUBE CR")
                {
                    if (barrierCr.GetComponent<BarrierStatus>().health < 100 && canRepair == true && barrierCr.GetComponent<BarrierStatus>().health > 0)
                    {
                        repairingText.gameObject.SetActive(true);
                        repairProgressText.gameObject.SetActive(true);
                        repairSlider.gameObject.SetActive(true);
                        repairTicks += Time.deltaTime;
                        repairingText.text = "Repairing Barricade: ";
                        float progress = repairTicks / 5.0f;
                        repairProgressText.text = (progress * 100).ToString("0.00") + "%";
                        repairSlider.value = progress;


                        if (barrierCrRepair.isPlaying == false)
                            barrierCrRepair.Play();

                        if (repairTicks >= 5.0f)
                        {
                            barrierCr.GetComponent<BarrierStatus>().health += 40.0f;

                            if (barrierCr.GetComponent<BarrierStatus>().health >= 100.0f)
                                barrierCr.GetComponent<BarrierStatus>().health = 100.0f;

                            repairTicks = 0.0f;
                            canRepair = false;
                            repairSlider.value = 0.0f;
                            repairingText.gameObject.SetActive(false);
                            repairProgressText.gameObject.SetActive(false);
                            repairSlider.gameObject.SetActive(false);
                            barrierCrRepair.Stop();
                        }
                    }
                }
                else if (hit.transform.tag == "REPAIR CUBE BEDROOM")
                {
                    if (barrierBedroom.GetComponent<BarrierStatus>().health < 100 && canRepair == true && barrierBedroom.GetComponent<BarrierStatus>().health > 0)
                    {
                        repairingText.gameObject.SetActive(true);
                        repairProgressText.gameObject.SetActive(true);
                        repairSlider.gameObject.SetActive(true);
                        repairTicks += Time.deltaTime;
                        repairingText.text = "Repairing Barricade: ";
                        float progress = repairTicks / 5.0f;
                        repairProgressText.text = (progress * 100).ToString("0.00") + "%";
                        repairSlider.value = progress;

                        if (barrierBedroomRepair.isPlaying == false)
                            barrierBedroomRepair.Play();

                        if (repairTicks >= 5.0f)
                        {
                            barrierBedroom.GetComponent<BarrierStatus>().health += 40.0f;

                            if (barrierBedroom.GetComponent<BarrierStatus>().health >= 100.0f)
                                barrierBedroom.GetComponent<BarrierStatus>().health = 100.0f;

                            repairTicks = 0.0f;
                            canRepair = false;
                            repairSlider.value = 0.0f;
                            repairingText.gameObject.SetActive(false);
                            repairProgressText.gameObject.SetActive(false);
                            repairSlider.gameObject.SetActive(false);
                            barrierBedroomRepair.Stop();
                        }
                    }
                }
            }
            else
            {
                repairSlider.value = 0.0f;
                repairingText.gameObject.SetActive(false);
                repairProgressText.gameObject.SetActive(false);
                repairSlider.gameObject.SetActive(false);
                repairTicks = 0.0f;
                canRepair = true;
                barrierDoorRepair.Stop();
                barrierDressingRepair.Stop();
                barrierCrRepair.Stop();
                barrierBedroomRepair.Stop();
            }
        }
    }
}
