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

        private void Start()
        {
			refVignette = Camera.main.GetComponent<VignetteAndChromaticAberration>();
			refSSAO = Camera.main.GetComponent<ScreenSpaceAmbientOcclusion>();
			refBloom = Camera.main.GetComponent<BloomOptimized>();
			AudioListener.volume = audioSlider.value;
			gameManager = GameManager.Self.gameObject;
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
		}


		public void LoadFunction (int slotN) {
			GameManager.Self.transform.GetComponent<SaveData> ().Load (slotN);
		}
    }
}