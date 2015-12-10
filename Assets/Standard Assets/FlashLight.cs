// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class FlashLight : MonoBehaviour {


public Light flashlightLightSource;
public bool  lightOn = true;
public float lightDrain = 0.1f;
private static float batteryLife = 0.0f;
public float maxBatteryLife = 2.0f;


private static float batteryPower = 1;

public float barDisplay = 0;
public Vector2 pos = new Vector2(20,40);
public Vector2 size = new Vector2(60, 20);
public Texture2D progressBarEmpty;
public Texture2D progressBarFull;

public AudioClip soundTurnOn;
public AudioClip soundTurnOff;


void  Start (){
	batteryLife = maxBatteryLife;
	flashlightLightSource = GetComponent<Light>();
}


public void  Recharge(){
    batteryLife = maxBatteryLife;
}



void  Update (){
	if(lightOn && batteryLife >= 0)
	{
		batteryLife -= Time.deltaTime * lightDrain;
	}
		if(lightOn && batteryLife <= 0.4f)
	{
		flashlightLightSource.GetComponent<Light>().intensity = 5;
	}
		if(lightOn && batteryLife <= 0.3f)
	{
		flashlightLightSource.GetComponent<Light>().intensity = 4;
	}
	if(lightOn && batteryLife <= 0.2f)
	{
		flashlightLightSource.GetComponent<Light>().intensity = 3;
	}
	if(lightOn && batteryLife <= 0.1f)
	{
		flashlightLightSource.GetComponent<Light>().intensity = 2;
	}
	if(lightOn && batteryLife <= 0)
	{
	    flashlightLightSource.GetComponent<Light>().intensity = 0;
        //TODO: Trigger Death
	}

    barDisplay = batteryLife;
	
	if(batteryLife <= 0)
	{
		batteryLife = 0;
		lightOn = false;
	}
	
	//if(Input.GetKeyDown(KeyCode.F))
    if(Input.GetButtonDown("Flashlight"))
	{
		toggleFlashlight();
		toggleFlashlightSFX();
		
		if(lightOn)
		{
			lightOn = false;
		}
		else if (!lightOn && batteryLife >= 0)
		{
			lightOn = true;
		}
	}

        //RaycastHit hit;
        //if (Physics.Raycast(flashlightLightSource.transform.position, Vector3.forward, out hit))
        //{
        //    if (hit.collider.gameObject.tag == "Monster")
        //    {
        //            var monster = hit.collider.gameObject.GetComponent<MonsterAI>();
        //            monster.HitByFlashlight();

        //    }
        //}
        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));
        RaycastHit hit; 
        if(lightOn)
        {
            if (Physics.Raycast(ray, out hit, 50) && hit.collider.tag == "Enemy")
            {
                GameObject Monster = hit.transform.gameObject;
                Debug.Log("Monster hit!");
                var monster = Monster.GetComponent<MonsterAI>();
                monster.HitByFlashlight();
            }
        }
    }
	

void  OnGUI (){
 
        // draw the background:
        GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
        GUI.Box ( new Rect(0,0, size.x, size.y),progressBarEmpty);
 
        // draw the filled-in part:
        GUI.BeginGroup (new Rect (0, 0, size.x * barDisplay, size.y));
        GUI.Box ( new Rect(0,0, size.x, size.y),progressBarFull);
        GUI.EndGroup ();
 
        GUI.EndGroup ();
} 
 	
void  toggleFlashlight (){
	if(lightOn)
	{
		flashlightLightSource.enabled = false;
	}
	else
	{
		flashlightLightSource.enabled = true;
	}
}
void  toggleFlashlightSFX (){
	if(flashlightLightSource.enabled)
	{
		GetComponent<AudioSource>().clip = soundTurnOn;
	}
	else
	{
		GetComponent<AudioSource>().clip = soundTurnOff;
	}
	GetComponent<AudioSource>().Play();
	
}	
}