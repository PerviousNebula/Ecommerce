using System;
using System.Collections.Generic;

public class OrderDto
{
    public int orderId { get; set; }
    public string orderNumber { get; set; }
    public string address1 { get; set; }
    public string address2 { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string country { get; set; }
    public string zip { get; set; }
    public double shipping { get; set; }
    public double tax { get; set; }
    public double discount { get; set; }
    public double total { get; set; }
    public DateTime date { get; set; }
    public int customerId { get; set; }
    public int addressId { get; set; }
    public AddressDto Address { get; set; }
    public IEnumerable<OrderDetailDto> OrderDetails { get; set; }
}