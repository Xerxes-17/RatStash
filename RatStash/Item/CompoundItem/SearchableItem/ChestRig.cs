using Newtonsoft.Json.Converters;

namespace RatStash
{
	using Newtonsoft.Json;

	public class ChestRig : SearchableItem
	{
		[JsonProperty("ArmorMaterial")]
		[JsonConverter(typeof(StringEnumConverter))]
		public ArmorMaterial ArmorMaterial { get; set; }

		[JsonProperty("BluntThroughput")]
		public int BluntThroughput { get; set; }

		[JsonProperty("Durability")]
		public int Durability { get; set; }

		[JsonProperty("MaxDurability")]
		public int MaxDurability { get; set; }

		[JsonProperty("RigLayoutName")]
		public string RigLayoutName { get; set; }

		[JsonProperty("armorClass")]
		public int ArmorClass { get; set; }

		[JsonProperty("armorZone", ItemConverterType = typeof(StringEnumConverter))]
		public ArmorZone[] ArmorZone { get; set; }

		[JsonProperty("mousePenalty")]
		public int MousePenalty { get; set; }

		[JsonProperty("speedPenaltyPercent")]
		public int SpeedPenaltyPercent { get; set; }

		[JsonProperty("weaponErgonomicPenalty")]
		public int WeaponErgonomicPenalty { get; set; }
	}
}
