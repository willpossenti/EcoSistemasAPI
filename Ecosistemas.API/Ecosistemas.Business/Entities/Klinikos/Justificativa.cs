﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class Justificativa
    {
        [Key]
        public Guid JustificativaId { get; set; }


        [Required(ErrorMessage = "A Descrição da justificativa é obrigatória")]
        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public virtual List<PessoaPaciente> PessoaPacientes { get; set; }
    }
}
