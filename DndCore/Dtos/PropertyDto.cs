﻿using System;
using System.Linq;

namespace DndCore
{
	public class PropertyDto
	{
		public string Name { get; set; }
		public ExpressionType Type { get; set; }
		public string Expression { get; set; }
		public string Description { get; set; }
		public PropertyDto()
		{

		}
	}
}
