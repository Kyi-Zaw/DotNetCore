using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KZDotNetCore.Domain.Models.Todo;

// Client
public class TodoRequestModel
{
    public string? TodoId { get; set; }
    public string? Title { get; set; }
    public string? Status { get; set; }
}