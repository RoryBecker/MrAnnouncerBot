﻿using System;
using System.Linq;
using System.Collections.Generic;
using CodingSeb.ExpressionEvaluator;

namespace DndCore
{
	public class AddSpellHitEffect : DndFunction
	{
		public override string Name { get; set; } = "AddSpellHitEffect";

		public override object Evaluate(List<string> args, ExpressionEvaluator evaluator, Character player, Target target = null, CastedSpell spell = null, DiceStoppedRollingData dice = null)
			{
			ExpectingArguments(args, 1, 11);
			if (player != null)
			{
				string effectName = Expressions.GetStr(args[0], player, target, spell);

				int hue = 0;
				if (args.Count > 1)
					hue = Expressions.GetInt(args[1], player, target, spell);

				int saturation = 100;
				if (args.Count > 2)
					saturation = Expressions.GetInt(args[2], player, target, spell);

				int brightness = 100;
				if (args.Count > 3)
					brightness = Expressions.GetInt(args[3], player, target, spell);

				double scale = 1;
				if (args.Count > 4)
					scale = Expressions.GetDouble(args[4], player, target, spell);

				double rotation = 0;
				if (args.Count > 5)
					rotation = Expressions.GetDouble(args[5], player, target, spell);


				double autoRotation = 0;
				if (args.Count > 6)
					autoRotation = Expressions.GetDouble(args[6], player, target, spell);

				int timeOffset = int.MinValue;
				if (args.Count > 7)
					timeOffset = Expressions.GetInt(args[7], player, target, spell);

				int secondaryHue = 0;
				if (args.Count > 8)
					secondaryHue = Expressions.GetInt(args[8], player, target, spell);

				int secondarySaturation = 100;
				if (args.Count > 9)
					secondarySaturation = Expressions.GetInt(args[9], player, target, spell);

				int secondaryBrightness = 100;
				if (args.Count > 10)
					secondaryBrightness = Expressions.GetInt(args[10], player, target, spell);

				player.AddSpellEffect(effectName, hue, saturation, brightness, 
					scale, rotation, autoRotation, timeOffset,
					secondaryHue, secondarySaturation, secondaryBrightness);
			}

			return null;
		}
	}
}
