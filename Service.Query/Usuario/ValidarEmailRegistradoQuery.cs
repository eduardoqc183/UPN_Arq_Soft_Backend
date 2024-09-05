using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Query.Usuario
{
    public class ValidarEmailRegistradoQuery : IRequest<bool>
    {      
        public string Email { get; }
        public ValidarEmailRegistradoQuery(string email)
        {
            Email = email.ToLower().Trim();
        }
    }

    public class ValidarEmailRegistradoEventHandler : IRequestHandler<ValidarEmailRegistradoQuery, bool>
    {
        private readonly JorplastContext context;

        public ValidarEmailRegistradoEventHandler(JorplastContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(ValidarEmailRegistradoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var emailExiste = await context.usuarios.AnyAsync(s => s.email.ToLower().Trim().Equals(request.Email));
                return emailExiste;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
