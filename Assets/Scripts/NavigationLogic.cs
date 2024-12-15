using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationLogic : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    [SerializeField]
    private GameObject wpParent;

    private List<Vector3> waypoints;
    private int currentWp;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializPatroleZone();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWaypoint();
       
    }
        
    /// Initialise la zone de patrouille en r�cup�rant tous les points de passage de l'objet parent sp�cifi�.
    public void InitializPatroleZone()
    {
        
        waypoints = new List<Vector3>();
        foreach (Transform t in wpParent.GetComponentsInChildren<Transform>())
        {
            waypoints.Add(t.position);
        }
        waypoints.Remove(wpParent.transform.position);
        
        currentWp = 0;
        agent.SetDestination(waypoints[currentWp]);
    }

    /// Met � jour le point de passage actuel lorsque l'agent atteint sa destination.
    public void UpdateWaypoint() {
        if (waypoints.Count > 1) {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                currentWp = ++currentWp % waypoints.Count;
                agent.SetDestination(waypoints[currentWp]);
                agent.speed = 5;
            }
        }
    }

    /// D�finit la destination de l'agent � une position proche de la position cible sp�cifi�e.
    public void SetPosition(Vector3 position) {
        Vector3 directionToPlayer = (position - transform.position).normalized;
        Vector3 newPosition = position - directionToPlayer * 5f;
        agent.SetDestination(newPosition);
    }
}
