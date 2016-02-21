using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;

public class MapManager : MonoBehaviour
{


    Sprite[][] spriteArray;
    Texture[] files;
    GameObject[] tileArray;
    private static Vector2 gridSize = new Vector2(0.64f, 0.64f);

    private static MapManager _instance;

    public static MapManager instance
    {
        get { return _instance; }
    }
    // Use this for initialization
    void Awake()
    {
        //this.files = Directory.GetFiles(Application.dataPath + "/Resources/Tilemaps/", "*.png");
        this.files = Resources.LoadAll<Texture>("Tilemaps");
        this.spriteArray = new Sprite[files.Length][];
        this.tileArray = GameObject.FindGameObjectsWithTag("Tile") as GameObject[];

        Sprite[] auxArray = Resources.LoadAll<Sprite>("Tilemaps");
        for (int i = 0; i < files.Length; i++)
        {
            //this.files[i] = files[i].Replace(Application.dataPath + "/Resources/Tilemaps/", "");
            //this.files[i] = files[i].Replace(".png", "");
            //this.spriteArray[i] = AssetDatabase.LoadAllAssetsAtPath("Assets/Tilemaps/" + files[i]).Select(x => x as Sprite).Where(x => x != null).ToArray();
            //Debug.Log("files[i] = " + files[i] + ", " + auxArray[0].name);
            this.spriteArray[i] = auxArray.Select(x => x as Sprite).Where(x => x.name.StartsWith(files[i].name)).ToArray();
        }
        _instance = this;
    }

    public void OnTileDied(Vector3 pos, bool[] nearbyTiles)
    {
        for (int i = 0; i < tileArray.Length; i++)
        {
            if (nearbyTiles[0])
            {
                if (Mathf.Approximately(tileArray[i].transform.position.x, pos.x - gridSize.x) && Mathf.Approximately(tileArray[i].transform.position.y, pos.y + gridSize.y) && Mathf.Approximately(tileArray[i].transform.position.z, pos.z))
                    UpdateTile(i, 7);
            }
            if (nearbyTiles[1])
            {
                if (Mathf.Approximately(tileArray[i].transform.position.x, pos.x) && Mathf.Approximately(tileArray[i].transform.position.y, pos.y + gridSize.y) && Mathf.Approximately(tileArray[i].transform.position.z, pos.z))
                    UpdateTile(i, 6);
            }
            if (nearbyTiles[2])
            {
                if (Mathf.Approximately(tileArray[i].transform.position.x, pos.x + gridSize.x) && Mathf.Approximately(tileArray[i].transform.position.y, pos.y + gridSize.y) && Mathf.Approximately(tileArray[i].transform.position.z, pos.z))
                    UpdateTile(i, 5);
            }
            if (nearbyTiles[3])
            {
                if (Mathf.Approximately(tileArray[i].transform.position.x, pos.x - gridSize.x) && Mathf.Approximately(tileArray[i].transform.position.y, pos.y) && Mathf.Approximately(tileArray[i].transform.position.z, pos.z))
                    UpdateTile(i, 4);
            }
            if (nearbyTiles[4])
            {
                if (Mathf.Approximately(tileArray[i].transform.position.x, pos.x + gridSize.x) && Mathf.Approximately(tileArray[i].transform.position.y, pos.y) && Mathf.Approximately(tileArray[i].transform.position.z, pos.z))
                    UpdateTile(i, 3);
            }
            if (nearbyTiles[5])
            {
                if (Mathf.Approximately(tileArray[i].transform.position.x, pos.x - gridSize.x) && Mathf.Approximately(tileArray[i].transform.position.y, pos.y - gridSize.y) && Mathf.Approximately(tileArray[i].transform.position.z, pos.z))
                    UpdateTile(i, 2);
            }
            if (nearbyTiles[6])
            {
                if (Mathf.Approximately(tileArray[i].transform.position.x, pos.x) && Mathf.Approximately(tileArray[i].transform.position.y, pos.y - gridSize.y) && Mathf.Approximately(tileArray[i].transform.position.z, pos.z))
                    UpdateTile(i, 1);
            }
            if (nearbyTiles[7])
            {
                if (Mathf.Approximately(tileArray[i].transform.position.x, pos.x + gridSize.x) && Mathf.Approximately(tileArray[i].transform.position.y, pos.y - gridSize.y) && Mathf.Approximately(tileArray[i].transform.position.z, pos.z))
                    UpdateTile(i, 0);
            }
        }
    }

    private void UpdateTile(int index, int nearbyIndexDeletion)
    {
        TileBehaviour tile = tileArray[index].GetComponent<TileBehaviour>();
        tile.DeleteNearbyTile(nearbyIndexDeletion);
        tileArray[index].GetComponent<SpriteRenderer>().sprite = AssignSpriteToTileType(tile.nearbyTiles, tile.index);
    }

