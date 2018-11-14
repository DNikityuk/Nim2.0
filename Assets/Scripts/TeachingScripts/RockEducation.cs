using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEducation :Rock {
    
    float startX = -5.0f;
    float startY = 3.28f;
    int rockNumber;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public RockEducation(int rockNum, int numHeap) {
        rockNumber = rockNum;
        showEducatRock(numHeap);
        rocListener = rockPrefab.GetComponent<onRockListner>();
    }

    void showEducatRock(int numHeap)
    {
        float rockWidth = ((GameObject)Resources.Load("rockEducation")).GetComponent<Renderer>().bounds.extents.x;
        float x = (startX + rockNumber * (2 * rockWidth + rockWidth / 2));
        float y = (startY - numHeap * (1.66f));
        Vector3 spawnPosition = new Vector3(x, y, 0.0f);
        Quaternion spawnRotation = Quaternion.identity;
        rockPrefab = (GameObject)Instantiate(Resources.Load("rockEducation"), spawnPosition, spawnRotation);
        
    }
   
}
