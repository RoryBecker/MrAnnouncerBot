﻿using System;
using System.Collections.Generic;
using System.Linq;
using CodingSeb.ExpressionEvaluator;

namespace DndCore
{
	public class AskFunction : DndFunction
	{
		public static event AskEventHandler AskQuestion;
		public static void OnAskQuestion(object sender, AskEventArgs ea)
		{
			AskQuestion?.Invoke(sender, ea);
		}
		public override string Name { get; set; } = "Ask";

		public override object Evaluate(List<string> args, ExpressionEvaluator evaluator, Character player)
		{
			AskEventArgs ea = new AskEventArgs(args[0], args.Skip(1).ToList());
			OnAskQuestion(player, ea);
			return ea.Result;
		}
	}
}