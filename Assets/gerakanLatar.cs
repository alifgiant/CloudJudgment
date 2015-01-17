using UnityEngine;
using System.Collections;

public class gerakanLatar : MonoBehaviour {
	public Sprite[] awan;
	public float screenSize;
	public int direction;
	private float speed;
	// Use this for initialization
	void Start () {
		screenSize = Screen.width;		
		if (tag != "first") {
			gameObject.GetComponent<SpriteRenderer> ().sprite = awan [Random.Range (0, awan.Length)];	
		}
		if (transform.localPosition.x > 0) {
			direction = -1;
		} else {
			direction = 1;
		}
		//direction = Random.Range (0, 2)*2-1;
		speed = Random.Range (1f, 3f);

	}

	void OnTriggerEnter2D(){
		Destroy (this.gameObject);
	}

	bool played;
	// Update is called once per frame
	void Update () {
		played = GameObject.Find ("Main Camera").GetComponent<gameScript> ().played;
		if (played) {
			transform.Translate (direction * speed * Time.deltaTime, 0, 0);
		}
	}
}
