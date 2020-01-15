using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public GameObject QuestMarker;

    public void UpdateQuestMarker(int x, int y) //call this from main quest registry
    {
        QuestMarker.transform.localPosition = new Vector3(300 + 48 * x, 100 + 36 * y, 0);
    }

    /* use the following x and y coordinates when placing the scene-specific player marker
     *       1       2       3
     *   +-------+-------+-------+
     * 5 |348,280|396,280|       |
     *   +-------+-------+-------+
     * 4 |348,244|396,244|       |
     *   +-------+-------+-------+
     * 3 |348,208|396,208|       |
     *   +-------+-------+-------+
     * 2 |       |396,172|448,172|
     *   +-------+-------+-------+
     * 1 |       |396,136|448,136|
     *   +-------+-------+-------+
     */
}