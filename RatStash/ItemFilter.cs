﻿using System.Collections.Generic;

namespace RatStash;

public class ItemFilter
{
	[JsonProperty("Filter")]
	public List<string> Whitelist { get; set; } = new();

	[JsonProperty("ExcludedFilter")]
	public List<string> Blacklist { get; set; } = new();
}