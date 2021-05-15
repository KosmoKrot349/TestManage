using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sr6.Models
{
    public class Scenario
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string title { get; set; }
        [Required]
        [Display(Name = "Результат")]
        public string result { get; set; }
        [Required]
        [Display(Name = "Дата проведения")]
        public DateTime dateOfExecution { get; set; }
        [Display(Name = "Тип ошибки")]
        public string typeOfError { get; set; }
        [Required]
        [Display(Name = "Крайняя дата исправления")]
        public DateTime dateOfBugFix { get; set; }
        [Required]
        [Display(Name = "Сотрудник отвественный за исправление")]
        public string nameOfFixer  { get; set; }
        public bool isFixed { get; set; }
        public int Projectid { get; set; }
        public Project project { get; set; }
        public List<Action> Actions { get; set; }
        
    }
}