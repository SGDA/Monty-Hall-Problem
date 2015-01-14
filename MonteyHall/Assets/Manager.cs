using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Manager : MonoBehaviour {
    public List<GameObject> doors;
	public GameObject gui;
	GameObject selected;
	bool switched;
	float iSwitched = 0,iNotSwitch = 0, iSwitchSuccess = 0, iNotSwitchSuccess = 0;
	float percSwitch = 0.0f, percNotSwitch = 0.0f;
	Vector3[] positions;
	Ray ray;
	string gameState = "Initial";
	// Use this for initialization
	void Start () {
				positions = new Vector3[doors.Count];
				for (int i = 0; i < doors.Count; ++i) {
						positions [i] = doors [i].transform.position;
				}
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		}
	
	// Update is called once per frame
	void Update () 
	{
		/*Debug.DrawLine (ray.origin, ray.direction,Color.red);
        if (Input.GetMouseButtonDown(0))
        {
			Debug.Log ("Mouse Clicked");
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit,100.0f))
			{
				Debug.Log ("Ray Hit");
                if (hit.collider.transform.tag == "Door")
                {
                    DoorScript ds = hit.collider.GetComponent<DoorScript>();
                }
            }
        }*/

		switch (gameState) 
		{
		case "Initial":
			foreach(GameObject d in doors)
			{
				DoorScript script = d.GetComponent<DoorScript>();
				if(script.getOpen())
				{
					script.Close();
				}
			}
			RandomizeDoors();
			selected = null;

			gameState = "Stage1";
			Debug.Log (gameState);
			break;
		case "Stage1": // player select

			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				selected = doors[0];
				gameState = "Stage2";
				
				Debug.Log (gameState);
			}
			else if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				selected = doors[1];
				gameState = "Stage2";
				
				Debug.Log (gameState);
			}
			else if(Input.GetKeyDown(KeyCode.Alpha3))
			{
				selected = doors[2];
				gameState = "Stage2";
				
				Debug.Log (gameState);
			}
			break;
		case "Stage2": // reveal a goat && player switch?
			foreach(GameObject d in doors)
			{
				if(d == selected || d.name == "DoorCar")
				{
					continue;
				}
				else
				{

					DoorScript script= d.GetComponent<DoorScript>();
					if(!script.getOpen())
					{
					script.Open();
					}
					break;
				}
			}
			if(Input.GetKeyDown(KeyCode.Y))
			{
				switched = true;
				foreach(GameObject d in doors)
				{
					if(d == selected)
					{
						continue;
					}
					else if (d.GetComponent<DoorScript>().getOpen())
					{
						continue;
					}
					else
					{
						selected = d;
					}
				}
				gameState = "Stage3";
				Debug.Log (gameState);
			}
			else if(Input.GetKeyDown(KeyCode.N))
			{
				switched = false;
				gameState = "Stage3";
				Debug.Log (gameState);
			}
			break;
		case "Stage3" : //reveal car, calculate stats

			foreach(GameObject d in doors)
			{
				DoorScript script = d.GetComponent<DoorScript>();
				if(script.getOpen() == false)
				{
					script.Open();
				}
			}
			if(switched)
			{
				iSwitched++;
				if(selected.name == "DoorCar")
				{
					iSwitchSuccess++;
				}
				else
				{
					//do nothing
				}
				percSwitch = iSwitchSuccess/iSwitched;
			}
			else
			{
				
				iNotSwitch++;
				if(selected.name == "DoorCar")
				{
					iNotSwitchSuccess++;
				}
				else
				{
					//do nothing
				}
				percNotSwitch = iNotSwitchSuccess/iNotSwitch;
			}
			Debug.Log("Switches: " + iSwitched + "Switch Success: " + percSwitch + 
			          "\n NotSwitch: " + iNotSwitch + "NotSwitch Success: " + percNotSwitch);
			gameState = "Initial";
			
			Debug.Log (gameState);
			break;
		}
	}
	void RandomizeDoors()
	{
        int dL = doors.Count;

		foreach (Vector3 p in positions) 
		{
			int rand = Random.Range(0,dL);
            GameObject lDoor = doors[rand];
            lDoor.transform.position = p;
            lDoor.SetActive(true);
			doors.Remove(lDoor);
			doors.Add(lDoor);
            dL--;

		}
	}
}
