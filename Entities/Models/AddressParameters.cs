public class AddressParameters : QueryStringParameters
{
    public string address1 { get; set; }
    public string address2 { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string country { get; set; }
    public string zip { get; set; }
    public bool archive { get; set; }
    public int? customerId { get; set; }
    
}