using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace pet_hotel
{
    public class PetOwner
    {
        public int id { get; set; }

        [Required]

        public string name { get; set; }

        [Required]

        public string emailAddress { get; set; }
        // validate format

        [JsonIgnore]

        public List<Pet> ownersPets { get; set; }

        [NotMapped]

        public int petCount
        {
            get
            {
                return (this.ownersPets != null ? this.ownersPets.Count : 0);
            }
        }

    }
}
