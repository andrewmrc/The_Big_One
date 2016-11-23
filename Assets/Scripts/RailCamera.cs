using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Cameras {

    public class RailCamera : MonoBehaviour {

        private byte i = 0;
        public float distance = 1f;
        public float waitTime = 1f;
        public float speed;
        public GameObject mainCamera;
        public List<Transform> cubeList = new List<Transform>(5);
        public List<Transform> listTarget = new List<Transform>();
        Fader refFader;

        void Start()
        {
           
            refFader = FindObjectOfType<Fader>();
            StartCoroutine(railCamera());
        }

        private IEnumerator railCamera()
        {
            mainCamera.GetComponent<FreeLookCam>().enabled = false;
            Vector3 currentPos = this.transform.position;

            while (true)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, cubeList[i].position, speed * 0.05f);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, cubeList[i].transform.rotation, speed * 0.05f);

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
                    i++;

                    // Opzionale... Quando devo fare l'ultima transizione abbasso il tempo di attesa a 0
                    if (i == cubeList.Count -1)
                        waitTime = 0f;

                    if (i >= cubeList.Count)
                    {
                        
                        Debug.Log("Dovrebbe iniziare ORA");
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