/*

**************************************
************ POOL MASTER *************
**************************************
______________________________________

VERSION: 2.0
FILE:    OBJECTPOOL.CS
AUTHOR:  CODY JOHNSON
COMPANY: HAMSTERBYTE, LLC
EMAIL:   HAMSTERBYTELLC@GMAIL.COM
WEBSITE: WWW.HAMSTERBYTE.COM
SUPPORT: WWW.HAMSTERBYTE.COM/POOL-MASTER

COPYRIGHT © 2014-2015 HAMSTERBYTE, LLC
ALL RIGHTS RESERVED

*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace hamsterbyte.PoolMaster
{
		[System.Serializable]
		public class ObjectPool : MonoBehaviour
		{
				public List<Pool> pools;
				public bool showDebugInfo = false;
				public bool showAllDebugInfo = false;
				public bool smartBuffering = false;
				public int smartBufferMax = 255;
				private int smartBufferCount;

				public ObjectPool ()
				{
						pools = new List<Pool> ();
				}

				void Awake ()
				{
						PreloadAll ();
				}

        #region PRELOAD ALL
				/// <summary>
				/// Preloads all.
				/// </summary>
				public void PreloadAll ()
				{

						//This function is used to instantiate all specified prefabs into the ObjectPool. It requires no input and should only be called once.
						//Calling this function more than once will inscrease the size of the ObjectPool multiplicatively.

						//These two variables are used to check if a pool already exists with the specified name, allowing for multiple pools to share a name and be consolidated into a single pool.
						//Sharing names between different pools is discouraged and should be avoided
						GameObject tObj;
						GameObject eObj;

						//Check to see if the object pool contains any pools
						if (pools.Count > 0) {

								//If pools are found then create a new object that corresponds to them
								//If two pools share a name, then only one object will be created. You should always give your pools a unique name.
								//If pools share a name this insures that no errors are created and objects from all pools sharing a name are consolidated into a single pool.
								foreach (Pool p in pools) {
										eObj = GameObject.Find (p.name);
										if (eObj == null) {
												tObj = new GameObject (p.name);
												tObj.transform.parent = this.transform;
										} else {
												tObj = eObj;
										}


										if (p.uniquePool.Count > 0) {
												if (p.preload & !p.hasLoaded) {
														if (showAllDebugInfo && Application.isEditor)
																Debug.Log ("Preloading Pool: " + p.name);
														p.hasLoaded = true;
														for (int i = 0; i < p.uniquePool.Count; i++) {
																for (int j = 0; j < p.bufferAmount[i]; j++) {
																		if (p.uniquePool [i] != null) {

																				//This instantiates the prefabs in the object pool
																				//Then gives them a unique name; the unique name is for reference only and it is not used to actually call the object.
																				//To call the object you use the original name of the prefab
																				//Lastly, it sets the parent of the new object to the parent object for the respective pool
																				GameObject g = (GameObject)Instantiate (p.uniquePool [i]);
																				g.name = g.name.Replace (' ', '_');
																				g.name = g.name.Split ('(') [0] + " - " + j.ToString ();
																				g.transform.parent = tObj.transform;

																				//If the prefab contains an AudioSource, check to see that the PooledAudio script is attached to it.
																				//If it does not already contain a PooledAudio script we will add a new one.
																				//This script is neccessary to control the behaviour of the audio source with Pool Master
																				if (g.GetComponent<AudioSource> () != null) {
																						if (g.GetComponent<PooledAudio> () == null) {
																								g.AddComponent<PooledAudio> ();
																						}
																				}

																				//If the prefab contains a ParticleSystem, check to see that the PooledParticlescript is attached to it.
																				//If it does not already contain a PooledParticle script we will add a new one.
																				//This script is neccessary to control the behaviour of the particle system with Pool Master
																				if (g.GetComponent<ParticleSystem> () != null) {
																						if (g.GetComponent<PooledParticle> () == null) {
																								g.AddComponent<PooledParticle> ();
																						}
																				}

																				//Despawn the object for use later. Objects in the pool are Despawnd by default to certify performance. Too many active objects will cause performace issues in your game.
																				//The performance issues are directly related to how intensive the scripts attached to the prefab are. More intensive scripts create a heavier load on the processor.
																				Despawn (g, true);

																				//This is the section that actually checks if there is another pool with the same name already present in the scene.
																				Pool pCheck = pools.FindLast (a => a.name == p.name);
																				pCheck.pool.Add (g);

																		}
																}

														}
												}
										}
								}
						}
				}
        #endregion

        #region PRELOAD
				/// <summary>
				/// Preload the specified poolName.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				public void Preload (string poolName)
				{
						Pool p = GetPool (poolName);
						GameObject parent = GameObject.Find (p.name);
						if (p != null && !p.hasLoaded) {
								p.hasLoaded = true;
								if (showAllDebugInfo && Application.isEditor)
										Debug.Log ("Preloading Pool: " + p.name);
								if (p.uniquePool.Count > 0) {
										for (int i = 0; i < p.uniquePool.Count; i++) {
												for (int j = 0; j < p.bufferAmount[i]; j++) {
														if (p.uniquePool [i] != null) {

																//This instantiates the prefabs in the object pool
																//Then gives them a unique name; the unique name is for reference only and it is not used to actually call the object.
																//To call the object you use the original name of the prefab
																//Lastly, it sets the parent of the new object to the parent object for the respective pool
																GameObject g = (GameObject)Instantiate (p.uniquePool [i]);
																g.name = g.name.Replace (' ', '_');
																g.name = g.name.Split ('(') [0] + " - " + j.ToString ();
																g.transform.parent = parent.transform;

																//If the prefab contains an AudioSource, check to see that the PooledAudio script is attached to it.
																//If it does not already contain a PooledAudio script we will add a new one.
																//This script is neccessary to control the behaviour of the audio source with Pool Master
																if (g.GetComponent<AudioSource> () != null) {
																		if (g.GetComponent<PooledAudio> () == null) {
																				g.AddComponent<PooledAudio> ();
																		}
																}

																//If the prefab contains a ParticleSystem, check to see that the PooledParticlescript is attached to it.
																//If it does not already contain a PooledParticle script we will add a new one.
																//This script is neccessary to control the behaviour of the particle system with Pool Master
																if (g.GetComponent<ParticleSystem> () != null) {
																		if (g.GetComponent<PooledParticle> () == null) {
																				g.AddComponent<PooledParticle> ();
																		}
																}

																//Despawn the object for use later. Objects in the pool are Despawnd by default to certify performance. Too many active objects will cause performace issues in your game.
																//The performance issues are directly related to how intensive the scripts attached to the prefab are. More intensive scripts create a heavier load on the processor.
																Despawn (g, true);

																//This is the section that actually checks if there is another pool with the same name already present in the scene.
																Pool pCheck = pools.FindLast (a => a.name == p.name);
																pCheck.pool.Add (g);

														}
												}

										}
								}
						} else {
								if (p == null && showDebugInfo && Application.isEditor)
										Debug.LogWarning ("Preload: Cannot find pool named '" + poolName + "'. Please verify spelling.");
						}
				}
        #endregion

        #region SPAWN
				/// <summary>
				/// Spawn the specified object from a specified pool
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				/// <param name="objName">Object name.</param>
				public void Spawn (string poolName, string objName)
				{
						Pool pCheck = pools.FindLast (a => a.name == poolName);
						if (pCheck != null) {
								GameObject tObj = pCheck.uniquePool.Find (n => n.name == objName);
								if (tObj != null) {
										GameObject tPoolObj = pCheck.pool.Find (o => o.name.Split (' ') [0] == tObj.name.Replace (' ', '_') && !o.activeSelf);
										if (tPoolObj != null) {
												if (tPoolObj.GetComponent<AudioSource> () != null)
														tPoolObj.transform.position = Camera.main.transform.position;

												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("Spawn: '" + tObj.name + "' spawned successfully.");
										} else {
												if (smartBuffering) {
														SmartBuffer (poolName, tObj);
												}
										}
								} else {
										if (Application.isEditor && showDebugInfo)
												Debug.LogWarning ("Spawn: '" + objName + "' not found! Please verify spelling.");
								}
						} else if (showDebugInfo && Application.isEditor) {
								Debug.LogWarning ("Spawn: Pool -> '" + poolName + "' not found! Please verify spelling.");
						}
				}

				/// <summary>
				/// Spawn the specified poolName, objName and position.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				/// <param name="objName">Object name.</param>
				/// <param name="position">Position.</param>
				public void Spawn (string poolName, string objName, Vector3 position)
				{
						Pool pCheck = pools.FindLast (a => a.name == poolName);
						if (pCheck != null) {
								GameObject tObj = pCheck.uniquePool.Find (n => n.name == objName);
								if (tObj != null) {
										GameObject tPoolObj = pCheck.pool.Find (o => o.name.Split (' ') [0] == tObj.name.Replace (' ', '_') && !o.activeSelf);
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("Spawn: '" + tObj.name + "' spawned successfully.");
										} else {
												if (smartBuffering) {
														SmartBuffer (poolName, tObj, position);
												}
										}
								} else {
										if (Application.isEditor && showDebugInfo)
												Debug.LogWarning ("Spawn: '" + objName + "' not found! Please verify name.");
								}
						} else if (showDebugInfo && Application.isEditor) {
								Debug.LogWarning ("Spawn: Pool -> '" + poolName + "' not found! Please verify spelling.");
						}
				}

				/// <summary>
				/// Spawn the specified poolName, objName, position and rotation.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				/// <param name="objName">Object name.</param>
				/// <param name="position">Position.</param>
				/// <param name="rotation">Rotation.</param>
				public void Spawn (string poolName, string objName, Vector3 position, Quaternion rotation)
				{
						Pool pCheck = pools.FindLast (a => a.name == poolName);
						if (pCheck != null) {
								GameObject tObj = pCheck.uniquePool.Find (n => n.name == objName);
								if (tObj != null) {
										GameObject tPoolObj = pCheck.pool.Find (o => o.name.Split (' ') [0] == tObj.name.Replace (' ', '_') && !o.activeSelf);
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.transform.rotation = rotation;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("Spawn: '" + tObj.name + "' spawned successfully.");
										} else {
												if (smartBuffering) {
														SmartBuffer (poolName, tObj, position, rotation);
												}
										}

								} else {
										if (Application.isEditor && showDebugInfo)
												Debug.LogWarning ("Spawn: '" + objName + "' not found! Please verify name.");
								}
						} else if (showDebugInfo && Application.isEditor) {
								Debug.LogWarning ("Spawn: Pool -> '" + poolName + "' not found! Please verify spelling.");
						}
				}
        #endregion

        #region SPAWN REFERENCE
				/// <summary>
				/// Spawns object with given pool name and object name; returns a reference.
				/// </summary>
				/// <returns>The reference.</returns>
				/// <param name="poolName">Pool name.</param>
				/// <param name="objName">Object name.</param>
				public GameObject SpawnReference (string poolName, string objName)
				{
						GameObject tPoolObj = null;
						Pool pCheck = pools.FindLast (a => a.name == poolName);
						if (pCheck != null) {
								GameObject tObj = pCheck.uniquePool.Find (n => n.name == objName);
								if (tObj != null) {
										tPoolObj = pCheck.pool.Find (o => o.name.Split (' ') [0] == tObj.name.Replace (' ', '_') && !o.activeSelf);
										if (tPoolObj != null) {
												if (tPoolObj.GetComponent<AudioSource> () != null)
														tPoolObj.transform.position = Camera.main.transform.position;

												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnReference: Object -> '" + tObj.name + "' spawned successfully.");
										} else {
												if (smartBuffering) {
														tPoolObj = SmartBufferReference (poolName, tObj);
												}
										}
								} else {
										if (Application.isEditor && showDebugInfo)
												Debug.LogWarning ("Spawn: '" + objName + "' not found! Please verify spelling.");
								}
						} else if (showDebugInfo && Application.isEditor) {
								Debug.LogWarning ("SpawnReference: Pool -> '" + poolName + "' not found! Please verify spelling.");
						}
						return tPoolObj;
				}

				/// <summary>
				/// Spawns object with given pool name, object name, and position; returns a reference.
				/// </summary>
				/// <returns>The reference.</returns>
				/// <param name="poolName">Pool name.</param>
				/// <param name="objName">Object name.</param>
				/// <param name="position">Position.</param>
				public GameObject SpawnReference (string poolName, string objName, Vector3 position)
				{
						GameObject tPoolObj = null;
						Pool pCheck = pools.FindLast (a => a.name == poolName);
						if (pCheck != null) {
								GameObject tObj = pCheck.uniquePool.Find (n => n.name == objName);
								if (tObj != null) {
										tPoolObj = pCheck.pool.Find (o => o.name.Split (' ') [0] == tObj.name.Replace (' ', '_') && !o.activeSelf);
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnReference: Object -> '" + tObj.name + "' spawned successfully.");
										} else {
												if (smartBuffering) {
														tPoolObj = SmartBufferReference (poolName, tObj, position);
												}
										}
								} else {
										if (Application.isEditor && showDebugInfo)
												Debug.LogWarning ("Spawn: '" + objName + "' not found! Verify name or increase object buffer.");
								}
						} else if (showDebugInfo && Application.isEditor) {
								Debug.LogWarning ("SpawnReference: Pool -> '" + poolName + "' not found! Please verify spelling.");
						}
						return tPoolObj;
				}

				/// <summary>
				/// Spawns object with given pool name, object name, position, and rotation; returns a reference.
				/// </summary>
				/// <returns>The reference.</returns>
				/// <param name="poolName">Pool name.</param>
				/// <param name="objName">Object name.</param>
				/// <param name="position">Position.</param>
				/// <param name="rotation">Rotation.</param>
				public GameObject SpawnReference (string poolName, string objName, Vector3 position, Quaternion rotation)
				{
						GameObject tPoolObj = null;
						Pool pCheck = pools.FindLast (a => a.name == poolName);
						if (pCheck != null) {
								GameObject tObj = pCheck.uniquePool.Find (n => n.name == objName);
								if (tObj != null) {
										tPoolObj = pCheck.pool.Find (o => o.name.Split (' ') [0] == tObj.name.Replace (' ', '_') && !o.activeSelf);
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.transform.rotation = rotation;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnReference: Object -> '" + tObj.name + "' spawned successfully.");
										} else {
												if (smartBuffering) {
														tPoolObj = SmartBufferReference (poolName, tObj, position, rotation);
												}
										}
								} else {
										if (Application.isEditor && showDebugInfo)
												Debug.LogWarning ("Spawn: '" + objName + "' not found! Verify name or increase object buffer.");
								}
						} else if (showDebugInfo && Application.isEditor) {
								Debug.LogWarning ("SpawnReference: Pool -> '" + poolName + "' not found! Please verify spelling.");
						}
						return tPoolObj;
				}
        #endregion

        #region SPAWN RANDOM
				/// <summary>
				/// Spawns random object with given pool name and position.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				/// <param name="position">Position.</param>
				public void SpawnRandom (string poolName, Vector3 position)
				{
						Pool pCheck = pools.FindLast (a => a.name == poolName);
						if (pCheck != null) {
								List<GameObject> inactive = pCheck.pool.FindAll (t => !t.activeSelf);
								if (inactive.Count > 0) {
										GameObject tPoolObj = inactive [Random.Range (0, inactive.Count)];
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnRandom: Object -> '" + tPoolObj.name.Split (' ') [0] + "' spawned successfully.");
										}
								} else {
										GameObject tObj = pCheck.uniquePool [Random.Range (0, pCheck.uniquePool.Count)];
										if (smartBuffering && tObj != null) {
												SmartBuffer (poolName, tObj, position);
										}
								}
						} else if (showDebugInfo && Application.isEditor) {
								Debug.LogWarning ("SpawnRandom: Pool -> '" + poolName + "' not found. Please verify spelling");
						}
				}
				/// <summary>
				/// Spawns random object from given list of pool names and position.
				/// </summary>
				/// <param name="poolNames">Pool names.</param>
				/// <param name="position">Position.</param>
				public void SpawnRandom (List<string> poolNames, Vector3 position)
				{
			
						if (CheckPoolsExist (poolNames)) {
								List<GameObject> inactive = MergedPool (poolNames).FindAll (t => !t.activeSelf);
								if (inactive.Count > 0) {
										GameObject tPoolObj = inactive [Random.Range (0, inactive.Count)];
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnRandom: Object -> '" + tPoolObj.name.Split (' ') [0] + "' spawned successfully.");
										}
								} else {
										int i = Random.Range (0, poolNames.Count);
										Pool pCheck = pools.FindLast (a => a.name == poolNames [i]);
										GameObject tObj = pCheck.uniquePool [Random.Range (0, pCheck.uniquePool.Count)];
										if (smartBuffering && tObj != null) {
												SmartBuffer (poolNames [i], tObj, position);
										}
								}
						}
				}
				/// <summary>
				/// Spawns random object from given array of pool names and position.
				/// </summary>
				/// <param name="poolNames">Pool names.</param>
				/// <param name="position">Position.</param>
				public void SpawnRandom (string[] poolNames, Vector3 position)
				{
			
						if (CheckPoolsExist (poolNames)) {
								List<GameObject> inactive = MergedPool (poolNames).FindAll (t => !t.activeSelf);
								if (inactive.Count > 0) {
										GameObject tPoolObj = inactive [Random.Range (0, inactive.Count)];
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnRandom: Object -> '" + tPoolObj.name.Split (' ') [0] + "' spawned successfully.");
										}
								} else {
										int i = Random.Range (0, poolNames.Length);
										Pool pCheck = pools.FindLast (a => a.name == poolNames [i]);
										GameObject tObj = pCheck.uniquePool [Random.Range (0, pCheck.uniquePool.Count)];
										if (smartBuffering && tObj != null) {
												SmartBuffer (poolNames [i], tObj, position);
										}
								}
						}
				}
				/// <summary>
				/// Spawns random object with given pool name, position, and rotation.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				/// <param name="position">Position.</param>
				/// <param name="rotation">Rotation.</param>
				public void SpawnRandom (string poolName, Vector3 position, Quaternion rotation)
				{
						Pool pCheck = pools.FindLast (a => a.name == poolName);
						if (pCheck != null) {
								List<GameObject> inactive = pCheck.pool.FindAll (t => !t.activeSelf);
								if (inactive.Count > 0) {
										GameObject tPoolObj = inactive [Random.Range (0, inactive.Count)];
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.transform.rotation = rotation;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnRandom: Object -> '" + tPoolObj.name.Split (' ') [0] + "' spawned successfully.");
										}
								} else {
										GameObject tObj = pCheck.uniquePool [Random.Range (0, pCheck.uniquePool.Count)];
										if (smartBuffering && tObj != null) {
												SmartBuffer (poolName, tObj, position);
										}
								}
						} else if (showDebugInfo && Application.isEditor) {
								Debug.LogWarning ("SpawnRandom: Pool -> '" + poolName + "' not found. Please verify spelling");
						}
				}
				/// <summary>
				/// Spawns random object from given list of pool names, position, and rotation.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				/// <param name="position">Position.</param>
				/// <param name="rotation">Rotation.</param>
				public void SpawnRandom (List<string> poolNames, Vector3 position, Quaternion rotation)
				{
						if (CheckPoolsExist (poolNames)) {
								List<GameObject> inactive = MergedPool (poolNames).FindAll (t => !t.activeSelf);
								if (inactive.Count > 0) {
										GameObject tPoolObj = inactive [Random.Range (0, inactive.Count)];
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.transform.rotation = rotation;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnRandom: Object -> '" + tPoolObj.name.Split (' ') [0] + "' spawned successfully.");
										}
								} else {
										int i = Random.Range (0, poolNames.Count);
										Pool pCheck = pools.FindLast (a => a.name == poolNames [i]);
										GameObject tObj = pCheck.uniquePool [Random.Range (0, pCheck.uniquePool.Count)];
										if (smartBuffering && tObj != null) {
												SmartBuffer (poolNames [i], tObj, position);
										}
								}
						}
				}
				/// <summary>
				/// Spawns random object from given array of pool names, position, and rotation.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				/// <param name="position">Position.</param>
				/// <param name="rotation">Rotation.</param>
				public void SpawnRandom (string[] poolNames, Vector3 position, Quaternion rotation)
				{
						if (CheckPoolsExist (poolNames)) {
								List<GameObject> inactive = MergedPool (poolNames).FindAll (t => !t.activeSelf);
								if (inactive.Count > 0) {
										GameObject tPoolObj = inactive [Random.Range (0, inactive.Count)];
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.transform.rotation = rotation;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnRandom: Object -> '" + tPoolObj.name.Split (' ') [0] + "' spawned successfully.");
										}
								} else {
										int i = Random.Range (0, poolNames.Length);
										Pool pCheck = pools.FindLast (a => a.name == poolNames [i]);
										GameObject tObj = pCheck.uniquePool [Random.Range (0, pCheck.uniquePool.Count)];
										if (smartBuffering && tObj != null) {
												SmartBuffer (poolNames [i], tObj, position);
										}
								}
						}
				}
        #endregion

        #region SPAWN RANDOM REFERENCE
				/// <summary>
				/// Spawns random object with given pool name and position; returns a reference.
				/// </summary>
				/// <returns>The random reference.</returns>
				/// <param name="poolName">Pool name.</param>
				/// <param name="position">Position.</param>
				public GameObject SpawnRandomReference (string poolName, Vector3 position)
				{
						GameObject tPoolObj = null;
						Pool pCheck = pools.FindLast (a => a.name == poolName);
						if (pCheck != null) {
								List<GameObject> inactive = pCheck.pool.FindAll (t => !t.activeSelf);
								if (inactive.Count > 0) {
										tPoolObj = inactive [Random.Range (0, inactive.Count)];
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnRandom: Object -> '" + tPoolObj.name.Split (' ') [0] + "' spawned successfully.");
										}
								} else {
										GameObject tObj = pCheck.uniquePool [Random.Range (0, pCheck.uniquePool.Count)];
										if (smartBuffering && tObj != null) {
												tPoolObj = SmartBufferReference (poolName, tObj, position);
										}
								}
						} else if (showDebugInfo && Application.isEditor) {
								Debug.LogWarning ("SpawnRandom: Pool -> '" + poolName + "' not found. Please verify spelling");
						}
						return tPoolObj;
				}
				/// <summary>
				/// Spawns random object from given list of pool names at given position; returns a reference.
				/// </summary>
				/// <returns>The random reference.</returns>
				/// <param name="poolNames">List of pool names</param>
				/// <param name="position">Position.</param>
				public GameObject SpawnRandomReference (List<string> poolNames, Vector3 position)
				{
						GameObject tPoolObj = null;
						if (CheckPoolsExist (poolNames)) {
								List<GameObject> inactive = MergedPool (poolNames).FindAll (t => !t.activeSelf);
								if (inactive.Count > 0) {
										tPoolObj = inactive [Random.Range (0, inactive.Count)];
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnRandom: Object -> '" + tPoolObj.name.Split (' ') [0] + "' spawned successfully.");
										}
								} else {
										int i = Random.Range (0, poolNames.Count);
										Pool pCheck = pools.FindLast (a => a.name == poolNames [i]);
										GameObject tObj = pCheck.uniquePool [Random.Range (0, pCheck.uniquePool.Count)];
										if (smartBuffering && tObj != null) {
												tPoolObj = SmartBufferReference (poolNames [i], tObj, position);
										}
								}
						}
						return tPoolObj;
				}
				/// <summary>
				/// Spawns random object from given array of pool names at given position; returns a reference.
				/// </summary>
				/// <returns>The random reference.</returns>
				/// <param name="poolNames">List of pool names</param>
				/// <param name="position">Position.</param>
				public GameObject SpawnRandomReference (string[] poolNames, Vector3 position)
				{
						GameObject tPoolObj = null;
						if (CheckPoolsExist (poolNames)) {
								List<GameObject> inactive = MergedPool (poolNames).FindAll (t => !t.activeSelf);
								if (inactive.Count > 0) {
										tPoolObj = inactive [Random.Range (0, inactive.Count)];
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnRandom: Object -> '" + tPoolObj.name.Split (' ') [0] + "' spawned successfully.");
										}
								} else {
										int i = Random.Range (0, poolNames.Length);
										Pool pCheck = pools.FindLast (a => a.name == poolNames [i]);
										GameObject tObj = pCheck.uniquePool [Random.Range (0, pCheck.uniquePool.Count)];
										if (smartBuffering && tObj != null) {
												tPoolObj = SmartBufferReference (poolNames [i], tObj, position);
										}
								}
						}
						return tPoolObj;
				}

				/// <summary>
				/// Spawns a random object with given pool name, position, and rotation; returns a reference.
				/// </summary>
				/// <returns>The random reference.</returns>
				/// <param name="poolName">Pool name.</param>
				/// <param name="position">Position.</param>
				/// <param name="rotation">Rotation.</param>
				public GameObject SpawnRandomReference (string poolName, Vector3 position, Quaternion rotation)
				{
						GameObject tPoolObj = null;
						Pool pCheck = pools.FindLast (a => a.name == poolName);
						if (pCheck != null) {
								List<GameObject> inactive = pCheck.pool.FindAll (t => !t.activeSelf);
								if (inactive.Count > 0) {
										tPoolObj = inactive [Random.Range (0, inactive.Count)];
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.transform.rotation = rotation;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnRandom: Object -> '" + tPoolObj.name.Split (' ') [0] + "' spawned successfully.");
										}
								} else {
										GameObject tObj = pCheck.uniquePool [Random.Range (0, pCheck.uniquePool.Count)];
										if (smartBuffering && tObj != null) {
												tPoolObj = SmartBufferReference (poolName, tObj, position);
										}
								}
						} else if (showDebugInfo && Application.isEditor) {
								Debug.LogWarning ("SpawnRandom: Pool -> '" + poolName + "' not found. Please verify spelling");
						}
						return tPoolObj;
				}
				/// <summary>
				/// Spawns a random object from one of the given pool names at the given position and rotation; returns a reference.
				/// </summary>
				/// <returns>The random reference.</returns>
				/// <param name="poolName">Pool names.</param>
				/// <param name="position">Position.</param>
				/// <param name="rotation">Rotation.</param>
				public GameObject SpawnRandomReference (List<string> poolNames, Vector3 position, Quaternion rotation)
				{
						GameObject tPoolObj = null;
						if (CheckPoolsExist (poolNames)) {
								List<GameObject> inactive = MergedPool (poolNames).FindAll (t => !t.activeSelf);
								if (inactive.Count > 0) {
										tPoolObj = inactive [Random.Range (0, inactive.Count)];
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.transform.rotation = rotation;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnRandom: Object -> '" + tPoolObj.name.Split (' ') [0] + "' spawned successfully.");
										}
								} else {
										int i = Random.Range (0, poolNames.Count);
										Pool pCheck = pools.FindLast (a => a.name == poolNames [i]);
										GameObject tObj = pCheck.uniquePool [Random.Range (0, pCheck.uniquePool.Count)];
										if (smartBuffering && tObj != null) {
												tPoolObj = SmartBufferReference (poolNames [i], tObj, position);
										}
								}
						}
						return tPoolObj;
				}
				/// <summary>
				/// Spawns a random object from one of the given pool names at the given position and rotation; returns a reference.
				/// </summary>
				/// <returns>The random reference.</returns>
				/// <param name="poolName">Pool names.</param>
				/// <param name="position">Position.</param>
				/// <param name="rotation">Rotation.</param>
				public GameObject SpawnRandomReference (string[] poolNames, Vector3 position, Quaternion rotation)
				{
						GameObject tPoolObj = null;
						if (CheckPoolsExist (poolNames)) {
								List<GameObject> inactive = MergedPool (poolNames).FindAll (t => !t.activeSelf);
								if (inactive.Count > 0) {
										tPoolObj = inactive [Random.Range (0, inactive.Count)];
										if (tPoolObj != null) {
												tPoolObj.transform.position = position;
												tPoolObj.transform.rotation = rotation;
												tPoolObj.SetActive (true);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SpawnRandom: Object -> '" + tPoolObj.name.Split (' ') [0] + "' spawned successfully.");
										}
								} else {
										int i = Random.Range (0, poolNames.Length);
										Pool pCheck = pools.FindLast (a => a.name == poolNames [i]);
										GameObject tObj = pCheck.uniquePool [Random.Range (0, pCheck.uniquePool.Count)];
										if (smartBuffering && tObj != null) {
												tPoolObj = SmartBufferReference (poolNames [i], tObj, position);
										}
								}
						}
						return tPoolObj;
				}
        #endregion

        #region DESPAWN
				/// <summary>
				/// Despawn the specified GameObject.
				/// </summary>
				/// <param name="g">The game object to despawn.</param>
				public void Despawn (GameObject g)
				{
						if (g != null) {
								if (g.activeSelf) {
										if (Application.isEditor && showAllDebugInfo)
												Debug.Log ("Despawn: Object -> '" + g.name + "' despawned successfully.");
										g.SetActive (false);
								}
						} else {
								if (Application.isEditor && showDebugInfo)
										Debug.LogWarning ("Despawn: Object -> '" + g.name + "' not found! Please check spelling.");
						}
				}
				/// <summary>
				/// Despawn the specified GameObject.
				/// </summary>
				/// <param name="g">The game object to despawn.</param>
				public void Despawn (GameObject g, bool ignoreDebug)
				{
						if (g != null) {
								if (g.activeSelf) {
										if (Application.isEditor && showAllDebugInfo && !ignoreDebug)
												Debug.Log ("Despawn: Object -> '" + g.name + "' despawned successfully.");
										g.SetActive (false);
								}
						} else {
								if (Application.isEditor && showDebugInfo && !ignoreDebug)
										Debug.LogWarning ("Despawn: Object -> '" + g.name + "' not found! Please check spelling.");
						}
				}

				/// <summary>
				/// Despawn the specified poolName.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				public void Despawn (string poolName)
				{
						GameObject parent = GameObject.Find (poolName);
						if (parent != null) {
								if (Application.isEditor && showAllDebugInfo)
										Debug.Log ("Despawn: Pool -> '" + poolName + "' despawned successfully.");

								foreach (Transform t in parent.transform)
										Despawn (t.gameObject, true);
						} else if (parent == null && showDebugInfo && Application.isEditor) {
								Debug.LogWarning ("Despawn: Pool -> '" + poolName + "' not found! Please verify spelling.");
						}

				}
				/// <summary>
				/// Despawn the specified poolNames.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				public void Despawn (string[] poolNames)
				{
						foreach (string s in poolNames) {
								Despawn (s);
						}
				}
				/// <summary>
				/// Despawn the specified poolNames.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				public void Despawn (List<string> poolNames)
				{
						foreach (string s in poolNames) {
								Despawn (s);
						}
				}
        #endregion

        #region DESTROY
				/// <summary>
				/// Destroy the specified g.
				/// </summary>
				/// <param name="g">The green component.</param>
				public void Destroy (GameObject g)
				{
						if (Application.isEditor && showAllDebugInfo)
								Debug.Log ("Destroy: Object -> '" + g.name + "' destroyed successfully.");
						GameObject.Destroy (g);
						if (smartBuffering && smartBufferCount > 0)
								smartBufferCount--;
				}
				/// <summary>
				/// Destroy the specified poolName.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				public void Destroy (string poolName)
				{
						Pool pCheck = pools.Find (n => n.name == poolName);
						if (pCheck != null) {
								int pInt = pools.FindLastIndex (i => i.name == poolName);
								pools.RemoveAt (pInt);
								if (Application.isEditor && showAllDebugInfo)
										Debug.LogWarning ("Destroy: Pool -> '" + poolName + "' destroyed successfully.");
						} else {
								if (Application.isEditor && showDebugInfo)
										Debug.LogWarning ("Destroy: Pool -> '" + poolName + "' not found! Please verify spelling.");
						}
						GameObject parent = GameObject.Find (poolName);
						if (parent != null) {
								foreach (Transform t in parent.transform) {
										GameObject.Destroy (t.gameObject);
										if (smartBuffering && smartBufferCount > 0)
												smartBufferCount--;
								}
								GameObject.Destroy (parent);
						}

				}
				/// <summary>
				/// Destroy the specified poolNames.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				public void Destroy (string[] poolNames)
				{
						foreach (string s in poolNames) {
								this.Destroy (s);
						}
				}
				/// <summary>
				/// Destroy the specified poolNames.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				public void Destroy (List<string> poolNames)
				{
						foreach (string s in poolNames) {
								this.Destroy (s);
						}
				}


        #endregion

        #region SMART BUFFER
				/// <summary>
				/// Smarts the buffer.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				/// <param name="obj">Object.</param>
				private void SmartBuffer (string poolName, GameObject obj)
				{
						if (smartBufferCount < smartBufferMax) {
								Pool pCheck = pools.FindLast (a => a.name == poolName);
								
								if (pCheck != null) {
										if (!pCheck.hasLoaded)
												Preload (pCheck.name);
										GameObject tPoolObj = pCheck.pool.FindLast (o => o.name.Split (' ') [0] == obj.name.Replace (' ', '_'));
										if (tPoolObj != null) {
												int lastIndex = pCheck.pool.Count (o => o.name.Split (' ') [0] == obj.name.Replace (' ', '_'));
												GameObject g = (GameObject)Instantiate (obj);
												g.name = g.name.Replace (' ', '_');
												g.name = g.name.Split ('(') [0] + " - " + (lastIndex).ToString ();
												g.transform.parent = GameObject.Find ("MASTER POOL").transform.FindChild (poolName);
												pCheck.pool.Add (g);
												smartBufferCount++;
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SmartBuffer: Object -> '" + g.name + "' successfully added to  Pool -> '" + pCheck.name + "'");
												//If the prefab contains an AudioSource, check to see that the PooledAudio script is attached to it.
												//If it does not already contain a PooledAudio script we will add a new one.
												//This script is neccessary to control the behaviour of the audio source with Pool Master
												if (g.GetComponent<AudioSource> () != null) {
														if (g.GetComponent<PooledAudio> () == null) {
																g.AddComponent<PooledAudio> ();
														}
														g.SetActive (false);
														g.SetActive (true);
												}
	
												//If the prefab contains a ParticleSystem, check to see that the PooledParticlescript is attached to it.
												//If it does not already contain a PooledParticle script we will add a new one.
												//This script is neccessary to control the behaviour of the particle system with Pool Master
												if (g.GetComponent<ParticleSystem> () != null) {
														if (g.GetComponent<PooledParticle> () == null) {
																g.AddComponent<PooledParticle> ();
														}
														g.SetActive (false);
														g.SetActive (true);
												}
										}
								} else {
										if (Application.isEditor && showDebugInfo)
												Debug.LogWarning ("SmartBuffer: Maximum buffer reached. Adjust buffer levels");
								}
						}
				}

				/// <summary>
				/// Smarts the buffer.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				/// <param name="obj">Object.</param>
				/// <param name="position">Position.</param>
				private void SmartBuffer (string poolName, GameObject obj, Vector3 position)
				{
						if (smartBufferCount < smartBufferMax) {
								Pool pCheck = pools.FindLast (a => a.name == poolName);
								
								if (pCheck != null) {
										if (!pCheck.hasLoaded)
												Preload (pCheck.name);
										GameObject tPoolObj = pCheck.pool.FindLast (o => o.name.Split (' ') [0] == obj.name.Replace (' ', '_'));
										if (tPoolObj != null) {
												int lastIndex = pCheck.pool.Count (o => o.name.Split (' ') [0] == obj.name.Replace (' ', '_'));
												GameObject g = (GameObject)Instantiate (obj, position, Quaternion.identity);
												g.name = g.name.Replace (' ', '_');
												g.name = g.name.Split ('(') [0] + " - " + (lastIndex).ToString ();
												g.transform.parent = GameObject.Find ("MASTER POOL").transform.FindChild (poolName);
												pCheck.pool.Add (g);
												smartBufferCount++;
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SmartBuffer: Object -> '" + g.name + "' successfully added to  Pool -> '" + pCheck.name + "'");
												//If the prefab contains an AudioSource, check to see that the PooledAudio script is attached to it.
												//If it does not already contain a PooledAudio script we will add a new one.
												//This script is neccessary to control the behaviour of the audio source with Pool Master
												if (g.GetComponent<AudioSource> () != null) {
														if (g.GetComponent<PooledAudio> () == null) {
																g.AddComponent<PooledAudio> ();
														}
														g.SetActive (false);
														g.SetActive (true);
												}
	
												//If the prefab contains a ParticleSystem, check to see that the PooledParticlescript is attached to it.
												//If it does not already contain a PooledParticle script we will add a new one.
												//This script is neccessary to control the behaviour of the particle system with Pool Master
												if (g.GetComponent<ParticleSystem> () != null) {
														if (g.GetComponent<PooledParticle> () == null) {
																g.AddComponent<PooledParticle> ();
														}
														g.SetActive (false);
														g.SetActive (true);
												}
										}
								}
						} else {
								if (Application.isEditor && showDebugInfo)
										Debug.LogWarning ("SmartBuffer: Maximum buffer reached. Adjust buffer levels");
						}
				}

				/// <summary>
				/// Smarts the buffer.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				/// <param name="obj">Object.</param>
				/// <param name="position">Position.</param>
				/// <param name="rotation">Rotation.</param>
				private void SmartBuffer (string poolName, GameObject obj, Vector3 position, Quaternion rotation)
				{
						if (smartBufferCount < smartBufferMax) {
								Pool pCheck = pools.FindLast (a => a.name == poolName);
								
								if (pCheck != null) {
										if (!pCheck.hasLoaded)
												Preload (pCheck.name);
										GameObject tPoolObj = pCheck.pool.FindLast (o => o.name.Split (' ') [0] == obj.name.Replace (' ', '_'));
										if (tPoolObj != null) {
												int lastIndex = pCheck.pool.Count (o => o.name.Split (' ') [0] == obj.name.Replace (' ', '_'));
												GameObject g = (GameObject)Instantiate (obj, position, rotation);
												g.name = g.name.Replace (' ', '_');
												g.name = g.name.Split ('(') [0] + " - " + (lastIndex).ToString ();
												g.transform.parent = GameObject.Find ("MASTER POOL").transform.FindChild (poolName);
												pCheck.pool.Add (g);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SmartBuffer: Object -> '" + g.name + "' successfully added to  Pool -> '" + pCheck.name + "'");
												//If the prefab contains an AudioSource, check to see that the PooledAudio script is attached to it.
												//If it does not already contain a PooledAudio script we will add a new one.
												//This script is neccessary to control the behaviour of the audio source with Pool Master
												if (g.GetComponent<AudioSource> () != null) {
														if (g.GetComponent<PooledAudio> () == null) {
																g.AddComponent<PooledAudio> ();
														}
														g.SetActive (false);
														g.SetActive (true);
												}
	
												//If the prefab contains a ParticleSystem, check to see that the PooledParticlescript is attached to it.
												//If it does not already contain a PooledParticle script we will add a new one.
												//This script is neccessary to control the behaviour of the particle system with Pool Master
												if (g.GetComponent<ParticleSystem> () != null) {
														if (g.GetComponent<PooledParticle> () == null) {
																g.AddComponent<PooledParticle> ();
														}
														g.SetActive (false);
														g.SetActive (true);
												}
										}
								}
						} else {
								if (Application.isEditor && showDebugInfo)
										Debug.LogWarning ("SmartBuffer: Maximum buffer reached. Adjust buffer levels");
						}
				}

				/// <summary>
				/// Smarts the buffer.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				/// <param name="obj">Object.</param>
				private GameObject SmartBufferReference (string poolName, GameObject obj)
				{
        	
						GameObject g = null;
						if (smartBufferCount < smartBufferMax) {
								Pool pCheck = pools.FindLast (a => a.name == poolName);
								if (pCheck != null) {
										if (!pCheck.hasLoaded)
												Preload (pCheck.name);
										GameObject tPoolObj = pCheck.pool.FindLast (o => o.name.Split (' ') [0] == obj.name.Replace (' ', '_'));
										if (tPoolObj != null) {
												int lastIndex = pCheck.pool.Count (o => o.name.Split (' ') [0] == obj.name.Replace (' ', '_'));
												g = (GameObject)Instantiate (obj);
												g.name = g.name.Replace (' ', '_');
												g.name = g.name.Split ('(') [0] + " - " + (lastIndex).ToString ();
												g.transform.parent = GameObject.Find ("MASTER POOL").transform.FindChild (poolName);
												pCheck.pool.Add (g);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SmartBufferReference: Object -> '" + g.name + "' successfully added to  Pool -> '" + pCheck.name + "'");
												//If the prefab contains an AudioSource, check to see that the PooledAudio script is attached to it.
												//If it does not already contain a PooledAudio script we will add a new one.
												//This script is neccessary to control the behaviour of the audio source with Pool Master
												if (g.GetComponent<AudioSource> () != null) {
														if (g.GetComponent<PooledAudio> () == null) {
																g.AddComponent<PooledAudio> ();
														}
														g.SetActive (false);
														g.SetActive (true);
												}
	
												//If the prefab contains a ParticleSystem, check to see that the PooledParticlescript is attached to it.
												//If it does not already contain a PooledParticle script we will add a new one.
												//This script is neccessary to control the behaviour of the particle system with Pool Master
												if (g.GetComponent<ParticleSystem> () != null) {
														if (g.GetComponent<PooledParticle> () == null) {
																g.AddComponent<PooledParticle> ();
														}
														g.SetActive (false);
														g.SetActive (true);
												}
	
										}
								}
						} else {
								if (Application.isEditor && showDebugInfo)
										Debug.LogWarning ("SmartBufferReference: Maximum buffer reached. Adjust buffer levels");
						}
						return g;
				}

				/// <summary>
				/// Smarts the buffer.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				/// <param name="obj">Object.</param>
				/// <param name="position">Position.</param>
				private GameObject SmartBufferReference (string poolName, GameObject obj, Vector3 position)
				{
						GameObject g = null;
						if (smartBufferCount < smartBufferMax) {
								Pool pCheck = pools.FindLast (a => a.name == poolName);
								if (pCheck != null) {
										if (!pCheck.hasLoaded)
												Preload (pCheck.name);
										GameObject tPoolObj = pCheck.pool.FindLast (o => o.name.Split (' ') [0] == obj.name.Replace (' ', '_'));
										if (tPoolObj != null) {
												int lastIndex = pCheck.pool.Count (o => o.name.Split (' ') [0] == obj.name.Replace (' ', '_'));
												g = (GameObject)Instantiate (obj, position, Quaternion.identity);
												g.name = g.name.Replace (' ', '_');
												g.name = g.name.Split ('(') [0] + " - " + (lastIndex).ToString ();
												g.transform.parent = GameObject.Find ("MASTER POOL").transform.FindChild (poolName);
												pCheck.pool.Add (g);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SmartBufferReference: Object -> '" + g.name + "' successfully added to  Pool -> '" + pCheck.name + "'");
												//If the prefab contains an AudioSource, check to see that the PooledAudio script is attached to it.
												//If it does not already contain a PooledAudio script we will add a new one.
												//This script is neccessary to control the behaviour of the audio source with Pool Master
												if (g.GetComponent<AudioSource> () != null) {
														if (g.GetComponent<PooledAudio> () == null) {
																g.AddComponent<PooledAudio> ();
														}
														g.SetActive (false);
														g.SetActive (true);
												}
	
												//If the prefab contains a ParticleSystem, check to see that the PooledParticlescript is attached to it.
												//If it does not already contain a PooledParticle script we will add a new one.
												//This script is neccessary to control the behaviour of the particle system with Pool Master
												if (g.GetComponent<ParticleSystem> () != null) {
														if (g.GetComponent<PooledParticle> () == null) {
																g.AddComponent<PooledParticle> ();
														}
														g.SetActive (false);
														g.SetActive (true);
												}
										}
								}
						} else {
								if (Application.isEditor && showDebugInfo)
										Debug.LogWarning ("SmartBufferReference: Maximum buffer reached. Adjust buffer levels");
						}
						return g;
				}

				/// <summary>
				/// Smarts the buffer.
				/// </summary>
				/// <param name="poolName">Pool name.</param>
				/// <param name="obj">Object.</param>
				/// <param name="position">Position.</param>
				private GameObject SmartBufferReference (string poolName, GameObject obj, Vector3 position, Quaternion rotation)
				{
						GameObject g = null;
						if (smartBufferCount < smartBufferMax) {
								Pool pCheck = pools.FindLast (a => a.name == poolName);
								if (pCheck != null) {
										if (!pCheck.hasLoaded)
												Preload (pCheck.name);
										GameObject tPoolObj = pCheck.pool.FindLast (o => o.name.Split (' ') [0] == obj.name.Replace (' ', '_'));
										if (tPoolObj != null) {
												int lastIndex = pCheck.pool.Count (o => o.name.Split (' ') [0] == obj.name.Replace (' ', '_'));
												g = (GameObject)Instantiate (obj, position, rotation);
												g.name = g.name.Replace (' ', '_');
												g.name = g.name.Split ('(') [0] + " - " + (lastIndex).ToString ();
												g.transform.parent = GameObject.Find ("MASTER POOL").transform.FindChild (poolName);
												pCheck.pool.Add (g);
												if (Application.isEditor && showAllDebugInfo)
														Debug.Log ("SmartBufferReference: Object -> '" + g.name + "' successfully added to  Pool -> '" + pCheck.name + "'");
												//If the prefab contains an AudioSource, check to see that the PooledAudio script is attached to it.
												//If it does not already contain a PooledAudio script we will add a new one.
												//This script is neccessary to control the behaviour of the audio source with Pool Master
												if (g.GetComponent<AudioSource> () != null) {
														if (g.GetComponent<PooledAudio> () == null) {
																g.AddComponent<PooledAudio> ();
														}
														g.SetActive (false);
														g.SetActive (true);
												}
	
												//If the prefab contains a ParticleSystem, check to see that the PooledParticlescript is attached to it.
												//If it does not already contain a PooledParticle script we will add a new one.
												//This script is neccessary to control the behaviour of the particle system with Pool Master
												if (g.GetComponent<ParticleSystem> () != null) {
														if (g.GetComponent<PooledParticle> () == null) {
																g.AddComponent<PooledParticle> ();
														}
														g.SetActive (false);
														g.SetActive (true);
												}
										}
								} else {
										if (Application.isEditor && showDebugInfo)
												Debug.LogWarning ("SmartBufferReference: Maximum buffer reached. Adjust buffer levels");
								}
						}
						return g;
				}
        #endregion
        
		#region GET POOL
				/// <summary>
				/// Gets a reference to a pool with a given name.
				/// </summary>
				/// <returns>The pool.</returns>
				/// <param name="name">Name.</param>
				public Pool GetPool (string name)
				{
						Pool pCheck = pools.FindLast (a => a.name == name);
						return pCheck;
				}
		
				/// <summary>
				/// Gets a reference to a pool with a given index.
				/// </summary>
				/// <returns>The pool.</returns>
				/// <param name="index">Index.</param>
				public Pool GetPool (int index)
				{
						if (index < this.pools.Count)
								return pools [index];
						else
								return null;
				}
		#endregion
		
		#region CHECK POOL EXISTS
				/// <summary>
				/// Checks the pool exists.
				/// </summary>
				/// <returns><c>true</c>, if pool exists was checked, <c>false</c> otherwise.</returns>
				/// <param name="poolName">Pool name.</param>
				private bool CheckPoolExists (string poolName)
				{
						if (GetPool (poolName) != null) {
								return true;
						} else {
								if (showDebugInfo && Application.isEditor)
										Debug.LogWarning ("CheckPoolExists: Pool -> '" + poolName + "' not found. Please verify spelling");
								return false;
						}	
				}
				/// <summary>
				/// Checks the pools exist.
				/// </summary>
				/// <returns><c>true</c>, if pools exist, <c>false</c> otherwise.</returns>
				/// <param name="poolNames">Pool names.</param>
				private bool CheckPoolsExist (List<string> poolNames)
				{
						bool b = true;
						foreach (string name in poolNames) {
								if (!CheckPoolExists (name)) {
										b = false;
								}
						}
						return b;
				}
				/// <summary>
				/// Checks the pools exist.
				/// </summary>
				/// <returns><c>true</c>, if pools exist, <c>false</c> otherwise.</returns>
				/// <param name="poolNames">Pool names.</param>
				private bool CheckPoolsExist (string[] poolNames)
				{
						bool b = true;
						foreach (string name in poolNames) {
								if (!CheckPoolExists (name)) {
										b = false;
								}
						}
						return b;
				}
		#endregion
		
		#region MERGED POOL
				/// <summary>
				/// Returns merged pool
				/// </summary>
				/// <returns>The pool.</returns>
				/// <param name="poolNames">Pool names.</param>
				private List<GameObject> MergedPool (List<string> poolNames)
				{
						if (CheckPoolsExist (poolNames)) {
								List<GameObject> mergedList = new List<GameObject> ();
								foreach (string s in poolNames) {
										Pool p = GetPool (s);
										mergedList.Concat (p.pool);
								}
								return mergedList.ToList ();
						} else {
								return null;
						}
				}
				/// <summary>
				/// Merges the pool
				/// </summary>
				/// <returns>The pool.</returns>
				/// <param name="poolNames">Pool names.</param>
				private List<GameObject> MergedPool (string[] poolNames)
				{
						List<GameObject> mergedList = new List<GameObject> ();
						if (CheckPoolsExist (poolNames)) {
								foreach (string s in poolNames) {
										Pool p = GetPool (s);
										mergedList.AddRange (p.pool);
								}
						} else {
								return null;
						}
						return mergedList;
				}
		#endregion
		}

}
