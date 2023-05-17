using System;
using System.ComponentModel.DataAnnotations;

namespace net6webAPI.Models
{
	public class UpdateEmployee
	{

        public string EmployeeName { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;


  
        public DateTime DateOfJoining { get; set; } = DateTime.MinValue;

        public string PhotoFileName { get; set; } = string.Empty;
    }
}

