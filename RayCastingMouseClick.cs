using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RayCastingMouseClick : MonoBehaviour {

    public Camera mainCam;
    public int CSVFileCount = 0;
    public SaveToCSV csv;
    private bool isInTrial = false;
    
    // Update is called once per frame
    void Update () {
        var sourceDate = DateTime.Now;
        //Check if space button is pressed
        if (Input.GetKeyDown((KeyCode.Space)))
        {
            isInTrial = !isInTrial;
            if (!isInTrial)
            {
                CSVFileCount += 1;
                csv.FileName(sourceDate.ToString().Replace("/", "_").Replace("\\", "_").Replace(":", "_") +" " + CSVFileCount);
                csv.Save();
            }
        }

        if (isInTrial) 
        { 
            //Raycast - Cast array from the mouse click coordinate in the direction of view
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    // did you pick a Stimulus, and was it changed?
                    bool isChanged = false; 
                    var stim = hit.transform.gameObject.GetComponent<Stimulus>();
                    if (stim != null)
                    {
                        isChanged = stim.isChanged;
                    }

                    //Prints to the console the object name, the date, etc.
				    Debug.Log("You selected the " + hit.transform.name + " at " + sourceDate + ", whose changed status is " + isChanged); 
                    
                    //Sending the object variables from this script to another script
                    csv.Add(sourceDate.ToString(), hit.transform.name, hit.transform.ToString(), isChanged);
                }
            }
        
        }
    }
}
