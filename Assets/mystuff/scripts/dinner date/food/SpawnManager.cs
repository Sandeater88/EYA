using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Include the TextMeshPro namespace

public class SpawnManager : MonoBehaviour
{
    public GameObject[] Atest; // Array to hold the Atest prefabs to spawn
    public GameObject[] Btest; // Array to hold the Btest prefabs to spawn
    public Vector3 spawnLocation; // Location where the prefab will be spawned
    public Canvas canvas; // Canvas to display the hover text
    public HPbar HPbar; // Reference to the HPbar script
    public TextMeshProUGUI hoverText; // Reference to the TextMeshPro text for hover text
    public int fontSize = 24; // Adjustable font size for hover text

    private GameObject currentPrefab; // The currently spawned prefab
    private Font customFont; // Reference to the custom font

    private float healthIncrement = 10f; // Health increment per correct action

    void Start()
    {
        // Check if the prefabs arrays have been set in the inspector
        if (Atest.Length == 0 || Btest.Length == 0)
        {
            Debug.LogError("Atest or Btest prefabs arrays not assigned in the inspector!");
            return;
        }

        // Load the custom font from the Resources folder (if needed)
        customFont = Resources.Load<Font>("Fonts/JazzCreateBubble"); // Adjust the path as necessary

        // Set up the hover text properties
        hoverText.fontSize = fontSize;
        hoverText.color = Color.red;
        hoverText.alignment = TextAlignmentOptions.Center;
        hoverText.text = "";

        // Start the coroutine to spawn prefabs
        StartCoroutine(SpawnPrefabsWithDelay());
    }

    IEnumerator SpawnPrefabsWithDelay()
    {
        while (true)
        {
            // Decide whether to spawn an Atest or a Btest prefab
            bool spawnBtest = Random.Range(0f, 1f) < 0.5f;

            if (spawnBtest)
                SpawnBtestPrefab();
            else
                SpawnAtestPrefab();

            // Start the coroutine to destroy the prefab after 30 seconds
            StartCoroutine(DestroyPrefabAfterDelay(currentPrefab, 30f));

            // Wait for 30 seconds before spawning the next prefab
            yield return new WaitForSeconds(30f);
        }
    }

    void Update()
    {
        if (currentPrefab != null)
        {
            // Raycast from the cursor position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == currentPrefab)
                {
                    // Display the hover text slightly above the object
                    hoverText.rectTransform.position = Input.mousePosition + new Vector3(0, 40f, 0);
                    hoverText.text = "Answer A or B?";

                    // Check for player input
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        if (IsAtestPrefab(currentPrefab))
                        {
                            HPbar.Heal(healthIncrement);
                        }
                        DestroyCurrentPrefab();
                    }
                    else if (Input.GetKeyDown(KeyCode.B))
                    {
                        if (IsBtestPrefab(currentPrefab))
                        {
                            HPbar.Heal(healthIncrement);
                        }
                        DestroyCurrentPrefab();
                    }
                }
                else
                {
                    // Hide the hover text if the raycast doesn't hit the prefab
                    hoverText.text = "";
                }
            }
            else
            {
                // Hide the hover text if the raycast doesn't hit anything
                hoverText.text = "";
            }
        }
        else
        {
            // Hide the hover text if there's no prefab
            hoverText.text = "";
        }
    }

    void SpawnAtestPrefab()
    {
        int randomIndex = Random.Range(0, Atest.Length);
        currentPrefab = Instantiate(Atest[randomIndex], spawnLocation, Quaternion.identity);
        currentPrefab.tag = "Atest"; // Ensure Atest prefabs have this tag
    }

    void SpawnBtestPrefab()
    {
        int randomIndex = Random.Range(0, Btest.Length);
        currentPrefab = Instantiate(Btest[randomIndex], spawnLocation, Quaternion.identity);
        currentPrefab.tag = "Btest"; // Ensure Btest prefabs have this tag
    }

    void DestroyCurrentPrefab()
    {
        Destroy(currentPrefab);
        currentPrefab = null;
    }

    IEnumerator DestroyPrefabAfterDelay(GameObject prefab, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (prefab != null)
        {
            Destroy(prefab);
            currentPrefab = null;
            HPbar.Heal(healthIncrement);
        }
    }

    bool IsAtestPrefab(GameObject prefab)
    {
        return prefab.CompareTag("Atest");
    }

    bool IsBtestPrefab(GameObject prefab)
    {
        return prefab.CompareTag("Btest");
    }
}
