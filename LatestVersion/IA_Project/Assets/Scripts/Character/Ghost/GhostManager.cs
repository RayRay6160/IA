using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour {

    [SerializeField] GhostData[] ghosts;
    float totalWeight;

    float pick;

    void Awake() {
        Initialize();
    }

    void Initialize() {
        //initialize total weight of all ghost
        for (int i = 0;i < ghosts.Length;i++) {
            totalWeight += ghosts[i].weight;
        }
    }

    public GameObject PickRandomGhost() { //pick one ghost in ghosts array with weight
        pick = Random.Range(0, totalWeight);
        for (int i = 0;i < ghosts.Length;i++) {
            if (pick > ghosts[i].weight) {
                pick -= ghosts[i].weight;
                continue;
            }
            return ghosts[i].ghost;
        }
        return null;
    }
}

[System.Serializable]
public class GhostData {

    public GameObject ghost;
    public float weight;

}