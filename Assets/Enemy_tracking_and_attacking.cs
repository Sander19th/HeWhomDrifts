using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
   public UnityEngine.AI.NavMeshAgent agent;

   public Transform player;

   public LayerMask WhatIsGround, whatIsPlayer;

   public float health;

   //Attacking
   public float timeBetweenAttacks;
   bool alreadyAttacked;
   public GameObject projectile;

   //States
   public float sightRange, attackRange;
   public bool playerInSightRange, playerInAttackRange;

   private void Awake(){
	   player = GameObject.Find("PlayerableCharacter").transform;
	   agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
   }

    private void Update(){
		//Check for sight and attack range
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

		//Exclamation indicates false statement
		if (!playerInSightRange && !playerInAttackRange) Idle();
		if (playerInSightRange && !playerInAttackRange) Alert();
		if (playerInSightRange && playerInAttackRange) AttackPlayer();
   }
      
	private void Idle(){
		agent.SetDestination(transform.position);
	}

   	private void Alert(){
		agent.SetDestination(transform.position);

		transform.LookAt(player);
	}

   private void AttackPlayer(){
	   agent.SetDestination(transform.position);

		transform.LookAt(player);

		if (!alreadyAttacked){
			//Attack code
			Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

			//
			alreadyAttacked = true;
			Invoke(nameof(ResetAttack), timeBetweenAttacks);
		}
   }

   private void ResetAttack(){
	   alreadyAttacked = false;
   }

	//Enemy damage simulation
   public void TakeDamage(int damage){
	   health -= damage;

	   if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
   }

   private void DestroyEnemy(){
	   Destroy(gameObject);
   }
}
