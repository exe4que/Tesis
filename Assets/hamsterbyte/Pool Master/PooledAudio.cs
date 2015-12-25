/*

**************************************
************ POOL MASTER *************
**************************************
______________________________________

VERSION: 1.0
FILE:    POOLEDAUDIO.CS
AUTHOR:  CODY JOHNSON
COMPANY: HAMSTERBYTE, LLC
EMAIL:   HAMSTERBYTELLC@GMAIL.COM
WEBSITE: WWW.HAMSTERBYTE.COM
SUPPORT: WWW.HAMSTERBYTE.COM/POOL-MASTER

COPYRIGHT © 2014 HAMSTERBYTE, LLC
ALL RIGHTS RESERVED

*/
using UnityEngine;
using System.Collections;

namespace hamsterbyte.PoolMaster
{
    public class PooledAudio : MonoBehaviour
    {
        private bool _loaded;
        void Awake()
        {
            //Has the sound been loaded into memory yet? If not, we make sure it is disabled to refrain from playing
            //the sound until it has been loaded and called upon.
            if (!_loaded)
            {

                if (this.gameObject.activeSelf)
                    this.gameObject.SetActive(false);

                _loaded = true;
            }
        }
        void OnEnable()
        {
            //Play the audio file when this object is enabled
            StartCoroutine("PlayAudio");
        }
        public IEnumerator PlayAudio()
        {
            //Find the audio source attached to this object and play it.
            AudioSource a = this.GetComponent<AudioSource>();
            a.Play();

            //If the audio is called to loop we skip this, if not the object is automatically disabled after the audio has finished playing
            if (!a.loop)
            {
                yield return new WaitForSeconds(a.clip.length);
                this.gameObject.SetActive(false);
            }

            StopAllCoroutines();

        }
    }
}
