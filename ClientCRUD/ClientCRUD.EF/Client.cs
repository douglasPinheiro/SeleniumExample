using System;
using System.ComponentModel.DataAnnotations;

namespace ClientCRUD.EF
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
