#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TablesVsDivsSample1App.Models;

public partial class Customers
{
    public int Id { get; set; }

    public string Company { get; set; }

    public string ContactType { get; set; }

    public string ContactName { get; set; }

    public string Country { get; set; }

    [Display(Name = "Join Date")]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateOnly JoinDate { get; set; }
}