using UnityEngine;

public class AudioManager : MonoBehaviour
{
[Header("----------------- Audio Source -----------------")]
  [SerializeField] AudioSource musicSource; //Music is played using the AudioSource and is picked up by AudioListener component attached to the camera
  [SerializeField] AudioSource SFXSource; //Sound is played using the AudioSource and is picked up by AudioListener component attached to the camera


 [Header("----------------- AUDIO ------------------------")]

  [Header("----------------- Player Clip --------------------")]
	public AudioClip background;
	public AudioClip playerDeath;
	public AudioClip cannonShoot;
	public AudioClip cannonReload;
	public AudioClip playerDriftingSound;

  [Header("----------------- Enemy Clip --------------------")]
	public AudioClip enemyAttack1;
	public AudioClip enemyDeath1;


	public void PlaySFX(AudioClip clip){
		//Making it public allows it to be accessed from other scripts, thereby playing the desired SFX
		SFXSource.PlayOneShot(clip);
	}
}



