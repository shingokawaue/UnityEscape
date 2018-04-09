using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//定数定義
	public const int WALL_FRONT = 1;
	public const int WALL_RIGHT = 2;
	public const int WALL_BACK = 3;
	public const int WALL_LEFT = 4;

	public const int COLOR_GREEN  = 0 ;
	public const int COLOR_RED  = 1 ;
	public const int COLOR_BLUE  = 2 ;
	public const int COLOR_WHITE  = 3 ;

	public GameObject panelWalls;

	public GameObject buttonMessage;
	public GameObject buttonMessageText;

	public GameObject buttonHammer;
	public GameObject buttonHammerIcon;

	public GameObject buttonKey;
	public GameObject imageKeyIcon;

	public GameObject buttonPig;

	public GameObject[] buttonLamp = new GameObject[3];
	public Sprite[] buttonPicture = new Sprite[4];
	public Sprite hammerPicture;
	public Sprite KeyPicture;

	private int wallNo;//現在向いている方向
	private bool doesHaveHammer;
	private bool doesHaveKey;
	private int[] buttonColor = new int[3];

	void Start () {
		wallNo = WALL_FRONT;
		doesHaveHammer = false;
		doesHaveKey = false;

		buttonColor [0] = COLOR_GREEN;
		buttonColor [1] = COLOR_RED;
		buttonColor [2] = COLOR_BLUE;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PushButtonBox (){
		if (doesHaveKey == false) {
			DisplayMessage ("鍵がかかっている。");
		} else {
			SceneManager.LoadScene ("ClearScene");
		}
	}

	public void PushButtonPig(){
		if (doesHaveHammer == false) {
			DisplayMessage ("素手では割れない。");
		} else {
			DisplayMessage ("貯金箱が割れて中から鍵が出てきた。");
			buttonPig.SetActive (false);
			buttonKey.SetActive (true);
			imageKeyIcon.GetComponent<Image> ().sprite = KeyPicture;

			doesHaveKey = true;
		}
	}

	public void PushButtonKey(){
		buttonKey.SetActive (false);
	}

	public void PushButtonLamp1(){
		ChangeButtonColor (0);
	}
	public void PushButtonLamp2(){
		ChangeButtonColor (1);
	}
	public void PushButtonLamp3(){
		ChangeButtonColor (2);
	}
		
	public void PushButtonMemo(){
		DisplayMessage ("エッフェル塔と書いてある。");
	}

	public void PushButtonHammer(){
		buttonHammer.SetActive (false);
	}

	public void PushButtonMessage(){
		buttonMessage.SetActive (false);
	}

	public void PushButtonLeft () {
		--wallNo;
		if (wallNo < WALL_FRONT)
			wallNo = WALL_LEFT;

		DisplayWall ();
		ClearButtons ();
	}

	public void PushButtonRight () {
		++wallNo;
		if (wallNo > WALL_LEFT)
			wallNo = WALL_FRONT;

		DisplayWall ();
		ClearButtons ();
	}


	void DisplayWall(){
		switch (wallNo) {
		case WALL_FRONT:
			panelWalls.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
			break;
		case WALL_RIGHT:
			panelWalls.transform.localPosition = new Vector3 (-1000f, 0.0f, 0.0f);
			break;
		case WALL_BACK:
			panelWalls.transform.localPosition = new Vector3 (-2000f, 0.0f, 0.0f);
			break;
		case WALL_LEFT:
			panelWalls.transform.localPosition = new Vector3 (-3000f, 0.0f, 0.0f);
			break;
		}
	}

	void DisplayMessage(string mes){
		buttonMessage.SetActive (true);
		buttonMessageText.GetComponent<Text> ().text = mes;
	}

	void ChangeButtonColor (int buttonNo){
		++buttonColor [buttonNo];

		if (buttonColor [buttonNo] > COLOR_WHITE)
			buttonColor [buttonNo] = COLOR_GREEN;

		buttonLamp [buttonNo].GetComponent<Image> ().sprite = //ボタンの画像変更
			buttonPicture [buttonColor [buttonNo]];


		if ((buttonColor [0] == COLOR_BLUE) &&
		    (buttonColor [1] == COLOR_WHITE) &&
		    (buttonColor [2] == COLOR_RED)) {
			if (doesHaveHammer == false) {//色があっていてハンマ持っていなければ手に入れる。
				DisplayMessage ("金庫の中にトンカチが入っていた。");
				buttonHammerIcon.GetComponent<Image> ().sprite = hammerPicture;

				buttonHammer.SetActive (true);
				doesHaveHammer = true;
			}

		}
	}


	void ClearButtons(){
			buttonHammer.SetActive(false);
		buttonKey.SetActive (false);
		buttonMessage.SetActive (false);
	
	}



}
