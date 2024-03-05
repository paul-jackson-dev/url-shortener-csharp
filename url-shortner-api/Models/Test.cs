﻿using System.ComponentModel.DataAnnotations;

namespace url_shortner_api.Models
{
    public class Test
    {
        public int Id { get; set; } //Id is auto-generated by the Entity Framework

        [Required(ErrorMessage = "Name is required...")]
        public string Name { get; set; }

        public Test(string name)
        {
            this.Name = name;
        }
    }
}