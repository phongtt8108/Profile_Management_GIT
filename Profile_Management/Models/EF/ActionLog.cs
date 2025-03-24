using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Profile_Management.Models.EF
{
    [Table("ActionLog")]
    public class ActionLog
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActionLogID { get; set; }
        [StringLength(50)]
        [Display(Name = "Action Log Type")]
        public string ActionLogType { get; set; }
        [StringLength(5000, ErrorMessage = "50文字を超えてはいけません!")]
        [Display(Name = "Action Log Description")]
        public string ActionLogDescription { get; set; }
        [Display(Name = "Action Log Date")]
        public DateTime ActionLogDate { get; set; }
        [Display(Name = "Action Log User")]
        public int ActionLogUser { get; set; }
        public string ActionLogAccountLog { get; set; }
        public string ActionLogIP { get; set; }
        public string ActionLogDevice { get; set; }

    }
}