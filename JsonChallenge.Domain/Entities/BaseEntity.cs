using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JsonChallenge.Domain.Entities
{
    public class BaseEntity
    {
        [JsonPropertyOrder(-1)]
        public Guid Id { get; set; }
    }
}
