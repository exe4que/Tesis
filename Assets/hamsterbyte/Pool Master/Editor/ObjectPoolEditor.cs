/*

**************************************
************ POOL MASTER *************
**************************************
______________________________________

VERSION: 2.0
FILE:    OBJECTPOOLEDITOR.CS
AUTHOR:  CODY JOHNSON
COMPANY: HAMSTERBYTE, LLC
EMAIL:   HAMSTERBYTELLC@GMAIL.COM
WEBSITE: WWW.HAMSTERBYTE.COM
SUPPORT: WWW.HAMSTERBYTE.COM/POOL-MASTER

COPYRIGHT © 2014-2015 HAMSTERBYTE, LLC
ALL RIGHTS RESERVED

*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using hamsterbyte.PoolMaster;

[System.Serializable]
[CustomEditor(typeof(ObjectPool))]

#pragma warning disable 0219

public class ObjectPoolEditor : Editor {
	//Version info variables
	private static string version = "2.1";
	private static string unityVersion = "4.6+";
	
	public static bool showStatistics = false;
	public static List<bool> foldouts = new List<bool>();
	public static bool showOptions = false;
	public ObjectPool oPool;
	public int poolIndex;
	
	private Color _gray;
	
	[MenuItem ("GameObject/Pool Master/New Master Pool", false, 0)]
	public static void CreateObjectPool(){
		if(Object.FindObjectOfType<ObjectPool>() == null){
			GameObject gObj = new GameObject("MASTER POOL");
			gObj.AddComponent<ObjectPool>();
		} else {
			EditorUtility.DisplayDialog("Cannot Create New Master Pool", "A master pool already exists in this scene! You may have only one master pool in a scene.", "Okay");
		}
	}
	
	[MenuItem ("GameObject/Pool Master/Version Info", false, 50)]
	public static void VersionInfo(){
		EditorUtility.DisplayDialog("Pool Master - Version Info", 
		                               "Version: " + version + System.Environment.NewLine +
		                               "Works With Unity: " + unityVersion + "+" + System.Environment.NewLine +
		                               "Copyright© 2014-2015 Hamsterbyte, LLC" + System.Environment.NewLine +
		                               "All Rights Reserved.", "Ok", "");
	}
	
	[MenuItem ("GameObject/Pool Master/Documentation", false, 51)]
	public static void LaunchSupport(){
		Application.OpenURL("www.hamsterbyte.com/pool-master");
	}
	
	[MenuItem ("GameObject/Pool Master/Developer Website", false, 52)]
	public static void LaunchDeveloperWebsite(){
		Application.OpenURL("www.hamsterbyte.com");
	}
	
	
	
	public override void OnInspectorGUI(){
		GUILayoutOption[] options = { GUILayout.ExpandWidth(true),};
		if(EditorGUIUtility.isProSkin){
			_gray = Color.gray;
		} else {
			_gray = Color.white;
		}
		
		oPool = (ObjectPool)target;
		EditorGUILayout.BeginVertical(GUILayout.MaxWidth(350));
		int totalUnique = 0;
		int totalObjects = 0;
		if(oPool != null){
			for(int i = 0; i < oPool.pools.Count; i++){
				foreach(int b in oPool.pools[i].bufferAmount){
					totalObjects += b;
				}
				foreach(GameObject g in oPool.pools[i].uniquePool){
					totalUnique += 1;
				}
				
			}
		}
		GUI.backgroundColor = _gray;
		EditorGUILayout.BeginVertical("button", options);
		GUI.backgroundColor = new Color(0, 100, 255);
		EditorGUILayout.BeginHorizontal("button", options);
		GUILayout.FlexibleSpace();
		EditorGUILayout.LabelField("POOL MASTER v" + version , GUILayout.MaxWidth(115));
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		GUI.backgroundColor = _gray;
		if(oPool == null){
			return;
		} else {
			EditorGUILayout.BeginVertical("button", options);
			EditorGUILayout.BeginHorizontal("button", options);
			EditorGUILayout.LabelField("Pools: " + oPool.pools.Count.ToString(), EditorStyles.miniLabel, GUILayout.MaxWidth(60));
			GUILayout.FlexibleSpace();
			EditorGUILayout.LabelField("Objects: " + totalUnique, EditorStyles.miniLabel, GUILayout.MaxWidth(70));
			GUILayout.FlexibleSpace();
			if(!oPool.smartBuffering){
				EditorGUILayout.LabelField("Total: " + totalObjects, EditorStyles.miniLabel, GUILayout.MaxWidth(100));
			} else {
				EditorGUILayout.LabelField("Total: Varies", EditorStyles.miniLabel, GUILayout.MaxWidth(100));
			}	
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginVertical("button", options);
			EditorGUILayout.BeginHorizontal(options);
			EditorGUILayout.LabelField("Show Options");
			GUILayout.FlexibleSpace();
			GUI.backgroundColor = Color.green ; //new Color(0, 100, 255)
			showOptions = EditorGUILayout.Toggle(showOptions, "button", new GUILayoutOption[]{GUILayout.MaxWidth(20), GUILayout.MinWidth(20)}); 
			GUI.backgroundColor = _gray;
			EditorGUILayout.EndHorizontal();
			if(showOptions){
				//SMART BUFFERING BLOCK
				EditorGUILayout.BeginVertical("button", options);
				EditorGUILayout.BeginHorizontal("button", options);
				EditorGUILayout.LabelField("Smart Buffering");
				GUILayout.FlexibleSpace();
				oPool.smartBuffering = EditorGUILayout.Toggle(oPool.smartBuffering, GUILayout.MaxWidth(15));
				EditorGUILayout.EndHorizontal();
				if(oPool.smartBuffering) {
					EditorGUILayout.BeginHorizontal("button", options);
					oPool.smartBufferMax = EditorGUILayout.IntField("Max: ", Mathf.Clamp(oPool.smartBufferMax, 0, int.MaxValue));
					EditorGUILayout.EndHorizontal();
					GUILayout.Space(5);
					EditorGUILayout.LabelField("Smart Buffering will instantiate more objects from the pool if they are required; eliminates null references." +
						System.Environment.NewLine +
					    System.Environment.NewLine +
						"*Instantiation will stop at the max buffer.", EditorStyles.wordWrappedLabel);
					
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space(5);
				
				//DEBUGGING BLOCK
				EditorGUILayout.BeginVertical("button", options);
				EditorGUILayout.BeginHorizontal("button", options);
				EditorGUILayout.LabelField("Debug Mode");
				GUILayout.FlexibleSpace();
				oPool.showDebugInfo = EditorGUILayout.Toggle(oPool.showDebugInfo, GUILayout.MaxWidth(15));
				EditorGUILayout.EndHorizontal();
				if(oPool.showDebugInfo) {
					EditorGUILayout.BeginHorizontal("button", options);
					EditorGUILayout.LabelField("Enhanced Debugging");
					GUILayout.FlexibleSpace();
					oPool.showAllDebugInfo = EditorGUILayout.Toggle(oPool.showAllDebugInfo, GUILayout.MaxWidth(15));
					EditorGUILayout.EndHorizontal();
					GUILayout.Space(5);
					EditorGUILayout.LabelField("Debug mode logs errors only.");
					if(oPool.showAllDebugInfo)
						EditorGUILayout.LabelField("Enhanced degugging logs ALL activity.");
					EditorGUILayout.LabelField("*Debugging may affect performance.");
				} else {
				if(oPool.showAllDebugInfo){
					oPool.showAllDebugInfo = false;
				}
			}
				EditorGUILayout.EndVertical();
				GUILayout.Space(5);
			}
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
		}
		if(oPool.pools.Count > 0){
			if(foldouts.Count <=0){
				foreach(Pool p in oPool.pools){
					foldouts.Add(false);
				}
			}
			for(int i = 0; i < oPool.pools.Count; i++){				
				EditorGUILayout.BeginVertical("button", GUILayout.MaxWidth(320));
				GUILayout.BeginHorizontal("button", options);
				if(!Application.isPlaying){
					GUI.backgroundColor = Color.red;
					if(GUILayout.Button("X", GUILayout.Width(20))){
						if(EditorUtility.DisplayDialog("Delete " + oPool.pools[i].name + "?", "Are you sure you want to delete " + oPool.pools[i].name + "? This action cannot be undone.", "Delete Pool", "Cancel")){
							if(i == 0 && oPool.pools.Count == 1)
								poolIndex = 0;
							oPool.pools.RemoveAt(i);
						}
						
					}
				}
				if(i < oPool.pools.Count){
					GUI.backgroundColor = new Color(0, 100, 255);
					EditorGUILayout.LabelField(oPool.pools[i].name, options);
					foldouts[i] = EditorGUILayout.Toggle(foldouts[i], "button", new GUILayoutOption[]{GUILayout.MaxWidth(20), GUILayout.MinWidth(20)});
					GUI.backgroundColor = _gray;
				}
				GUILayout.EndHorizontal();
				if(foldouts[i]){
					EditorGUILayout.BeginHorizontal("box", options);
					if(i < oPool.pools.Count){
						if(oPool.pools[i].uniquePool != null){
							if(oPool.pools[i].uniquePool.Count != 0){
								int totalItems = oPool.pools[i].bufferAmount.Take(oPool.pools[i].bufferAmount.Count).Sum ();
								EditorGUILayout.LabelField("Unique: " + oPool.pools[i].uniquePool.Count.ToString(), GUILayout.MaxWidth(100));
								EditorGUILayout.LabelField("Total: " + totalItems.ToString(), GUILayout.MaxWidth(100));
								
							} else {
								EditorGUILayout.LabelField("Unique: 0", GUILayout.MaxWidth(100));
								EditorGUILayout.LabelField("Total: 0", GUILayout.MaxWidth(100));
							}
							EditorGUILayout.EndHorizontal();
							if(!Application.isPlaying){
								EditorGUILayout.BeginVertical("box", options);
								if(oPool.pools.FindAll(a => a.name == oPool.pools[i].name).Count > 1){
									oPool.pools[i].name = oPool.pools[i].name + "X";
									EditorUtility.SetDirty(oPool);
								}
								oPool.pools[i].name = EditorGUILayout.TextField("Pool Name: ", oPool.pools[i].name, options);
								oPool.pools[i].maxBuffer = Mathf.Clamp(EditorGUILayout.IntField("Local Buffer: ", oPool.pools[i].maxBuffer, options), 1, int.MaxValue);
								oPool.pools[i].preload = EditorGUILayout.Toggle("Load On Start?", oPool.pools[i].preload, options);
								GUILayout.Space(5);
								EditorGUILayout.EndVertical();
							}
							
							
							if(oPool.pools[i].uniquePool.Count != 0){
								EditorGUILayout.BeginHorizontal("textfield", GUILayout.MaxWidth(320));
								if(!Application.isPlaying){
									GUI.backgroundColor = Color.black;
									GUILayout.Space(10);
									EditorGUILayout.LabelField("Clamp", GUILayout.Width(40));
									EditorGUILayout.LabelField("Buffer", GUILayout.Width(40));
									EditorGUILayout.LabelField("Object", options);
									GUI.backgroundColor = _gray;
								} else {
									GUI.backgroundColor = Color.black;
									EditorGUILayout.LabelField("Active", GUILayout.Width(60));
									EditorGUILayout.LabelField("Object", options);
									GUI.backgroundColor = _gray;
								}
								EditorGUILayout.EndHorizontal();	
							}
							for(int g = 0; g < oPool.pools[i].uniquePool.Count; g++){
								
								EditorGUILayout.BeginHorizontal("box", options);
								if(!Application.isPlaying){
									GUI.backgroundColor = Color.red;
									if(GUILayout.Button("X", GUILayout.Width(20))){
										oPool.pools[i].uniquePool.RemoveAt(g);
										oPool.pools[i].bufferAmount.RemoveAt(g);
										oPool.pools[i].alwaysMax.RemoveAt(g);
									}
									GUI.backgroundColor = _gray;
								}
								if(g < oPool.pools[i].uniquePool.Count){
									GUILayout.Space(7);
									if(g < oPool.pools[i].uniquePool.Count){
									
										//RESOLVES UPGRADE ERRORS////////////////////////////////////////////
										if(oPool.pools[i].alwaysMax.Count < oPool.pools[i].uniquePool.Count)
											oPool.pools[i].alwaysMax.Add(true);
										////////////////////////////////////////////////////////////////////////
										if(oPool.pools[i].alwaysMax.Count == oPool.pools[i].uniquePool.Count){
											if(!Application.isPlaying)
												oPool.pools[i].alwaysMax[g] =  EditorGUILayout.Toggle(oPool.pools[i].alwaysMax[g], GUILayout.Width(15));
											if(!Application.isPlaying){
												if(!oPool.pools[i].alwaysMax[g])
													oPool.pools[i].bufferAmount[g] = EditorGUILayout.IntField(oPool.pools[i].bufferAmount[g], GUILayout.Width(40));
												else
													oPool.pools[i].bufferAmount[g] = Mathf.Clamp(EditorGUILayout.IntField(oPool.pools[i].bufferAmount[g], GUILayout.Width(40)), oPool.pools[i].maxBuffer, oPool.pools[i].maxBuffer);
											} else {
												EditorGUILayout.LabelField(oPool.pools[i].pool.Count(t => t.name.Contains(oPool.pools[i].uniquePool[g].name.Replace(' ', '_')) && t.activeSelf) + "/" + oPool.pools[i].pool.Count(t => t.name.Contains(oPool.pools[i].uniquePool[g].name.Replace(' ', '_'))), GUILayout.Width(60));
											}
										}
										if(!Application.isPlaying)
											oPool.pools[i].uniquePool[g] = (GameObject)EditorGUILayout.ObjectField(oPool.pools[i].uniquePool[g], typeof(GameObject), true, options);
										else
											EditorGUILayout.LabelField(oPool.pools[i].uniquePool[g].name, options);
									}
									
								}
								
								EditorGUILayout.EndHorizontal();
							}
							if(!Application.isPlaying){
								GUI.backgroundColor = Color.green;
								if(GUILayout.Button("Add Object", options)){
									oPool.pools[i].uniquePool.Add(null);
									oPool.pools[i].bufferAmount.Add(oPool.pools[i].maxBuffer);
									oPool.pools[i].alwaysMax.Add(true);
								}
								GUI.backgroundColor = _gray;
							}
							
							
						}
					}
					
					if( i < oPool.pools.Count){
						if(oPool.pools[i].uniquePool.Count != 0){
							if(!Application.isPlaying){
								GUILayout.Space(5);
								GUI.backgroundColor = Color.red;
								if(GUILayout.Button("Clear Pool", options)){
									if(EditorUtility.DisplayDialog("Clear All Objects From " + oPool.pools[i].name + "?", "Are you sure you want to clear all objects from " + oPool.pools[i].name + "? This action cannot be undone.", "Clear Objects", "Cancel")){
										oPool.pools[i].uniquePool = new List<GameObject>();
										oPool.pools[i].bufferAmount = new List<int>();
										oPool.pools[i].alwaysMax = new List<bool>();
									}
								}
								GUILayout.Space(5);
								GUI.backgroundColor = _gray;
							}
						}
					}
				}
				
				EditorGUILayout.EndVertical();
			}
		}
		if(!Application.isPlaying){
			GUILayout.Space(10);
			EditorGUILayout.BeginVertical("button", options);
			GUI.backgroundColor = Color.green;
			if(GUILayout.Button("Add Pool", options)){
				foldouts.Add(false);
				oPool.pools.Add(new Pool("New Pool"));
				if(oPool.pools.FindAll(a => a.name.Contains("New Pool")).Count > 1){
					oPool.pools[oPool.pools.Count -1].name = "New Pool " + (oPool.pools.FindAll(a => a.name.Contains("New Pool")).Count).ToString();
					EditorUtility.SetDirty(oPool);
				}
				/*if(oPool.pools.Count > 0){
					oPool.pools.Add(new Pool("Pool " + (poolIndex + 1).ToString()));
					poolIndex++;
				} else {
					oPool.pools.Add(new Pool("Pool 1"));
					poolIndex++;
				}*/
			}
			GUILayout.Space(5);
			if(oPool.pools.Count > 0){
				GUI.backgroundColor = Color.red;
				if(GUILayout.Button("Clear All Pools", options)){
					if(EditorUtility.DisplayDialog("Clear All Pools?", "Are you sure you want to clear all pools? This action cannot be undone.", "Clear All", "Cancel")){
						foldouts = new List<bool>();
						oPool.pools = new List<Pool>();
						poolIndex = 0;
					}
				}
			}
			GUI.backgroundColor = _gray;
			EditorGUILayout.EndVertical();
		} else {
			EditorGUILayout.LabelField("Monitoring pools may affect performance.");
		}
			EditorGUILayout.EndVertical();
		if(GUI.changed && !Application.isPlaying && Application.isEditor){
			EditorUtility.SetDirty(oPool);
		} else if(Application.isPlaying && Application.isEditor){
			EditorUtility.SetDirty(oPool);
		}
		
	}	
}