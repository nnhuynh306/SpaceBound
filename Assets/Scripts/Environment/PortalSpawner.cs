using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : Singleton<PortalSpawner>
{
    public string portalResourcePath = "Prefabs/Environment/VictoryPortal";
    // Start is called before the first frame update
    GameObject portalPrefab;

    public Vector2 position = Vector2.zero;

    public void spawn(Vector2 position) {
        Instantiate(getPrefab(), position, Quaternion.identity);
    }

    public void spawn() {
        spawn(position);
    }

    private GameObject getPrefab() {
        if (portalPrefab == null) {
            portalPrefab = Resources.Load<GameObject>(portalResourcePath);
        }

        return portalPrefab;
    }
    
}
