using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public Toggle fullscreenTog;
    public Toggle vsyncTog;

    public List<ResItem> resolutions = new List<ResItem>();
    private int counter;
    public TMP_Text resolutionLabel;
    // Start is called before the first frame update
    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }
        bool foundRes = false;

        for(int i = 0; i < resolutions.Count; i++)
        {
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;

                counter = i;
                UpdateResLabel();
            }
        }

        if (!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            counter = resolutions.Count - 1;
            UpdateResLabel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResLeft()
    {
        counter--;
        if(counter < 0)
        {
            counter = 0;
        }
        UpdateResLabel();
    }
    public void ResRight()
    {
        counter++;
        if (counter > resolutions.Count - 1)
        {
            counter = resolutions.Count - 1;
        }
        UpdateResLabel();
    }
    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[counter].horizontal.ToString() + " x " + resolutions[counter].vertical.ToString();
    }
    public void ApplyGraphics()
    {
        //Screen.fullScreen = fullscreenTog.isOn;
        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        Screen.SetResolution(resolutions[counter].horizontal, resolutions[counter].vertical, fullscreenTog.isOn);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal;
    public int vertical;
}