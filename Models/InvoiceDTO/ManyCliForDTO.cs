using System.Collections.Generic;

namespace AspNET.Models.InvoiceDTO
{
    public class ManyCliForDTO
    {
        public List<CliForDTO> CliFors { get; set; }
        public int Count { get; set; }

        public ManyCliForDTO()
        {
            this.CliFors = new List<CliForDTO>();
        }
    }
}
