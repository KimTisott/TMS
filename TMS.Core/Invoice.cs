﻿namespace TMS.Core;

public class Invoice : Entity
{
    public decimal Price { get; set; }
    public string Filename { get; set; }
    public Customer Customer { get; set; }
}