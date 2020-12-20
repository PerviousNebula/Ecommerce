using System;
using System.Collections.Generic;

public class CustomerDto
{
    public int customerId { get; set; }
    public string name { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public DateTime signupDate { get; set; }
    public bool archive { get; set; }

    public IEnumerable<AddressDto> Addresses { get; set; }
    public IEnumerable<OrderDto> Orders { get; set; }
}