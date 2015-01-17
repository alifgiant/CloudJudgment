using UnityEngine;
using System.Collections;

public class dragCloud : MonoBehaviour {
	
	//Fraction defined by user that will limit the touch area
	public int frac;
	public Sprite putih,gelap;

	// Use this for initialization
	void Start () {

	}

	private float dist;
	private Transform toDrag;
	private bool dragging = false;
	private Vector3 offset;
	private Vector3 v3;

	private bool played;
	// Update is called once per frame
	void Update () {

		played = GameObject.Find ("Main Camera").GetComponent<gameScript> ().played;
		if (played) {
			//android platform
			if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer){
				if (Input.GetKeyDown(KeyCode.Escape)) {
					Application.Quit(); 
				}
				
				if(Input.touchCount > 0) {
					if(Input.GetTouch(0).phase == TouchPhase.Began){
						checkTouch(Input.GetTouch(0).position.x);
					}
					if(Input.GetTouch(0).phase == TouchPhase.Moved){
						if (dragging) {
							checkDrag(Input.GetTouch(0).position.x);
						}
					}
					if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled) {					
						shootLightning ();					
					}
				}
			}else{
			//If running game in editor
			//#if UNITY_EDITOR
			//If mouse button 0 is down
			if (Input.GetMouseButton (0)) {
				//Cache mouse position
				/*Vector2 mouseCache = Input.mousePosition; 
				 * touched = true;*/
				if (Input.GetMouseButtonDown (0)) {
					checkTouch(Input.mousePosition.x);
				}
				if (Input.GetMouseButton (0)) {
					if (dragging) {
						checkDrag(Input.mousePosition.x);						
					}
				}
			}
			if (Input.GetMouseButtonUp (0)) {					
					shootLightning ();					
			}
			//Debug.LogError(dragging);
			}
			//#endif


		}
	}
	RuntimePlatform platform = Application.platform;
	//bool shooted;

	void shootLightning(){
		dragging = false;
		Transform light = transform.FindChild ("petir");
		audio.Play ();
		light.GetComponent<SpriteRenderer> ().enabled = true;
		light.GetComponent<BoxCollider2D> ().enabled = true;
		light.SendMessage("startAnimated");
		gameObject.GetComponent<SpriteRenderer> ().sprite = putih;
		//shooted = true;
	}

	void checkTouch(float posX){
//		jika perlu ada hit
//		Ray ray = Camera.main.ScreenPointToRay(touch.position);
//		RaycastHit hit = new RaycastHit();
//		if (Physics.Raycast(ray, out hit, maxPickingDistance)) 
//		{ 
//			pickedObject = hit.transform;
//			startPos = touch.position;
//		} 
//		else
//		{
//			pickedObject = null;
//		}
//		atau
//		void OnMouseDown() {
//			plane.SetNormalAndPosition(Camera.main.transform.forward, transform.position);
//			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//			float dist;
//			plane.Raycast (ray, out dist);
//			v3Offset = transform.position - ray.GetPoint (dist);         
//		}
//		
//		void OnMouseDrag() {
//			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//			float dist;
//			plane.Raycast (ray, out dist);
//			Vector3 v3Pos = ray.GetPoint (dist);
//			transform.position = v3Pos + v3Offset;    
//		}
		toDrag = transform;
		dist = toDrag.position.z - Camera.main.transform.position.z;
		v3 = new Vector3 (posX, 0, dist);
		v3 = Camera.main.ScreenToWorldPoint (v3);
		offset = toDrag.position - v3;
		dragging = true;
		gameObject.GetComponent<SpriteRenderer> ().sprite = gelap;
	}

	void checkDrag(float posX){
		v3 = new Vector3 (posX, 0, dist);
		v3 = Camera.main.ScreenToWorldPoint (v3);
		toDrag.position = v3 + offset;
	}

	//	RuntimePlatform platform = Application.platform;
	/*  ini jika perlu cek objek
	void checkTouch(Vector3 pos){
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		RaycastHit hit = Physics2D.OverlapPoint(touchPos);
		
		if(hit){
			Debug.Log(hit.transform.gameObject.name);
			hit.transform.gameObject.SendMessage("Clicked",0,SendMessageOptions.DontRequireReceiver);
		}
	}
	*/
}
