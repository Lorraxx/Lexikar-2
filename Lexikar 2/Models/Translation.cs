using Lexikar_2.Models.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lexikar_2.Models
{
    public class Translation : BaseEntity
    {
		// Inherited from BaseEntity:
        // [Key]
		// public int		ID			{ get; set; }
		public bool		Nominal		{ get; set; }
		public bool		Verbal		{ get; set; }
		public bool		Other		{ get; set; }
		public string	Original	{ get; set; }
		public string	Translated	{ get; set; }
		public string	Source		{ get; set; }
		public string	Definition	{ get; set; }
		public string	Systematics { get; set; }
		public string	Note		{ get; set; }
    }
}
