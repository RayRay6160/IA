using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBuilder : MonoBehaviour {

    #region StageParameter

    [SerializeField] Vector3 stageSize;

    float XDiameter => stageSize.x / 2;
    float YDiameter => stageSize.y / 2;
    float ZDiameter => stageSize.z / 2;

    float x, y, z;

    #endregion

    #region GhostManager

    GhostManager ghostManager;

    [SerializeField] LayerMask ghostMask;

    [SerializeField] int ghostAmount;
    [SerializeField] float distanceBetweenGhost;

    List<Vector3> ghostSpawnPoints = new List<Vector3>();

    Vector3 spawnPoint;

    #endregion

    #region Gizmos

    [SerializeField] bool showGizmos;
    Vector3 StageCentre => new Vector3(transform.position.x, transform.position.y + stageSize.y / 2, transform.position.z);
    Vector3 StageMidPoint(float x, float y, float z) => new Vector3(StageCentre.x + x, StageCentre.y + y, StageCentre.z + z);

    #endregion

    void Awake() {
        ghostManager = GetComponentInChildren<GhostManager>();
    }

    void Start() {
        BuildStage();
        Timer.Instance.StartTimer(5);
    }

    //build stage
    //-->generate ghost with random position inside stage
    void BuildStage() {

        //Keep distance between every ghost
        for (int i = 0;i < ghostAmount;i++) {

            spawnPoint = RandomPosInStage();

            if (ghostSpawnPoints.Count == 0) { //first add of spawn points list
                ghostSpawnPoints.Add(spawnPoint);
                i++;
                continue;
            }

            for (int j = 0;j < ghostSpawnPoints.Count;j++) {
                if ((spawnPoint - ghostSpawnPoints[j]).sqrMagnitude > distanceBetweenGhost * distanceBetweenGhost) { //spawn point didn't overlap other ghost
                    if (j == ghostSpawnPoints.Count - 1) {
                        ghostSpawnPoints.Add(spawnPoint);
                    }
                    continue; //keep checking
                }
                break;
            }
        }

        //Generate ghost
        foreach (Vector3 spawnPoint in ghostSpawnPoints) {
            PoolManager.Release(ghostManager.PickRandomGhost(), spawnPoint);
        }
        Debug.Log("Successful generate " + ghostSpawnPoints.Count + " Ghost!");
    }

    Vector3 RandomPosInStage() { //return a random position inside stage

        x = Random.Range(transform.position.x - XDiameter, transform.position.x + XDiameter);
        y = Random.Range(transform.position.y, transform.position.y + YDiameter * 2);
        z = Random.Range(transform.position.z - ZDiameter, transform.position.z + ZDiameter);

        return new Vector3(x, y, z);
    }

    void OnDrawGizmosSelected() {

        if (!showGizmos)
            return;

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(StageCentre, stageSize);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(StageMidPoint(-XDiameter, 0, 0), StageMidPoint(XDiameter, 0, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(StageMidPoint(0, -YDiameter, 0), StageMidPoint(0, YDiameter, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(StageMidPoint(0, 0, -ZDiameter), StageMidPoint(0, 0, ZDiameter));

    }
}