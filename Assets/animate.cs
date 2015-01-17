using UnityEngine;
using System.Collections;

public class animate : MonoBehaviour {
	
	public Sprite[] source;
	public Sprite[] tersambar;
	
	float speed;
	// Use this for initialization
	void Start () {
		int level = (GameObject.Find ("scores").GetComponent<score> ().nowscore/7)+1;
		speed = Random.Range (5, 14)*level;
		normal = true;
		beforeDestroy = 19;
	}
	
	int pointer = 0;
	int pointerPetir = 0;
	public float timer = 0f;

	float lastTime = 0.05f;

	bool played;

	void OnTriggerEnter2D(Collider2D other){
		//Debug.Log("collide");
		if (other.gameObject.tag == "petir") {
			if (this.gameObject.tag == "bad") {
				//GameObject.Destroy (this.gameObject);
				normal = false;
				audio.Play ();
				GameObject.Find ("scores").SendMessage ("addscore", 1);
			} else {
				GameObject.Find ("Main Camera").SendMessage ("gameover");
			}
		} else if (other.gameObject.tag == "end") {
			if (this.gameObject.tag == "bad") {
				GameObject.Find ("Main Camera").SendMessage ("gameover");
			} else {
				GameObject.Destroy (this.gameObject);
				//normal = false;
			}
		} else if (other.gameObject.tag == "coution" && this.gameObject.tag == "bad") {
			transform.FindChild("attention").GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	bool normal;

	void OnCollisionEnter2D(Collision2D coll) {
		//if (coll.gameObject.tag == "Enemy")
		//GameObject.Destroy (this.gameObject);		
		//Debug.Log("collide");
	}

	int beforeDestroy;
	// Update is called once per frame
	void FixedUpdate () {
		played = GameObject.Find ("Main Camera").GetComponent<gameScript> ().played;
		if (played) {
			timer += Time.fixedDeltaTime;
			if (Round(timer,3)>lastTime){
				if (normal){
					gameObject.GetComponent<SpriteRenderer>().sprite = source[pointer];
					transform.Translate (speed * Time.deltaTime, 0, 0);
					pointer+=1;
					if (pointer>11) {
						pointer=0;
					}
				}else{
					gameObject.GetComponent<SpriteRenderer>().sprite = tersambar[pointerPetir];								
					pointerPetir+=1;
					if (pointerPetir>2) {
						pointerPetir=0;
					}
					beforeDestroy -=1;
				}
				lastTime = Round(timer,2)+0.055f;
			}			
		}
		if (beforeDestroy==0) {
			GameObject.Destroy(this.gameObject);
		}
	}

	void destroyThisObject(){
		GameObject.Destroy (gameObject);
	}

	static float Round(float value, int digits)
	{
		float mult = Mathf.Pow(10.0f, (float)digits);
		return Mathf.Round(value * mult) / mult;
	}
}
