namespace RatStash
{
	using Newtonsoft.Json;

	public class PortContainer
    {
        [JsonProperty("containType")]
        public object[] ContainType { get; set; }

        [JsonProperty("isSecured")]
        public bool IsSecured { get; set; }

        [JsonProperty("lootFilter")]
        public object[] LootFilter { get; set; }

        [JsonProperty("maxCountSpawn")]
        public int MaxCountSpawn { get; set; }

        [JsonProperty("minCountSpawn")]
        public int MinCountSpawn { get; set; }

        [JsonProperty("openedByKeyID")]
        public object[] OpenedByKeyId { get; set; }

        [JsonProperty("sizeHeight")]
        public int SizeHeight { get; set; }

        [JsonProperty("sizeWidth")]
        public int SizeWidth { get; set; }

        [JsonProperty("spawnRarity")]
        public string SpawnRarity { get; set; }

        [JsonProperty("spawnTypes")]
        public string SpawnTypes { get; set; }
    }
}
