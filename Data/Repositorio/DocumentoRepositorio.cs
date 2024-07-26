using Data.Interfaces.IRepositorio;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    public class DocumentoRepositorio : Repositorio<Documento>, IDocumentoRepositorio
    {
        private readonly ApplicationDbContext _db;


        public DocumentoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Documento documento)
        {
            var documentoDb = _db.Documentos.FirstOrDefault(x => x.Id == documento.Id);

            if(documentoDb != null)
            {
                documento.Descripcion = documento.Descripcion;
                documentoDb.url = documento.url;
                documentoDb.TipoDocumentoId = documento.TipoDocumentoId;
                documentoDb.FechaActualizacion = DateTime.Now;
                documentoDb.ActualizadoPorId = documento.ActualizadoPorId;
                _db.SaveChanges();
            }
        }
    }
}
