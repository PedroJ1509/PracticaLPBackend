﻿namespace API.Errores
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string mensaje=null)
        {
            StatusCode = statusCode;
            Mensaje = mensaje ?? GetMensajeStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Mensaje { get; set; }

        private string GetMensajeStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Se ha realziado una solicitud no válida",
                401 => "No estas autorizado para este recurso",
                404 => "Recurso No encontrado",
                500 => "Error interno del sevidor",
                _ => null
            };
        }
    }
}
