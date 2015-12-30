using UnityEngine;
using System.Collections;

[RequireComponent (typeof(translateAndLookAt))]
[RequireComponent (typeof(CircleCollider2D))]
public class BotMovementController : MonoBehaviour
{

	public Vector3 botDirection;
	translateAndLookAt bot;

	void Awake ()
	{
		bot = this.GetComponent<translateAndLookAt> ();
		botDirection = Vector2.right * (int)Random.Range (-1, 2);
	}

	void Update ()
	{
		if ((int)Random.Range (0, (1 / Time.deltaTime) * 5) == 0)
			RandomizeDirection ();

		if (bot.isColliding) {
			RandomizeDirection ();
		}
		if (bot.isBot)
			bot.inputAxis = botDirection;
	}

	void RandomizeDirection ()
	{
		do {
			botDirection = new Vector3 ((int)Random.Range (-1, 2), (int)Random.Range (-1, 2));
		} while ((int)botDirection.x == 0 & (int)botDirection.x == (int)botDirection.y);
	}
}
