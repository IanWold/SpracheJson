namespace SpracheJson.Example;

public class Address
{
	public string StreetAddress { get; set; } = string.Empty;

	public string City { get; set; } = string.Empty;

	public string State { get; set; } = string.Empty;

	public string PostalCode { get; set; } = string.Empty;

	public override string ToString() =>
		$"{StreetAddress}\r\n{City} {State} {PostalCode}";
}
