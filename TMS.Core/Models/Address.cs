﻿namespace TMS.Core;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public City City { get; set; }
}