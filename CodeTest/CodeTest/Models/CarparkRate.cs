
namespace CodeTestTallyIT.Models
{
	public class CarparkRate
	{
		public enum RateType
		{
			FlatRate,
			Hourly
		}

		public string Name { get; }
		public string Type { get; set; }
		public float Rate { get; set; }

		public CarparkRate (string name, float rate, RateType type)
		{
			this.Name = name;
			this.Type = type.ToString();
			this.Rate = rate;
		}
	}
}
