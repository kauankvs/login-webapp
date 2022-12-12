using LoginWebApp.DTOs;
using LoginWebApp.Models;
using LoginWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace LoginWebApp.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        public UsuarioController(IUsuarioService service)
            => _service = service;

        [HttpPost]
        [AllowAnonymous]
        [Route("registrar")]
        public async Task<ActionResult<Usuario>> RegistrarAsync([FromForm] UsuarioDTO usuario)
            => await _service.RegistrarAsync(usuario);

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<string>> LoginAsync([FromForm] EmailESenhaDTO usuario)
        {
            var token = await _service.LoginAsync(usuario);
            HttpContext.Session.SetString("Token", token.ToString());
            return new OkObjectResult(token);
        }
           
        [HttpDelete]
        [Authorize]
        [Route("deletar")]
        public async Task<ActionResult<Usuario>> DeletarUsuarioAsync([FromForm] string senha)
            => await _service.DeletarUsuarioAsync(User.FindFirstValue(ClaimTypes.Email), senha);

        [HttpPut]
        [Authorize]
        [Route("alterar/email")]
        public async Task<ActionResult<Usuario>> MudarEmailAsync([FromForm] string emailNovo)
            => await _service.MudarEmailAsync(User.FindFirstValue(ClaimTypes.Email), emailNovo);

        [HttpPut]
        [Authorize]
        [Route("alterar/senha")]
        public async Task<ActionResult<Usuario>> MudarSenhaAsync(string email, [FromForm] string senha, [FromForm] string senhaNova)
            => await _service.MudarSenhaAsync(User.FindFirstValue(ClaimTypes.Email), senha, senhaNova);

        [HttpPut]
        [Authorize(Roles = "Gerente")]
        [Route("alterar/role")]
        public async Task<ActionResult<Usuario>> MudarPapelDaContaAsync([FromForm] string emailDoUsuario, [FromForm] string senha)
            => await _service.MudarPapelDaContaAsync(emailDoUsuario, senha);

        [HttpGet]
        [Authorize]
        [Route("minha-conta")]
        public async Task<ActionResult<List<string>>> SelecionarMinhaConta()
            => await _service.SelecionarMinhaConta(User.FindFirstValue(ClaimTypes.Email));
    }
}
