﻿using System.Collections.Generic;
using System.Linq;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.TopDownEngine
{
	/// <summary>
	/// Link this component to a Health component, and it'll be able to process incoming damage through resistances, handling damage reduction/increase, condition changes, movement multipliers, feedbacks and more.
	/// </summary>
	[AddComponentMenu("TopDown Engine/Character/Health/DamageResistanceProcessor")]
	public class DamageResistanceProcessor : MonoBehaviour
	{
		[Header("Damage Resistance List")]
		
		/// If this is true, this component will try to auto-fill its list of damage resistances from the ones found in its children 
		[Tooltip("If this is true, this component will try to auto-fill its list of damage resistances from the ones found in its children")]
		public bool AutoFillDamageResistanceList = true;
		/// If this is true, disabled resistances will be ignored by the auto fill 
		[Tooltip("If this is true, disabled resistances will be ignored by the auto fill")]
		public bool IgnoreDisabledResistances = true;
		
		/// the list of damage resistances this processor will handle. Auto filled if AutoFillDamageResistanceList is true
		[Tooltip("the list of damage resistances this processor will handle. Auto filled if AutoFillDamageResistanceList is true")]
		public List<DamageResistance> DamageResitanceList;

		/// <summary>
		/// On awake we initialize our processor
		/// </summary>
		protected virtual void Awake()
		{
			Initialization();
		}

		/// <summary>
		/// Auto finds resistances if needed and sorts them
		/// </summary>
		protected virtual void Initialization()
		{
			if (AutoFillDamageResistanceList)
			{
				DamageResistance[] foundResistances =
					this.gameObject.GetComponentsInChildren<DamageResistance>(
						includeInactive: !IgnoreDisabledResistances);
				if (foundResistances.Length > 0)
				{
					DamageResitanceList = foundResistances.ToList();	
				}
			}
			SortDamageResistanceList();
		}

		/// <summary>
		/// A method used to reorder the list of resistances, based on priority by default.
		/// Don't hesitate to override this method if you'd like your resistances to be handled in a different order
		/// </summary>
		public virtual void SortDamageResistanceList()
		{
			// we sort the list by priority
			DamageResitanceList.Sort((p1,p2)=>p1.Priority.CompareTo(p2.Priority));
		}
		
		/// <summary>
		/// Processes incoming damage through the list of resistances, and outputs the final damage value
		/// </summary>
		/// <param name="damage"></param>
		/// <param name="typedDamages"></param>
		/// <param name="damageApplied"></param>
		/// <returns></returns>
		public virtual float ProcessDamage(float damage, List<TypedDamage> typedDamages, bool damageApplied)
		{
			float totalDamage = 0f;
			if (DamageResitanceList.Count == 0) // if we don't have resistances, we output raw damage
			{
				totalDamage = damage;
				if (typedDamages != null)
				{
					foreach (TypedDamage typedDamage in typedDamages)
					{
						totalDamage += typedDamage.DamageCaused;
					}
				}
				return totalDamage;
			}
			else // if we do have resistances
			{
				totalDamage = damage;
				
				foreach (DamageResistance resistance in DamageResitanceList)
				{
					totalDamage = resistance.ProcessDamage(totalDamage, null, damageApplied);
				}
				
				if (typedDamages != null) 
				{
					foreach (TypedDamage typedDamage in typedDamages)
					{
						float currentDamage = typedDamage.DamageCaused;
						
						foreach (DamageResistance resistance in DamageResitanceList)
						{
							currentDamage = resistance.ProcessDamage(currentDamage, typedDamage.AssociatedDamageType, damageApplied);
						}
						totalDamage += currentDamage;
					}
				}
				return totalDamage;
			}
		}

		public virtual void SetResistanceByLabel(string searchedLabel, bool active)
		{
			foreach (DamageResistance resistance in DamageResitanceList)
			{
				if (resistance.Label == searchedLabel)
				{
					resistance.gameObject.SetActive(active);
				}
			}
		}

		/// <summary>
		/// When interrupting all damage over time of the specified type, stops their associated feedbacks if needed
		/// </summary>
		/// <param name="damageType"></param>
		public virtual void InterruptDamageOverTime(DamageType damageType)
		{
			foreach (DamageResistance resistance in DamageResitanceList)
			{
				if ( resistance.gameObject.activeInHierarchy &&
					((resistance.DamageTypeMode == DamageTypeModes.BaseDamage) ||
				        (resistance.TypeResistance == damageType))
				    && resistance.InterruptibleFeedback)
				{
					resistance.OnDamageReceived?.StopFeedbacks();
				}
			}
		}

		/// <summary>
		/// Checks if any of the resistances prevents the character from changing condition, and returns true if that's the case, false otherwise
		/// </summary>
		/// <param name="typedDamage"></param>
		/// <returns></returns>
		public virtual bool CheckPreventCharacterConditionChange(DamageType typedDamage)
		{
			foreach (DamageResistance resistance in DamageResitanceList)
			{
				if (!resistance.gameObject.activeInHierarchy)
				{
					continue;
				}
				
				if (typedDamage == null)
				{
					if ((resistance.DamageTypeMode == DamageTypeModes.BaseDamage) &&
					    (resistance.PreventCharacterConditionChange))
					{
						return true;	
					}
				}
				else
				{
					if ((resistance.TypeResistance == typedDamage) &&
					    (resistance.PreventCharacterConditionChange))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Checks if any of the resistances prevents the character from changing condition, and returns true if that's the case, false otherwise
		/// </summary>
		/// <param name="typedDamage"></param>
		/// <returns></returns>
		public virtual bool CheckPreventMovementModifier(DamageType typedDamage)
		{
			foreach (DamageResistance resistance in DamageResitanceList)
			{
				if (!resistance.gameObject.activeInHierarchy)
				{
					continue;
				}
				if (typedDamage == null)
				{
					if ((resistance.DamageTypeMode == DamageTypeModes.BaseDamage) &&
					    (resistance.PreventMovementModifier))
					{
						return true;	
					}
				}
				else
				{
					if ((resistance.TypeResistance == typedDamage) &&
					    (resistance.PreventMovementModifier))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}