﻿using LoginWebApp.DTOs;
using LoginWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginWebApp.Services
{
    public interface IUsuarioService
    {
        public Task<ActionResult<Usuario>> RegistrarAsync(UsuarioDTO usuarioDTO);
        public Task<ActionResult<string>> LoginAsync(EmailESenhaDTO usuario);
        public Task<ActionResult<Usuario>> DeletarUsuarioAsync(string email, string senha);
        public Task<ActionResult<Usuario>> MudarEmailAsync(string email, string emailNovo);
        public Task<ActionResult<Usuario>> MudarSenhaAsync(string email, string senha, string senhaNova);
        public Task<ActionResult<Usuario>> MudarPapelDaContaAsync(string email, string senha);
        public Task<ActionResult<List<string>>> SelecionarMinhaConta(string email);
    }
}