    public Sprite AssignSpriteToTileType(bool[] pos, int index)
    {
        //0/
        if (pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & pos[7])
            return spriteArray[index][0];
        //1/
        if (!pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & pos[7])
            return spriteArray[index][1];
        //2/
        if (pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & pos[7])
            return spriteArray[index][2];
        //3/
        if (pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & !pos[7])
            return spriteArray[index][3];
        //4/
        if (pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & pos[7])
            return spriteArray[index][4];
        //5/
        if (!pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & pos[7])
            return spriteArray[index][5];
        //6/
        if (pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & !pos[7])
            return spriteArray[index][6];
        //7/
        if (pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & !pos[7])
            return spriteArray[index][7];
        //8/
        if (!pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & pos[7])
            return spriteArray[index][8];
        //9/
        if (!pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & !pos[7])
            return spriteArray[index][9];
        //10/
        if (pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & pos[7])
            return spriteArray[index][10];
        //11/
        if (!pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & pos[5] & pos[6] & !pos[7])
            return spriteArray[index][11];
        //12/
        if (pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & !pos[7])
            return spriteArray[index][12];
        //13/
        if (!pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & !pos[7])
            return spriteArray[index][13];
        //14/
        if (!pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & pos[7])
            return spriteArray[index][14];
        //15/
        if (!pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & !pos[5] & pos[6] & !pos[7])
            return spriteArray[index][15];
        //16/
        if (!pos[1] & pos[3] & pos[4] & pos[5] & pos[6] & pos[7])
            return spriteArray[index][16];
        //17/
        if (pos[0] & pos[1] & pos[3] & !pos[4] & pos[5] & pos[6])
            return spriteArray[index][17];
        //18/
        if (pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & !pos[6])
            return spriteArray[index][18];
        //19/
        if (pos[1] & pos[2] & !pos[3] & pos[4] & pos[6] & pos[7])
            return spriteArray[index][19];
        //20/
        if (!pos[1] & pos[3] & !pos[4] & pos[5] & pos[6])
            return spriteArray[index][20];
        //21/
        if (pos[0] & pos[1] & pos[3] & !pos[4] & !pos[6])
            return spriteArray[index][21];
        //22/
        if (pos[1] & pos[2] & !pos[3] & pos[4] & !pos[6])
            return spriteArray[index][22];
        //23/
        if (!pos[1] & !pos[3] & pos[4] & pos[6] & pos[7])
            return spriteArray[index][23];
        //24/
        if (!pos[1] & pos[3] & pos[4] & !pos[6])
            return spriteArray[index][24];
        //25/
        if (pos[1] & !pos[3] & !pos[4] & pos[6])
            return spriteArray[index][25];
        //26/
        if (!pos[1] & pos[3] & !pos[4] & !pos[6])
            return spriteArray[index][26];
        //27/
        if (pos[1] & !pos[3] & !pos[4] & !pos[6])
            return spriteArray[index][27];
        //28/
        if (!pos[1] & !pos[3] & pos[4] & !pos[6])
            return spriteArray[index][28];
        //29/
        if (!pos[1] & !pos[3] & !pos[4] & pos[6])
            return spriteArray[index][29];
        //30
        if (!pos[1] & !pos[3] & !pos[4] & !pos[6])
            return spriteArray[index][30];
        //31/
        if (!pos[0] & pos[1] & pos[3] & !pos[4] & pos[5] & pos[6])
            return spriteArray[index][31];
        //32/
        if (pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & !pos[6])
            return spriteArray[index][32];
        //33/
        if (pos[1] & pos[2] & !pos[3] & pos[4] & pos[6] & !pos[7])
            return spriteArray[index][33];
        //34/
        if (!pos[1] & pos[3] & pos[4] & !pos[5] & pos[6] & pos[7])
            return spriteArray[index][34];
        //35/
        if (!pos[0] & pos[1] & pos[2] & pos[3] & pos[4] & !pos[6])
            return spriteArray[index][35];
        //36/
        if (pos[1] & !pos[2] & !pos[3] & pos[4] & pos[6] & pos[7])
            return spriteArray[index][36];
        //37/
        if (!pos[1] & pos[3] & pos[4] & pos[5] & pos[6] & !pos[7])
            return spriteArray[index][37];
        //38/
        if (pos[0] & pos[1] & pos[3] & !pos[4] & !pos[5] & pos[6])
            return spriteArray[index][38];
        //39/
        if (!pos[0] & pos[1] & pos[3] & !pos[4] & !pos[6])
            return spriteArray[index][39];
        //40/
        if (pos[1] & !pos[2] & !pos[3] & pos[4] & !pos[6])
            return spriteArray[index][40];
        //41/
        if (!pos[1] & !pos[3] & pos[4] & pos[6] & !pos[7])
            return spriteArray[index][41];
        //42/
        if (!pos[1] & pos[3] & !pos[4] & !pos[5] & pos[6])
            return spriteArray[index][42];
        //43/
        if (!pos[0] & pos[1] & !pos[2] & pos[3] & pos[4] & !pos[6])
            return spriteArray[index][43];
        //44/
        if (pos[1] & !pos[2] & !pos[3] & pos[4] & pos[6] & !pos[7])
            return spriteArray[index][44];
        //45/
        if (!pos[1] & pos[3] & pos[4] & !pos[5] & pos[6] & !pos[7])
            return spriteArray[index][45];
        //46/
        if (!pos[0] & pos[1] & pos[3] & !pos[4] & !pos[5] & pos[6])
            return spriteArray[index][46];

        return null;
    }
}
