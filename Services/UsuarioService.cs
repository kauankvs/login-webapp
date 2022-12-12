﻿using LoginWebApp.Context;
using LoginWebApp.Controllers;
using LoginWebApp.DTOs;
using LoginWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginWebApp.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly LoginContext _context;
        private readonly IAutenticacaoService _auth;
        public UsuarioService(LoginContext context, IAutenticacaoService senha)
        {
            _context = context;
            _auth = senha;
        }

        public async Task<ActionResult<Usuario>> RegistrarAsync(UsuarioDTO usuarioDTO)
        {
            byte[] senhaHash, senhaSalt;
            bool usuarioExiste = await _auth.ChecarQueUsuarioExisteAsync(usuarioDTO.Email);
            if (usuarioExiste.Equals(true))
                return new ConflictResult();

            _auth.CriarSenhaComHashESalt(usuarioDTO.Senha, out senhaHash, out senhaSalt);
            Usuario usuario = new Usuario()
            {
                Nome = usuarioDTO.Nome,
                Sobrenome = usuarioDTO.Sobrenome,
                Email = usuarioDTO.Email,
                DataDeNascimento = usuarioDTO.DataDeNascimento,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt,
                DataDeCriacao = DateTime.Now,
                Papel = Papel.Cliente
            };

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return new CreatedResult(nameof(UsuarioController), usuario);
        }

        public async Task<ActionResult<string>> LoginAsync(EmailESenhaDTO usuario)
        {
            bool usuarioExiste = await _auth.ChecarQueUsuarioExisteAsync(usuario.Email);
            if (usuarioExiste.Equals(false))
                return new NotFoundObjectResult(usuario);

            bool senheCorreta = await _auth.ChecarSeSenhaECorretaAsync(usuario.Senha, usuario.Email);
            if (senheCorreta.Equals(false))
                return new ForbidResult();

            var token = await _auth.CriarTokenAsync(usuario.Email);
            return token;
        }

        public async Task<ActionResult<Usuario>> DeletarUsuarioAsync(string email, string senha)
        {
            if (_auth.ChecarSeSenhaECorretaAsync(senha, email).Equals(false))
                return new ConflictResult();

            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.Email.Equals(email));
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return new AcceptedResult();
        }

        public async Task<ActionResult<Usuario>> MudarEmailAsync(string email, string emailNovo)
        {
            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.Email.Equals(email));
            usuario.Email = emailNovo;
            await _context.SaveChangesAsync();
            return new AcceptedResult();
        }

        public async Task<ActionResult<Usuario>> MudarSenhaAsync(string email, string senha, string senhaNova)
        {
            byte[] senhaHash, senhaSalt;
            Usuario usuario;

            bool senhaCorreta = await _auth.ChecarSeSenhaECorretaAsync(senha, email);
            if (senhaCorreta.Equals(false))
                return new BadRequestResult();

            usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.Email.Equals(email));
            _auth.CriarSenhaComHashESalt(senhaNova, out senhaHash, out senhaSalt);
            usuario.SenhaHash = senhaHash;
            usuario.SenhaSalt = senhaSalt;
            await _context.SaveChangesAsync();
            return new AcceptedResult();
        }

        public async Task<ActionResult<Usuario>> MudarPapelDaContaAsync(string email, string senha)
        {
            bool usuarioExiste = await _auth.ChecarQueUsuarioExisteAsync(email);
            if (usuarioExiste.Equals(false))
                return new NotFoundResult();

            Usuario usuario = null;
            usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.Email.Equals(email));
            if (senha.Equals(Settings.SenhaAuxiliar))
            {
                usuario.Papel = Papel.Auxiliar;
                await _context.SaveChangesAsync();
                return new AcceptedResult();
            }
            if (senha.Equals(Settings.SenhaGerencial))
            {
                usuario.Papel = Papel.Gerente;
                await _context.SaveChangesAsync();
                return new AcceptedResult();
            }
            return new BadRequestResult();
        }

        public async Task<ActionResult<List<string>>> SelecionarMinhaConta(string email) 
        {
            Usuario usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
            List<string> usuarioDisplay = new List<string>
            {
                usuario.Nome,
                usuario.Sobrenome,
                usuario.Email,
                usuario.DataDeNascimento.ToString(),
                usuario.Papel.ToString(),
                usuario.DataDeCriacao.ToString(),
            };
            return new OkObjectResult(usuarioDisplay);
        }
    }
}
