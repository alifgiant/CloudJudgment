using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public AudioClip play, over;

	public void playgame(){
		GameObject.Find ("scores").GetComponent<Text> ().enabled = true;
		GameObject.Find ("skull").GetComponent<RawImage> ().enabled = true;
		resetGame ();
	}

	public GameObject goodGuy,badGuy,awanLatar;

	public void resetGame(){
		audio.clip = play;
		audio.Play ();
		GameObject scoreBoard = GameObject.Find ("scores");
		scoreBoard.GetComponent<Text> ().text = "0";
		scoreBoard.SendMessage ("resetScore");
		destroyAllGuy ("bad");
		destroyAllGuy ("good");
		played = true;
		endMenu (false);
	}

	void destroyAllGuy(string tag){
		GameObject [] player = GameObject.FindGameObjectsWithTag (tag);
		foreach (var item in player) {
			//item.SendMessage("destroy")	;
			GameObject.Destroy((GameObject)item);
		}
	}

	public Sprite on, off;

	bool status = true;
	public Sprite setSound(){
		if (status) {
			status = false;
			return off;
		} else {
			status = true;
			return on;
		}
	}

	public bool played = false;
	float timer = 0.01f;
	float scale = 0.0f;

	// Update is called once per frame
	void Update () {
		if (played) {
			if (timer>scale) {
				int place = Random.Range(0,2)*2-1;
				createGuy(awanLatar,place*13.63f,4.05f,0);						
				int select = Random.Range(0,2);
				if (select==0){
					createGuy(badGuy,-13f,-3,0);
				}else {
					createGuy(goodGuy,-13f,-3,0);				
				}
				scale+=Random.Range(1f, 1.6f);
			}	
			timer+=Time.deltaTime;
		}
		else {
			timer = 0.01f;
			scale = 0.0f;
		}
		high = getHighScore ();
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
	}

	void createGuy(GameObject guy, float x, float y, float z){
		//Instantiate (guy, new Vector3 (x, y, z), Quaternion.identity);
		Instantiate (guy, new Vector3 (x, y, z), Quaternion.identity);
	}

	public int high;
	int getHighScore(){
		return PlayerPrefs.GetInt("highscore", 0);   
	}

	void StoreHighscore(int newHighscore)
	{
		int oldHighscore = getHighScore();    
		if(newHighscore > oldHighscore)
			PlayerPrefs.SetInt("highscore", newHighscore);
	}

	void gameover(){
		played = false;
		int _high = GameObject.Find ("scores").GetComponent<score> ().nowscore;
		StoreHighscore (_high);
		audio.clip = over;
		audio.Play ();
		endMenu (true);
	}

	void endMenu(bool stat){
		GameObject reset =  GameObject.Find("reset");
		reset.GetComponent<Image> ().enabled = stat;
		reset.GetComponent<Button> ().enabled = stat;

		GameObject share = GameObject.Find ("share");
		share.GetComponent<Image> ().enabled = stat;
		share.GetComponent<Button> ().enabled = stat;

		GameObject rate = GameObject.Find("rate");
		rate.GetComponent<Image> ().enabled = stat;
		rate.GetComponent<Button> ().enabled = stat;

		GameObject highScore = GameObject.Find ("highScores");
		highScore.GetComponent<Text> ().enabled = stat;
		if (stat) {
			highScore.GetComponent<Text> ().text = getHighScore ().ToString ();	
		}
	}	

	const string AppId = "848811441848696";
	const string ShareUrl = "https://www.facebook.com/dialog/feed";

	public void shareBut(){
		//AndroidJNI.
		Share ("http://bit.ly/1Aojikg", "http://www.mediafire.com/convkey/1458/lo22pct7n09fruczg.jpg", "Hey let's play cloud's judgment", "A new game by Rantau tak pulang", "My high score is " + high.ToString(), "https://www.facebook.com");

	}

	void Share(string link, string pictureLink, string name,string caption, string description, string redirectUri)
	{
		Application.OpenURL (ShareUrl+"?app_id="+ AppId +"&picture="+WWW.EscapeURL(pictureLink)+"&description="+ WWW.EscapeURL(description) +"&link=" + WWW.EscapeURL( link )+"&name=" + WWW.EscapeURL(name) +"&redirect_uri="+ WWW.EscapeURL(redirectUri));
	}

}
