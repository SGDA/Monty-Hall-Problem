using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	public GameObject hindge;
	bool open = false;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Open()
	{
		hindge.transform.Rotate (new Vector3(0,0,90));
		open = true;
	}
	public void Close()
	{
		hindge.transform.rotation=  (new Quaternion(0,0,0,0));
		open = false;
	}
	public bool getOpen(){return open;}
}
