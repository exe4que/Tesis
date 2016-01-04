using UnityEngine;
using System.Collections;

[RequireComponent (typeof(translateAndLookAt))]
[RequireComponent (typeof(CircleCollider2D))]
public class BotMovementController : MonoBehaviour
{

	public Vector3 botDirection;
    Vector3 lastDirection;
	translateAndLookAt bot;

	void Awake ()
	{
		this.bot = this.GetComponent<translateAndLookAt> ();
		this.botDirection = Vector2.right * (int)Random.Range (-1, 2);
	}

	void Update ()
	{
		if ((int)Random.Range (0, (1 / Time.deltaTime) * 5) == 0)
			RandomizeDirection ();

		if (bot.isColliding) {
			RandomizeDirection ();
		}
        this.bot.inputAxis = this.botDirection;
		
	}

	void RandomizeDirection ()
	{
        lastDirection = botDirection;
        do
        {
			botDirection = new Vector3 ((int)Random.Range (-1, 2), (int)Random.Range (-1, 2));
		} while (((int)botDirection.x == 0 && (int)botDirection.y == 0) || botDirection == lastDirection);
	}
}
