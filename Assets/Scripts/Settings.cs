using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UnityStandardAssets.ImageEffects
{
    public class Settings : MonoBehaviour
    {
        public VignetteAndChromaticAberration refVignette;
        public ScreenSpaceAmbientOcclusion refSSAO;
        public BloomOptimized refBloom;
		public Slider audioSlider;
		public GameObject gameManager;
		public GameObject saveText;
		public GameObject loadText;

        private void Start()
        {
			refVignette = Camera.main.GetComponent<VignetteAndChromaticAberration>();
			refSSAO = Camera.main.GetComponent<ScreenSpaceAmbientOcclusion>();
			refBloom = Camera.main.GetComponent<BloomOptimized>();
			AudioListener.volume = audioSlider.value;
			gameManager = GameManager.Self.gameObject;
			loadText = GameObject.FindGameObjectWithTag ("Fader").transform.GetChild (1).gameObject;
        }

        public void ActiveEffect(int _index)
        {
            switch (_index)
            {
                case 0:
                    refVignette.enabled = !refVignette.enabled;
                    break;
                case 1:
                    refSSAO.enabled = !refSSAO.enabled;
                    break;
                case 2:
                    refBloom.enabled = !refBloom.enabled;
                    break;
            }          
        }

        public void ButtonSwitch(Toggle tog)
        {
            tog.isOn = !tog.isOn;
        }

		public void MusicVolume () {
			AudioListener.volume = audioSlider.value;
		}


		public void SaveFunction (int slotN) {
			GameManager.Self.transform.GetComponent<SaveData> ().Save (slotN);
			saveText.gameObject.SetActive (true);
			this.gameObject.GetComponent<MenuController> ().Resume ();
			StartCoroutine(EndSave());
		}


		public IEnumerator EndSave () {
			yield return new WaitForSeconds (3f);
			saveText.gameObject.SetActive (false);
		}


		public void LoadFunction (int slotN) {

			GameObject.FindGameObjectWithTag ("Fader").GetComponent<Fader> ().StartFadeOut ();
			loadText.SetActive (true);
			GameManager.Self.transform.GetComponent<SaveData> ().Load (slotN);

		}

		/*
		public IEnumerator EndLoad () {
			yield return new WaitForSeconds (2f);
			loadText.gameObject.SetActive (false);
		}*/


    }
}