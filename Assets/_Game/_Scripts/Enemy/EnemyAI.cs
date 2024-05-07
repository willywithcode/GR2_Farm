using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    enum State
    {
        Roaming,
        ChaseTarget, 
        GoBackStart
    }
    State state;
    Rigidbody2D rb;
    EnemyController EnemyController;
    public bool Roaming;
    public Seeker seeker;
    Path path;
    Coroutine MoveCoroutine;
    public float nextWayPointDistance = 2f;
    [SerializeField] float MoveSpeed;
    [SerializeField] float SpeedAttack;
    Vector2 moveDir;
    Vector3 StartingPos;
    public float targetRange = 3f;
    public float StopChaseDistance;
    Vector3 target;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        EnemyController = GetComponent<EnemyController>();
    }
    private void Start()
    {
        state = State.Roaming;
        StartingPos = transform.position;
        StartCoroutine(RoamingRoutine());
    }
    public void CaculatePath()
    {
        target = GameManager.Instance.PlayerTF.position;
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target, OnCompletePath);
        }
    }

    void OnCompletePath(Path p)
    {
        if (!p.error)
        {
            path = p;
            MovetoTarget();
        }
    }
    private void FixedUpdate()
    {
        if (EnemyController.knockBack.GettingKnockedBack || EnemyController.IsDeath)
        {
            return;
        }
        
        switch (state)
        {
            case State.Roaming:
                rb.MovePosition(rb.position + moveDir * MoveSpeed * Time.fixedDeltaTime);
                FindTarget();
                break;

            case State.ChaseTarget:
                rb.MovePosition(rb.position + moveDir * SpeedAttack * Time.fixedDeltaTime);
                if (Vector3.Distance(transform.position, GameManager.Instance.PlayerTF.position) > StopChaseDistance)
                {
                    BackToStart();
                }
                break;
            case State.GoBackStart:
                rb.MovePosition(rb.position + moveDir * MoveSpeed * Time.fixedDeltaTime);
                FindTarget();
                transform.position = Vector3.MoveTowards(transform.position, StartingPos, MoveSpeed * Time.deltaTime);
                if(Vector3.Distance(transform.position, StartingPos) < 0.01f)
                {
                    ChangeStateRoaming();
                }
                break;
        }

    }

    IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamingDir = GetRandomDir();
            MoveTo(roamingDir);
            yield return new WaitForSeconds(1f);
        }
    }

    public Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
    private void FindTarget()
    {
        
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        if(Vector3.Distance(transform.position, playerPos) < targetRange)
        {
            state = State.ChaseTarget;
            EnemyController.ChangeAnim("Chase");
            InvokeRepeating("CaculatePath", 0f, 0.5f);
        }
    }

    public void MoveTo(Vector2 targetPos)
    {
        moveDir = targetPos;
    }

    void MovetoTarget()
    {
        if (MoveCoroutine != null)
        {
            StopCoroutine(MoveCoroutine);
        }
        StartCoroutine(MoveToTargetCoroutine());
    }

    void BackToStart()
    {
        state = State.GoBackStart;
        EnemyController.ChangeAnim("Roam");
        CancelInvoke("CaculatePath");
    }
    void ChangeStateRoaming()
    {
        state = State.Roaming;
        EnemyController.ChangeAnim("Roam");
        StartCoroutine(RoamingRoutine());
    }
    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        while (currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;           
            MoveTo(direction);
            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < nextWayPointDistance)
                currentWP++;
            yield return null;
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {

    //        Player player = collision.GetComponent<Player>();
    //        SoundManager.Instance.OnTakeDamage();
    //        player.TakeDamage(20);
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Player player = collision.GetComponent<Player>();
            SoundManager.Instance.OnTakeDamage();
            player.TakeDamage(20);
        }
    }
}
