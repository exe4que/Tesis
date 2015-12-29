using UnityEngine;
using System.Collections;

[RequireComponent (typeof (translateAndLookAt))]
public class BotMovementController : MonoBehaviour {

	public Vector3 botDirection;
	translateAndLookAt bot;

	void Awake(){
		bot = this.GetComponent<translateAndLookAt>();
		botDirection = Vector2.right * (int)Random.Range(-1, 2);
	}

	void Update(){
		if(bot.isColliding){
			do{
			botDirection = new Vector3((int)Random.Range(-1,2), (int)Random.Range(-1,2));
			}while((int)botDirection.x == 0 & (int)botDirection.x == (int)botDirection.y);
		}
	}

}
