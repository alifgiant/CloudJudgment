using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class soundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void setSound(){
		GameObject cam = GameObject.Find ("Main Camera");
		Sprite now = cam.GetComponent<gameScript> ().setSound ();
		gameObject.GetComponent<Image> ().sprite = now;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
