namespace LoginModels
{
	public class Audience
	{
		public string Secret { get; set; }
		public string Iss { get; set; }
		public string TokenTime { get; set; }
		public string Aud { get; set; }
	}

}
