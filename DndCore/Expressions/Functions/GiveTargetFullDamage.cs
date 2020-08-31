﻿using System;
using System.Linq;
using System.Collections.Generic;
using CodingSeb.ExpressionEvaluator;

namespace DndCore
{
	[Tooltip("Gives the target full damage from the last spell/magic attack.")]
	public class GiveTargetFullDamage : DndFunction
	{
		public override string Name { get; set; } = "GiveTargetFullDamage";

		public override object Evaluate(List<string> args, ExpressionEvaluator evaluator, Character player, Target target = null, CastedSpell spell = null, DiceStoppedRollingData dice = null)
		{
			ExpectingArguments(args, 0);
			Dictionary<DamageType, int> latestDamage = Expressions.GetCustomData<Dictionary<DamageType, int>>(evaluator.Variables);
			if (latestDamage == null)
				return null;

			foreach (Creature creature in target.Creatures)
			{
				InGameCreature inGameCreature = AllInGameCreatures.GetByCreature(creature);
				inGameCreature.StartTakingDamage();
				if (inGameCreature != null)
				{
					foreach (DamageType damageType in latestDamage.Keys)
					{
						// TODO: pass in AttackKind with the custom data
						if (creature.IsVulnerableTo(damageType, AttackKind.Magical))
							player.Game.TellDungeonMaster($"{inGameCreature.Name} is vulnerable to {damageType} damage.");
						else if (creature.IsResistantTo(damageType, AttackKind.Magical))
							player.Game.TellDungeonMaster($"{inGameCreature.Name} is resistant to {damageType} damage.");
						inGameCreature.TakeSomeDamage(player, damageType, AttackKind.Magical, latestDamage[damageType]);
					}
				}
				inGameCreature.FinishTakingDamage();
				// TODO: Also get players from the target and work with them.
			}
			return null;
		}
	}
}
