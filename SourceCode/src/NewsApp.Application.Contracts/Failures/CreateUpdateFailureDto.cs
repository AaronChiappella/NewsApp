using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using NewsApp.Searches;

namespace NewsApp.Failures
{
    public class CreateUpdateFailureDto
    {

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ErrorDateTime { get; set; }
        [Required]
        public SearchDto Search { get; set; }
        public Guid SearchOfFailureId { get; set; }
    }
}
