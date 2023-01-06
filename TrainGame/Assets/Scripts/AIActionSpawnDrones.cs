using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.TopDownEngine
{
	/// <summary>
	/// An AIACtion used to have an AI kill itself
	/// </summary>
	[AddComponentMenu("TopDown Engine/Character/AI/Actions/AIActionSpawnDrones")]
	public class AIActionSpawnDrones : AIAction
	{
		public bool OnlyRunOnce = true;
		public GameObject drone;
		protected Character _character;
		protected Health _health;
		protected bool _alreadyRan = false;

		/// <summary>
		/// On init we grab our Health
		/// </summary>
		public override void Initialization()
		{
			if (!ShouldInitialize) return;
			base.Initialization();
			_character = this.gameObject.GetComponentInParent<Character>();
			_health = _character.CharacterHealth;
		}

		/// <summary>
		/// Kills the AI
		/// </summary>
		public override void PerformAction()
		{
			if (OnlyRunOnce && _alreadyRan)
			{
				return;
			}
			Instantiate(drone, transform.position + new Vector3(0, 10, 0) , drone.transform.rotation);
			
		}

		/// <summary>
		/// On enter state we reset our flag
		/// </summary>
		public override void OnEnterState()
		{
			base.OnEnterState();
			_alreadyRan = false;
		}
	}
}