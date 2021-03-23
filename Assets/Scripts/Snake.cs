using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class Snake : MonoBehaviour {

	public GameObject tailPrefab;
	public GameObject food;
	public Transform rBorder;
	public Transform lBorder;
	public Transform tBorder;
	public Transform bBorder;
	int score = 0;
	private float moveSpeed = 0.2f;
	private float padding = 0.15f;
	[SerializeField]TMP_Text scoreText;
	[SerializeField]public Button upButton;
	[SerializeField] public Button downButton;
	[SerializeField] public Button rightButton;
	[SerializeField] public Button leftButton;
	[SerializeField]private float speed = 0.1f;
	[SerializeField] int scoreValue = 1;

	[Header("Audio")]
	[SerializeField] AudioClip eatSound;
	[SerializeField] [Range(0, 1)] float eatSoundVolume = 0.7f;
	[SerializeField] AudioClip deathSound;
	[SerializeField] [Range(0, 1)] float deathSoundVolume = 0.2f;

	Vector2 vector = new Vector2(0, 0.2f);
	Vector2 moveVector;

	List<Transform> tail = new List<Transform>();

	bool eat = false;
	bool vertical = true;
	bool horizontal = true;
	
	void Start () {
		SpawnFood ();
		InvokeRepeating("Movement", 0.1f, speed);
	
	}
	public void UpButton()
    {
		if(horizontal == true)
        {
			upButton.interactable = false;
			downButton.interactable = false;
			rightButton.interactable = true;
			leftButton.interactable = true;
			vector = new Vector2(0, moveSpeed);
		}
			
	}
	public void DownButton()
	{
		if (horizontal == true)
		{
			upButton.interactable = false;
			downButton.interactable = false;
			rightButton.interactable = true;
			leftButton.interactable = true;
			vector = new Vector2(0, -moveSpeed);
		}
	}
	public void RightButton()
	{
		if (vertical == true)
		{
			upButton.interactable = true;
			downButton.interactable = true;
			rightButton.interactable = false;
			leftButton.interactable = false;
			vector = new Vector2(moveSpeed, 0);
		}
		
	}
	public void LeftButton()
	{
		if (vertical == true)
		{
			upButton.interactable = true;
			downButton.interactable = true;
			rightButton.interactable = false;
			leftButton.interactable = false;
			vector = new Vector2(-moveSpeed, 0);
		}
	}


	void Update () {
		/*if (Input.GetKey (KeyCode.RightArrow) && horizontal) {
			horizontal = false;
			vertical = true;
			vector = new Vector2(0.5f,0);
		} else if (Input.GetKey (KeyCode.UpArrow) && vertical) {
			horizontal = true;
			vertical = false;
			vector = new Vector2(0,0.5f);
		} else if (Input.GetKey (KeyCode.DownArrow) && vertical) {
			horizontal = true;
			vertical = false;
			vector = new Vector2(0,-0.5f);
		} else if (Input.GetKey (KeyCode.LeftArrow) && horizontal) {
			horizontal = false;
			vertical = true;
			vector = new Vector2(-0.5f,0);
		}*/

		moveVector = vector;
		scoreText.text = GetScore().ToString();
	}

	public void SpawnFood() {
		float x = Random.Range (lBorder.position.x + padding, rBorder.position.x - padding);
		float y = Random.Range (bBorder.position.y + padding, tBorder.position.y - padding);
		
		Instantiate (food, new Vector2 (x, y), Quaternion.identity);
	}

	void Movement() {

		Vector2 ta = transform.position;
		transform.Translate(moveVector);
		if (eat) {
			GameObject g =(GameObject)Instantiate(tailPrefab, ta, Quaternion.identity);
			
			tail.Insert(0, g.transform);
			eat = false;
		}
		else if (tail.Count > 0) {
			tail.Last().position = ta;
			tail.Insert(0, tail.Last());
			tail.RemoveAt(tail.Count-1);
		}
		
	}
	public int GetScore()
	{
		return score;
	}

	public void AddToScore(int scoreValue)
	{
		score += scoreValue;
	}

	void OnTriggerEnter2D(Collider2D c) {

		if (c.name.StartsWith("Apple")) {
			eat = true;
			Destroy(c.gameObject);
			SpawnFood();
			AddToScore(scoreValue);
			EatSound();
		}
	}
	public void DeathSound()
    {
		AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
	}
	public void EatSound()
    {
		AudioSource.PlayClipAtPoint(eatSound, Camera.main.transform.position, eatSoundVolume);
	}
}
