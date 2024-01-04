namespace LoginModels
{
	public class Result<T>
	{
		public bool Status { get; set; }
		public List<string> Message { get; set; }
		public T Data { get; set; }
	}
}
