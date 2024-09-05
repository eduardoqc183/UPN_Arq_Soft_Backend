using Common.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Command.Usuario
{
    public class RegistrarUsuarioCommand : IRequest<bool>
    {
        [EmailAddress]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Nombres { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Apellidos { get; set; }
        public int TipoDocIdentidadId { get; set; }
        [MaxLength(20)]
        public string NroDocIdentidad { get; set; }
    }

    public class RegistrarUsuarioEventHandler : IRequestHandler<RegistrarUsuarioCommand, bool>
    {
        private readonly JorplastContext context;

        public RegistrarUsuarioEventHandler(JorplastContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var emailexiste = await context.usuarios.AnyAsync(w => w.email.ToLower().Trim().Equals(request.Email.ToLower().Trim()));
                if (emailexiste)
                {
                    throw new Exception("Email ya registrado previamente");
                }

                var p = new persona
                {
                    apellidos = request.Apellidos,
                    nombres = request.Nombres,
                    nrodocidentidad = request.NroDocIdentidad,
                    rolid = 1,
                    fecharegistro = DateTime.Now
                };

                var u = new usuario
                {
                    email = request.Email,
                    fecharegistro = DateTime.Now,
                    password = EncryptionHelper.EncryptPassword(request.Password)
                };

                p.usuarios.Add(u);

                await context.personas.AddAsync(p);
                var r = await context.SaveChangesAsync();
                return r > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
