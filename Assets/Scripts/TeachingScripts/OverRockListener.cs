using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverRockListener : MonoBehaviour {

    EducationController controller;
	// Use this for initialization
	void Start () {
        controller = GameObject.Find("educationField").GetComponent<EducationController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        if (controller.catSay == 0 && !controller.catSpeak)
        {
            if (controller.findRightStep())
                controller.catSay = 1;
            else
                controller.catSay = 2;
            controller.catCanSpeak = true;
        }
    }
    void OnMouseExit()
    {

    }
}
