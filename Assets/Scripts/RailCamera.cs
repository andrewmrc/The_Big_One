using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Cameras
{

    [System.Serializable]
    public class RailContainer
    {
        public Transform targetA, targetB;
        public Transform targetToLook;
        [Tooltip("Mercuri è un coglione")]
        public bool fadeIn, fadeOut;
        public float timeExec, waitTime;
    }


    public class RailCamera : MonoBehaviour {

        public RailContainer[] moveCamera;

        private byte i = 0;
        public float distance = 1f;
        public float waitTime = 1f;
        public float time;
        [Range(0,1)]
        public float speed = 0;
        public GameObject mainCamera;
        public List<Transform> cubeList = new List<Transform>(5);
        public List<Transform> listTarget = new List<Transform>();
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
                    //Vector3 currentPos = this.transform.position;

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

        private IEnumerator railCamera()
        {
            mainCamera.GetComponent<FreeLookCam>().enabled = false;
            Vector3 currentPos = this.transform.position;

            while (true)
            {
                speed += Time.deltaTime;
                this.transform.position = Vector3.Lerp(currentPos, cubeList[i].position, speed / time);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, cubeList[i].transform.rotation, speed / time);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    yield break;
                }

                // Se la distanza tra la camera ed i cubi è MAGGIORE della distance continua a muoverti
                if ((this.transform.position - cubeList[i].position).magnitude >= distance)
                {
                    //this.transform.LookAt(listTarget[i]);
                    yield return null;
                }

                // Se la distanza tra la camera ed i cubi è MINORE della distance aspetta waitTime
                // punta al nuovo target (i++) 
                else
                {
                    yield return new WaitForSeconds(waitTime);
                    currentPos = this.transform.position;
                    i++;
                    speed = 0;

                    // Opzionale... Quando devo fare l'ultima transizione abbasso il tempo di attesa a 0
                    if (i == cubeList.Count -1)
                        waitTime = 0f;

                    if (i >= cubeList.Count)
                    {
                        
                        // Esegue il FadeIn in Fader
                        refFader.StartCoroutine(refFader.FadeIn());
                        yield return new WaitForSeconds(2);

                        // Esegue il FadeOut in Fader e riattiva la MainCamera
                        refFader.StartCoroutine(refFader.FadeOut());
                        mainCamera.GetComponent<FreeLookCam>().enabled = true;

                        // Infine spengo la RailCamera
                        this.gameObject.SetActive(false);
                        yield break;
                    }
                }

            }
        }     

       
    }
}