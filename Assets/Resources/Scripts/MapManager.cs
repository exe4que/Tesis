using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Linq;

public class MapManager : MonoBehaviour {


    Sprite[][] spriteArray;
    string[] files;

    static MapManager _instance;

    public static MapManager instance
    {
        get
        {
            return _instance;
        }
    }
	// Use this for initialization
	void Awake () {
        _instance = this;

        files = Directory.GetFiles(Application.dataPath + "/Tilemaps/", "*.png");
        spriteArray = new Sprite[files.Length][];


        for (int i = 0; i < files.Length; i++)
        {
            files[i] = files[i].Replace(Application.dataPath + "/Tilemaps/", "");
            spriteArray[i] = AssetDatabase.LoadAllAssetsAtPath("Assets/Tilemaps/" + files[i]).Select(x => x as Sprite).Where(x => x != null).ToArray();
        }
	}

    public Sprite AssignSpriteToTileType(bool[] pos, int index)
    {
        //0/
        if (pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & pos[7])
            return spriteArray[index][0];
        //1/
        if (!pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & pos[7])
            return spriteArray[index][0];
        //2/
        if (pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & pos[7])
            return spriteArray[index][0];
        //3/
        if (pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & !pos[7])
            return spriteArray[index][0];
        //4/
        if (pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & pos[7])
            return spriteArray[index][0];
        //5/
        if (!pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & pos[7])
            return spriteArray[index][0];
        //6/
        if (pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & !pos[7])
            return spriteArray[index][0];
        //7/
        if (pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & !pos[7])
            return spriteArray[index][0];
        //8/
        if (!pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & pos[7])
            return spriteArray[index][0];
        //9/
        if (!pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & !pos[7])
            return spriteArray[index][0];
        //10/
        if (pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & pos[7])
            return spriteArray[index][0];
        //11/
        if (!pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & !pos[7])
            return spriteArray[index][0];
        //12/
        if (pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & !pos[7])
            return spriteArray[index][0];
        //13/
        if (!pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & !pos[7])
            return spriteArray[index][0];
        //14/
        if (!pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & pos[7])
            return spriteArray[index][0];
        //15/
        if (!pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & !pos[7])
            return spriteArray[index][0];
        //16/
        if (!pos[1] & pos[3] & pos[4] & pos[5] & pos[6] & pos[7])
            return spriteArray[index][0];
        //17/
        if (pos[0] & pos[1] & pos[3] & !pos[4] & pos[5] & pos[6])
            return spriteArray[index][0];
        //18/
        if (pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & !pos[6])
            return spriteArray[index][0];
        //19/
        if (pos[1] & pos[2] & !pos[3] & pos[4] & pos[6] & pos[7])
            return spriteArray[index][0];
        //20/
        if (!pos[1] & pos[3] & !pos[4] & pos[5] & pos[6])
            return spriteArray[index][0];
        //21/
        if (pos[0] & pos[1] & pos[3] & !pos[4] & !pos[6])
            return spriteArray[index][0];
        //22/
        if (pos[1] & pos[2] & !pos[3] & pos[4] & !pos[6])
            return spriteArray[index][0];
        //23/
        if (!pos[1] & !pos[3] & pos[4] & pos[6] & pos[7])
            return spriteArray[index][0];
        //24/
        if (!pos[1]& pos[3] & pos[5] & !pos[6])
            return spriteArray[index][0];
        //25/
        if (pos[1] & !pos[3] & !pos[5] & pos[6])
            return spriteArray[index][0];
        //26/
        if (!pos[1] & pos[3] & !pos[4] & !pos[6])
            return spriteArray[index][0];
        //27/
        if (pos[1] & !pos[3] & !pos[4] & !pos[6])
            return spriteArray[index][0];
        //28/
        if (!pos[1] & !pos[3] & pos[4] & !pos[6])
            return spriteArray[index][0];
        //29/
        if (!pos[1] & !pos[3] & !pos[4] & pos[6])
            return spriteArray[index][0];
        //30
        if (!pos[1] & !pos[3] & !pos[4] & !pos[6])
            return spriteArray[index][0];
        //31/
        if (!pos[0] & pos[1] & pos[3] & !pos[4] & pos[5] & pos[6])
            return spriteArray[index][0];
        //32/
        if (pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & !pos[6])
            return spriteArray[index][0];
        //33/
        if (pos[1] & pos[2] & !pos[3] & pos[4] & pos[6] & !pos[7])
            return spriteArray[index][0];
        //34/
        if (!pos[1] & pos[3] & pos[4] & !pos[5] & pos[6] & pos[7])
            return spriteArray[index][0];
        //35/
        if (!pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & !pos[6])
            return spriteArray[index][0];
        //36/
        if (pos[1] & !pos[2] & !pos[3] & pos[4] & pos[6] & pos[7])
            return spriteArray[index][0];
        //37/
        if (!pos[1] & pos[3] & pos[4] & pos[5] & pos[6] & !pos[7])
            return spriteArray[index][0];
        //38/
        if (pos[0] & pos[1] & pos[3] & !pos[4] & !pos[5] & pos[6])
            return spriteArray[index][0];
        //39/
        if (!pos[0] & pos[1] & pos[3] & !pos[4] & !pos[6])
            return spriteArray[index][0];
        //40/
        if (pos[1] & !pos[2] & !pos[3] & pos[4] & !pos[6])
            return spriteArray[index][0];
        //41/
        if (!pos[1] & !pos[3] & pos[4] & pos[6] & !pos[7])
            return spriteArray[index][0];
        //42/
        if (!pos[1] & pos[3] & !pos[4] & !pos[5] & pos[6])
            return spriteArray[index][0];
        //43/
        if (!pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & !pos[6])
            return spriteArray[index][0];
        //44/
        if (pos[1] & !pos[2] & !pos[3] & pos[4] & pos[6] & !pos[7])
            return spriteArray[index][0];
        //45/
        if (!pos[1] & pos[3] & pos[4] & !pos[5] & pos[6] & !pos[7])
            return spriteArray[index][0];
        //46/
        if (!pos[0] & pos[1] & pos[3] & !pos[4] & !pos[5] & pos[6])
            return spriteArray[index][0];

        return null;
    }
}
