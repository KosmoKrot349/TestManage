using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sr6.Models
{
    public class Action
    {
        public int id { get; set; }
        [Required]
        [Range (0,9999999,ErrorMessage ="Поле не заполненно, либо заполненно не корректно")]
        [Display(Name = "Номер")]
        public int number { get; set; }
        [Required]
        [Display(Name = "Действие")]
        public string action { get; set; }
        public int Scenarioid { get; set; }
        public Scenario scenario { get; set; }
    }
}