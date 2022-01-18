using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    [SerializeField]
    private float aggroRange = 5f;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private int targetInRangeDelay = 5;

    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        // Avoid unnecessary calls each frame.
        StartCoroutine(WaitforSeconds(targetInRangeDelay));

        if (CheckIfTargetInRange())
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        float step = enemy.Speed * Time.deltaTime;
        target.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    private bool CheckIfTargetInRange()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < aggroRange)
        {
            return true;
        }

        return false;
    }

    private IEnumerator WaitforSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
