using System.Collections.Generic;

namespace AspNET.Models.InvoiceDTO
{
    public class ManyBodyDTO
    {
        public List<BodyDTO> Bodies { get; set; }

        public int TotalPages { get; set; }

        public ManyBodyDTO()
        {
            this.Bodies = new List<BodyDTO>();
        }
    }
}
