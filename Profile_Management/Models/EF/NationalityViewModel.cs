using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Profile_Management.Models.EF
{
	public class NationalityViewModel
	{
        public Nationality NewNationality { get; set; }
        public List<Nationality> Nationalities { get; set; }
    }
}