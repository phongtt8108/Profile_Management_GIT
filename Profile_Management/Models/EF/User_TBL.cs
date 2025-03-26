using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Profile_Management.Models.EF;

namespace Profile_Management.Controllers
{
	[Table("User_TBL")]
    public class User_TBL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [Required]
        [DisplayName("氏名")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "氏名は必須です！")]
        [DisplayName("生年月日")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage = "性別は必須です！")]
        [DisplayName("性別")]
        [StringLength(20)]
        public String Gender { get; set; }
        [Required(ErrorMessage = "個人番号は必須です！")]
        [DisplayName("個人番号")]
        [Range(0, 9999999999999999999, ErrorMessage = "20文字を超えてはいけません!")]
        public long NationalID { get; set; }
        [Required(ErrorMessage = "国籍は必須です！")]
        [DisplayName("国籍")]
        public int? Nation_ID { get; set; } =1;
        public virtual Nationality Nationality { get; set; }
        [StringLength(20)]
        [DisplayName("婚姻状況")]
        public String MaritalStatus { get; set; }
        [StringLength(50, ErrorMessage = "50文字を超えてはいけません!")]
        [DisplayName("携帯電話")]
        public string PhoneNumber { get; set; }
        [StringLength(500, ErrorMessage = "500文字を超えてはいけません!")]
        [DisplayName("住所")]
        public string Address { get; set; }
        [StringLength(50, ErrorMessage = "50文字を超えてはいけません!")]
        [DisplayName("職業")]
        public string Job { get; set; }
        [StringLength(100, ErrorMessage = "100文字を超えてはいけません!")]
        [DisplayName("会社名")]
        public string Company { get; set; }
        [StringLength(50, ErrorMessage = "50文字を超えてはいけません!")]
        [DisplayName("役職")]
        public string Position { get; set; }
        [DisplayName("写真")]
        [StringLength(1000, ErrorMessage = "1000文字を超えてはいけません!")]
        public string ProfilePicture { get; set; }
    }
}