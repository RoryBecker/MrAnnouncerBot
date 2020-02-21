﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace MapCore
{
	public class LightnessChangeCommand : BaseStampAbsoluteValueCommand
	{

		public LightnessChangeCommand()
		{

		}

		protected override void PrepareForExecution(Map map, List<IStampProperties> selectedStamps)
		{
			base.PrepareForExecution(map, selectedStamps);
			foreach (IStampProperties stampProperties in selectedStamps)
				SaveValue(stampProperties, stampProperties.Lightness);
		}

		protected override void ActivateRedo(Map map)
		{
			foreach (IStampProperties stampProperties in SelectedStamps)
				stampProperties.Lightness = RedoValue;
		}

		protected override void ActivateUndo(Map map)
		{
			foreach (IStampProperties stampProperties in SelectedStamps)
				stampProperties.Lightness = GetSavedValue(stampProperties);
		}
	}
}
