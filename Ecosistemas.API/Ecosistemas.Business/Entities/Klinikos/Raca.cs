﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class Raca
    {
        [Key]
        public Guid RacaId { get; set; }

        [Required(ErrorMessage = "O nome da raça é obrigatório")]
        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public virtual List<PessoaPaciente> PessoaPacientes { get; set; }
    }
}
