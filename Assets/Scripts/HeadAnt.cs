using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HeadAnt : MonoBehaviour
{
    int numberOfAnts = 0;
    bool onAntTimeout = false;
    bool onEnemyTimeout = false;
    bool onHeartTimeout = false;

    public float maxHitPoints;
    public float startingHitPoints;

    public HitPoints hitPoints;
    public HealthBar healthBar;

    public AudioSource audioSource;
    public AudioClip waspNoise;
    public AudioClip antYay;
    public AudioClip rockBreak;
    public AudioClip heartDing;

    public TextMeshProUGUI notEnoughAnts; 
    public TextMeshProUGUI numberOfAntsDisplay;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints.value = startingHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        numberOfAntsDisplay.text = numberOfAnts + " Ants Found!";
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("UnrecruitedAnt") && !onAntTimeout) {
            audioSource.PlayOneShot(antYay);
            RecruitableAnt recruitableAnt = other.gameObject.GetComponent<Consumable>().ant;

            if(recruitableAnt != null) {
                other.gameObject.SetActive(false);
                
                numberOfAnts++;
            }
            StartCoroutine(addAntCooldown());
        }

        if (other.gameObject.CompareTag("heart") && !onHeartTimeout) {
            audioSource.PlayOneShot(heartDing);
            Heart recruitableAnt = other.gameObject.GetComponent<Consumable>().heart;

            if(recruitableAnt != null) {
                other.gameObject.SetActive(false);
                hitPoints.value += 10;

            }
            StartCoroutine(addHeartCooldown());
        }

        if (other.gameObject.CompareTag("rock")) {
            Rock rock = other.gameObject.GetComponent<Consumable>().rock;

            if(rock != null) {
                if (numberOfAnts >= 10) {
                    audioSource.PlayOneShot(rockBreak);
                    other.gameObject.SetActive(false);
                    StartCoroutine(waitThenBreak());
                } else {
                    notEnoughAnts.text = "Not enough ants! Get out there and find more!";
                    StartCoroutine(notEnoughAntsTextRemoval());
                }
            }
        }

        if(other.gameObject.CompareTag("wasp") && !onEnemyTimeout) {
            audioSource.PlayOneShot(waspNoise);
            hitPoints.value -= 20;

            if (hitPoints.value <= 0) {
                SceneManager.LoadScene("Lose Screen");
            }
            StartCoroutine(addEnemyCooldown());
        }
    }

    IEnumerator addEnemyCooldown() {
        onEnemyTimeout = true;
        yield return new WaitForSeconds(2);
        onEnemyTimeout = false;
    }

    IEnumerator addAntCooldown() {
        onAntTimeout = true;
        yield return new WaitForSeconds(1);
        onAntTimeout = false;
    }

    IEnumerator addHeartCooldown() {
        onHeartTimeout = true;
        yield return new WaitForSeconds(1);
        onHeartTimeout = false;
    }

    IEnumerator waitThenBreak() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Win Screen");
    }

    IEnumerator notEnoughAntsTextRemoval() {
        yield return new WaitForSeconds(4);
        notEnoughAnts.text = "";
    }
}
