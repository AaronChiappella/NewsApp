using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsApp.Reads
{
    public class CreateUpdateReadDto
    {
        [Required]
        public DateTime FechaHora { get; set; }

        [Required]
        public bool Likeada { get; set; }

    }
}
