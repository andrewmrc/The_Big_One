using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Acutize {
	public class PowerBarHandler : MonoBehaviour 
	{

		public Image powerBarImage;
		public float speed = 5f;
		public float currentPower;
		//public bool isCooldown = false;
		//AudioSource asource;
		//public bool playsound = true;

		void Start()
		{
			currentPower = GameManager.Self.powerQuantity;
			powerBarImage.fillAmount = currentPower/100f;
		}

		/*
		public void StartCooldown(float powerInSeconds)
		{
			speed = powerInSeconds;
			currentPower= GameManager.Self.powerQuantity;
		}

		public void Reset(){
			currentPower = 0;
		}

		public float GetPowerLeft(){
			return speed-currentPower;
		}*/

		void Update () 
		{
			currentPower = GameManager.Self.powerQuantity;
			powerBarImage.fillAmount = (currentPower/100); 

			if (!GameManager.Self.outOfYourBody && currentPower < 100)
			//if(isCooldown)
			{
				Debug.Log ("Cooldown");
				//Increase fill amount
				GameManager.Self.powerQuantity+=Time.deltaTime*speed;
				//1.0f/speed * Time.deltaTime;
				/*
				if (playsound && speed-currentPower<5) {
					playsound=false;
					if (asource!=null) asource.Play();
				}*/
				//Debug.Log ("Fill Amount: " + power.fillAmount);
			}

		}
	}
}