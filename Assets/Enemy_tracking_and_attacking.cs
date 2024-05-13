using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UintyEngine.AI;

public class EnemyAI : MonoBehaviour
{
   public NavMeshAgent agent;

   public Transform player;

   public LayerMask WhatIsGround, WhatIsPlayer;

   //Attacking
   public float timeBetweenAttacks;
   bool alreadyAttacked;

   //States
   public float sightRange, attackRange;
   public bool playerInSightRange, playerInAttackRange;

   private void Awake(){
	   player = GameObject.Find("PlayerableCharacter").transform;
	   agent = GetComponent<NavMeshAgent>();
   }

    private void Update(){
		//Check for sight and attack range
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

		//Exclamation is for false statement 
		if (!playerInSightRange && !playerInAttackRange) Idle();
		if (playerInSightRange && !playerInAttackRange) Idle();

   }

   private void Idle(){

   }

   private void Attacking(){

   }

    private void Attacking(){

   }

}
