using System;

public class CustomerParameters : QueryStringParameters
{
    public string name { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public DateTime? MinSignupDate { get; set; } = null;
    public DateTime? MaxSignupDate { get; set; } = null;
    public bool? archive { get; set; }

    public bool ValidYearRange => MinSignupDate == null || MaxSignupDate == null || DateTime.Compare((DateTime)MinSignupDate.Value, (DateTime)MaxSignupDate.Value) <= 0;
}