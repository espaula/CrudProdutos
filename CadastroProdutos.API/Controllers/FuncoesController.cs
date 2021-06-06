using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroProdutos.BLL.Models;
using CadastroProdutos.DAL;
using CadastroProdutos.DAL.Interfaces;
using CadastroProdutos.API.ViewModels;

namespace CadastroProdutos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncoesController : Controller
    {
        private readonly IFuncaoRepositorio _funcoesRepositorio;

        public FuncoesController(IFuncaoRepositorio funcoesRepositorio)
        {
            _funcoesRepositorio = funcoesRepositorio;
        }


        // GET: Funcoes

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcao>>> GetFuncoes()
        {
            return await _funcoesRepositorio.PegarTodos().ToListAsync();
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Funcao>> GetFuncoes(string id)
        {
            var funcao = await _funcoesRepositorio.PegarPeloId(id);
          
            if (funcao == null)
            {
                return NotFound();
            }

            return funcao;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutFuncoes(string id, FuncoesViewModel funcoes)
        {
            if (id != funcoes.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                Funcao funcao = new Funcao
                {
                    Id = funcoes.Id,
                    Name = funcoes.Name,
                    Descricao = funcoes.Descricao,
                    NormalizedName = funcoes.Name.ToUpper()


                };

                await _funcoesRepositorio.AtualizarFuncao(funcao);

                return Ok(new { 
                     mensagem = $"Função {funcao.Name} atualizada com sucesso"
                });     
            }

            return BadRequest(ModelState);
      
        }

        [HttpPost]
        public async Task<ActionResult<Funcao>> PostProduto(FuncoesViewModel funcoes)
        {
            if (ModelState.IsValid)
            {

                Funcao funcao = new Funcao
                {
                    Name = funcoes.Name,
                    Descricao = funcoes.Descricao,
                   // Id = Guid.NewGuid().ToString(),
                    NormalizedName = funcoes.Name.ToUpper()

                   

                };

                await _funcoesRepositorio.AdicionarFuncao(funcao);
                return Ok(new
                {
                    mensagem = $"Funcao { funcoes.Name} adicionada com sucesso"
                });
            }

            return BadRequest(ModelState);
        }


        // GET: Funcoes/Delete/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Funcao>> Delete(string id)
        {

            var funcao = await _funcoesRepositorio.PegarPeloId(id);
            if (funcao == null)
            {
                return NotFound();
            }

            await _funcoesRepositorio.Excluir(funcao);

            return Ok(new
            {
                mensagem = $"Funcão { funcao.Name} excluido com sucesso"
            });
        }
        [HttpGet("FiltrarFuncoes/{nomeFuncao}")]
        public async Task<ActionResult<IEnumerable<Funcao>>> FiltrarFuncoes(string nomeFuncao)
        {

            return await _funcoesRepositorio.FiltrarFuncoes(nomeFuncao).ToListAsync();

        }

    }
}
