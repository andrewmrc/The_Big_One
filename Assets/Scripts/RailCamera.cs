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
		public bool loop;
        public float distance = 1f;
        [Range(0,1)]
        public float speed = 0;
        public GameObject mainCamera;
		GameObject realMainCamera;
		GameObject player;

        Fader refFader;
		bool fadeNow;

        void OnEnable()
        {
			GameObject pivot = mainCamera.transform.GetChild (0).gameObject;
			realMainCamera = pivot.transform.GetChild (0).gameObject;
			mainCamera.SetActive (false);

			mainCamera.GetComponent<FreeLookCam> ().resetAngle = true;

            refFader = FindObjectOfType<Fader>();
			player = GameObject.FindGameObjectWithTag ("Player");
			player.GetComponent<Animator>().SetFloat("Forward", 0);
			player.GetComponent<FSMLogic> ().enabled = false;
			player.GetComponent<CharController> ().enabled = false;
			player.transform.rotation = new Quaternion (0f, 0f, 0f, 0f);

			StartCoroutine(RailCameraCO());
        }

        private IEnumerator RailCameraCO()
        {
            mainCamera.GetComponent<FreeLookCam>().enabled = false;
			realMainCamera.SetActive (false);

            for (int i = 0; i < moveCamera.Length; i++)
            {
                if (moveCamera[i].fadeOut)
                {
                    refFader.StartCoroutine(refFader.FadeOut());
                    //yield return new WaitForSeconds(2);
                }
                
                
				if (moveCamera [i].targetB != null) {
					fadeNow = false;


					while ((this.transform.position - moveCamera [i].targetB.position).magnitude >= distance) {
						speed += Time.deltaTime;

						//Fa partire il fade prima di arrivare a destinazione.
						if((speed > (moveCamera [i].timeExec - 1)) && !fadeNow){
							//Debug.Log ("FADE ANTICIPATO");
							fadeNow = true;
							if (moveCamera[i].fadeIn)
							{
								refFader.StartCoroutine(refFader.FadeIn());
								//yield return new WaitForSeconds(2);
							}
						}

						this.transform.position = Vector3.Lerp (moveCamera [i].targetA.position, moveCamera [i].targetB.position, speed / moveCamera [i].timeExec);
						//this.transform.rotation = Quaternion.Slerp(this.transform.rotation, cubeList[i].transform.rotation, speed / moveCamera[i].timeExec);
						this.transform.LookAt (moveCamera [i].targetToLook);
						yield return null;
					}
				} else {
					this.transform.position = moveCamera [i].targetA.position;
					this.transform.LookAt (moveCamera [i].targetToLook);
					while ((speed <= moveCamera [i].timeExec)) {
						speed += Time.deltaTime;
						yield return null;
					}
					if (moveCamera[i].fadeIn)
					{
						refFader.StartCoroutine(refFader.FadeIn());
					}
				}

				/*
                if (moveCamera[i].fadeIn)
                {
                    refFader.StartCoroutine(refFader.FadeIn());
                    //yield return new WaitForSeconds(2);
                }*/


                yield return new WaitForSeconds(moveCamera[i].waitTime);
                speed = 0;
            }

			AfterCinematic ();

        }


		public void AfterCinematic () {
			if (loop) {
				//Debug.Log ("LOOP ATTIVO");
				StartCoroutine(RailCameraCO());
			} else {
				refFader.StartCoroutine (refFader.FadeOut ());
				
				realMainCamera.SetActive (true);
				mainCamera.SetActive (true);
                mainCamera.GetComponent<FreeLookCam>().enabled = true;

                this.gameObject.SetActive (false);
				player.GetComponent<FSMLogic> ().enabled = true;

			}
		}

    }
}