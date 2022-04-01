using System.Collections.Generic;

namespace APIClientes.Models.Dto
{
    public class ResponseDto
    {
        // controlar todas las respuestas
        public bool IsSuccess { get; set; } = true;

        //los resultados
        public object Result { get; set; }

        //
        public string DisplayMessage { get; set; }

        //lista de errores cuando se haga un request
        public List<string> ErrorMessages { get; set; }


    }
}
