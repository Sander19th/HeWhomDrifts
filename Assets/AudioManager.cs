using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------------- Audio Source -----------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

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
    public AudioClip enemyDeath2;
    public AudioClip enemyDeath3;
    public AudioClip enemyDeath4; // Yodel sound

    public float enemyDeathVolume = 0.5f; // Volume for enemy death sounds
    public float woodImpactVolume = 0.5f; // Volume for wood impact sounds

    // Existing method to play a specific SFX
    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        SFXSource.PlayOneShot(clip, volume);
    }

    // New method to play a random enemy death sound with volume and rarity adjustments
    public void PlayRandomEnemyDeathSound()
    {
        // Array of enemy death sounds with higher frequency for common sounds
        AudioClip[] enemyDeathSounds = {
            enemyDeath1, enemyDeath2,
            enemyDeath1, enemyDeath2,
            enemyDeath1, enemyDeath2, 
            enemyDeath1, enemyDeath2, 
            enemyDeath1, enemyDeath2, 
            enemyDeath1, enemyDeath2,
            enemyDeath1, enemyDeath2,
            enemyDeath1, enemyDeath2,
            enemyDeath4, enemyDeath3, // Add the rare sound less frequently
        };

        if (enemyDeathSounds.Length > 0)
        {
            // Select a random clip from the array
            int randomIndex = Random.Range(0, enemyDeathSounds.Length);
            AudioClip randomClip = enemyDeathSounds[randomIndex];

            // Play the selected clip with the specified volume
            PlaySFX(randomClip, enemyDeathVolume);
        }
    }

    [Header("----------------- Environment Clip --------------------")]
    public AudioClip woodImpact1;
    public AudioClip woodImpact2;
    public AudioClip woodImpact3;
    public AudioClip woodImpact4;

     // New method to play a random wood impact sound with volume and rarity adjustments
    public void PlayRandomWoodImpactSound()
    {
        // Array of wood impact sounds with higher frequency for common sounds
        AudioClip[] woodImpactSounds = {
            woodImpact1, woodImpact2, woodImpact3, woodImpact4,
            woodImpact1, woodImpact2, woodImpact3, woodImpact4,
            woodImpact1, woodImpact2, woodImpact3, woodImpact4,
             // Add the rare sound less frequently
        };

        if (woodImpactSounds.Length > 0)
        {
            // Select a random clip from the array
            int randomIndex = Random.Range(0, woodImpactSounds.Length);
            AudioClip randomClip = woodImpactSounds[randomIndex];

            // Play the selected clip with the specified volume
            PlaySFX(randomClip, woodImpactVolume);
        }
    }
}




