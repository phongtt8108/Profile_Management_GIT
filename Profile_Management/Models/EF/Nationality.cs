using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Profile_Management.Controllers;

namespace Profile_Management.Models.EF
{
	[Table("Nationality")]
	public class Nationality
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Nation_ID { get; set; }
        [Required(ErrorMessage = "国籍は必須です！")]
        [DisplayName("国籍")]
        [StringLength(50, ErrorMessage = "50文字を超えてはいけません!")]
        public string Nation_Name { get; set; }
		public virtual ICollection<User_TBL> user_TBLs { get; set; }

	}
}