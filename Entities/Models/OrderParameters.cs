using System;

public class OrderParameters : QueryStringParameters
{
    public string orderNumber { get; set; }
    public string address1 { get; set; }
    public string address2 { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string country { get; set; }
    public string zip { get; set; }
    public double? MinShipping { get; set; }
    public double? MaxShipping { get; set; }
    public double? MinTotal { get; set; }
    public double? MaxTotal { get; set; }
    public DateTime? MinDate { get; set; } = null;
    public DateTime? MaxDate { get; set; } = null;
    public int? customerId { get; set; }

    public bool ValidDateRange => MinDate == null || MaxDate == null || DateTime.Compare((DateTime)MinDate.Value, (DateTime)MaxDate.Value) <= 0;
    public bool ValidShippingRange => MinShipping == null || MaxShipping == null || MaxShipping > MinShipping;
    public bool ValidTotalRange => MinTotal == null || MaxTotal == null || MaxTotal > MinTotal;
}