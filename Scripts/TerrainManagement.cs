using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManagement : MonoBehaviour
{
    public Terrain[] terrains; // Array of your five terrain objects.
    public float deactivatePosition = -1144f;// Left-bound position to trigger terrain changes.
    public float activatePosition = -150f;
    private int currentTerrainIndex = 0; // Index of the currently active terrain.


    void Start()
    {

    }

    void Update()
    {
        // Check the x-position of the current terrain.
        // Check the x-position of the current terrain.
        float currentTerrainX = terrains[currentTerrainIndex].transform.position.x;

        // Check if the current terrain should be deactivated.
        if (currentTerrainX <= deactivatePosition)
        {
            // Deactivate the current terrain.
            DeactivateTerrain(currentTerrainIndex);
            // Increment the terrain index.
            currentTerrainIndex++;
            // Ensure the index stays within bounds.
            if (currentTerrainIndex >= terrains.Length)
            {
                currentTerrainIndex = 0; // Wrap around to the first terrain.
            }

            // Activate the next terrain.
            ActivateTerrain(currentTerrainIndex);
        }

        // Check if the new terrain should be activated.
        if (currentTerrainX <= activatePosition)
        {
            // Determine the index of the terrain to activate next.
            int nextTerrainIndex = currentTerrainIndex + 1;
            if (nextTerrainIndex >= terrains.Length)
            {
                nextTerrainIndex = 0; // Wrap around to the first terrain.
            }
            // Activate the new terrain.
            ActivateTerrain(nextTerrainIndex);
        }
    }

    void ActivateTerrain(int index)
    {
        // Activate the terrain at the specified index.
        terrains[index].gameObject.SetActive(true);
    }

    void DeactivateTerrain(int index)
    {
        // Deactivate the terrain at the specified index.
        terrains[index].gameObject.SetActive(false);
    }
}

