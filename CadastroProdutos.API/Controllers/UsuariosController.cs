﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroProdutos.BLL.Models;
using CadastroProdutos.DAL;
using CadastroProdutos.DAL.Interfaces;
using System.IO;
using CadastroProdutos.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using CadastroProdutos.API.Services;

namespace CadastroProdutos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

      

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Usuario>> GetProdutos(string id)
        {
            var usuario = await _usuarioRepositorio.PegarPeloId(id);

            if (id == null)
            {
                return NotFound();
            }

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

       

     
        [HttpPost("LogarUsuario")]
        public async Task<ActionResult> LogarUsuario(LoginViewModel model)
        {
            if (model == null)
                return NotFound("Usuário e / ou senhas inválidos");


            AutenticaSMSerevice autenticaSMSerevice = new AutenticaSMSerevice(_usuarioRepositorio);

            RetornoLogarUsuario retornousuario =  await autenticaSMSerevice.LogarUsuario(model);


            if (retornousuario.usuario != null)
            {
                var funcaoUsuario = await _usuarioRepositorio.PegarFuncoesUsuarios(retornousuario.usuario);
                var token = TokenService.GerarToken(retornousuario.usuario, funcaoUsuario.First());
                
                await _usuarioRepositorio.LogarUsuario(retornousuario.usuario, false);

                return Ok(new
                {
                    loginUsuarioLogado = retornousuario.usuario.login,
                    usuarioId = retornousuario.usuario.Id,
                    tokenUsuarioLogado = token,
                    

                });
            }
            else
            {
                return NotFound(retornousuario.erro);

            }

        }

    }
}
