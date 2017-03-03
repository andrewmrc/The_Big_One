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

        private void Start()
        {
            refVignette = FindObjectOfType<VignetteAndChromaticAberration>();
            refSSAO = FindObjectOfType<ScreenSpaceAmbientOcclusion>();
            refBloom = FindObjectOfType<BloomOptimized>();
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
    }
}