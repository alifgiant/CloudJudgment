using UnityEngine;
using System.Collections;

public class lightning : MonoBehaviour {
	// Use this for initialization
	void Start () {
		animated = false;
		counter = 0f;
	}

//	public float y;

	void startAnimated(){
		//setOriginPos ();
		animated = true;
	}

	void stopAnimated(){
		animated = false;
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
	}

	bool animated;

	//void set

	//-3.61
	//-2.33
	// Update is called once per frame

	//public float coba;
	float counter;
	void Update () {
		//coba = transform.localPosition.y - originPos.y;
		//if (animated && transform.localPosition.y-originPos.y>-1.28f) {
		//	transform.Translate(0,-10f*Time.deltaTime,0);
		//}
		if (animated) {
			counter += Time.deltaTime;
			if (counter > 0.3f){
				stopAnimated();
				counter = 0;
			}
		}
	}
}
