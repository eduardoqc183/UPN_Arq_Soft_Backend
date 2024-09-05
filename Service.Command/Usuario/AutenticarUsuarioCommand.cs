using Common.Exceptions;
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
    public class AutenticarUsuarioCommand : IRequest<bool>
    {
        [EmailAddress]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }

    public class AutenticarUsuarioEventHandler : IRequestHandler<AutenticarUsuarioCommand, bool>
    {
        private readonly JorplastContext context;

        public AutenticarUsuarioEventHandler(JorplastContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(AutenticarUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var u = await this.context.usuarios.FirstOrDefaultAsync(w => w.email.ToLower().Equals(request.Email.ToLower()));

                if (u == null)
                {
                    throw new NotFoundException("Usuario no encontrado");
                }

                var verify = EncryptionHelper.VerifyPassword(request.Password, u.password);
                return verify;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
