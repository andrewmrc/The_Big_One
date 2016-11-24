using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityStandardAssets.Cameras
{

    [System.Serializable]
    public class RailContainer
    {
        public Transform targetA, targetB;
		[Tooltip("Asset da guardare durante la carrellata")]
        public Transform targetToLook;
        [Tooltip("La schermata si SCURISCE")]
        public bool fadeIn; 
		[Tooltip("La schermata si SCHIARISCE")]
		public bool fadeOut;
		[Tooltip("Il tempo di esecuzione della carrellata tra TargetA e TargetB")]
        public float timeExec;
		[Tooltip("Il tempo di attesa una volta raggiunto il Target")]
		public float waitTime;
    }


    public class RailCamera : MonoBehaviour {

        public RailContainer[] moveCamera;

        public float distance = 1f;
        [Range(0,1)]
        public float speed = 0;
        public GameObject mainCamera;
		
        Fader refFader;

        void Start()
        {
            refFader = FindObjectOfType<Fader>();
            StartCoroutine(RailCameraCO());
        }

        private IEnumerator RailCameraCO()
        {
            mainCamera.GetComponent<FreeLookCam>().enabled = false;

            for (int i = 0; i < moveCamera.Length; i++)
            {
                if (moveCamera[i].fadeOut)
                {
                    refFader.StartCoroutine(refFader.FadeOut());
                    //yield return new WaitForSeconds(2);
                }
                
                

                while ((this.transform.position - moveCamera[i].targetB.position).magnitude >= distance)
                {
                    speed += Time.deltaTime;
                    this.transform.position = Vector3.Lerp(moveCamera[i].targetA.position, moveCamera[i].targetB.position, speed / moveCamera[i].timeExec);
                    //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, cubeList[i].transform.rotation, speed / moveCamera[i].timeExec);
                    this.transform.LookAt(moveCamera[i].targetToLook);
                    yield return null;
                }
                if (moveCamera[i].fadeIn)
                {
                    refFader.StartCoroutine(refFader.FadeIn());
                    //yield return new WaitForSeconds(2);
                }
                yield return new WaitForSeconds(moveCamera[i].waitTime);
                speed = 0;
            }
            refFader.StartCoroutine(refFader.FadeOut());
            mainCamera.GetComponent<FreeLookCam>().enabled = true;
            this.gameObject.SetActive(false);
        }
    }
}