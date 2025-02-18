using System.Collections.Generic;

namespace RatStash;

public class CompoundItem : Item
{
	[JsonProperty("CanPutIntoDuringTheRaid")]
	public bool CanPutIntoDuringTheRaid { get; set; }

	[JsonProperty("CantRemoveFromSlotsDuringRaid")]
	public List<EquipmentSlot> CantRemoveFromSlotsDuringRaid { get; set; } = new();

	[JsonConverter(typeof(RawItemConverter<Grid>))]
	[JsonProperty("Grids")]
	public List<Grid> Grids { get; set; } = new();
	
	[JsonProperty("Slots")]
	public List<Slot> Slots { get; set; } = new();
}
