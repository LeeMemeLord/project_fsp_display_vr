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
        
    /// Initialise la zone de patrouille en récupérant tous les points de passage de l'objet parent spécifié.
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

    /// Met à jour le point de passage actuel lorsque l'agent atteint sa destination.
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

    /// Définit la destination de l'agent à une position proche de la position cible spécifiée.
    public void SetPosition(Vector3 position) {
        Vector3 directionToPlayer = (position - transform.position).normalized;
        Vector3 newPosition = position - directionToPlayer * 5f;
        agent.SetDestination(newPosition);
    }
}
