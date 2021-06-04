using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroProdutos.BLL;
using CadastroProdutos.DAL;
using CadastroProdutos.DAL.Interfaces;

namespace CadastroProdutos.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosRepositorio _produtosRepositorio;

        public ProdutosController(IProdutosRepositorio produtosRepositorio)
        {
            _produtosRepositorio = produtosRepositorio;
        }

        // GET: Produtos
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produtos>>> GetProdutos()
        {
            return await  _produtosRepositorio.PegarTodos().ToListAsync();
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Produtos>> GetProdutos(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _produtosRepositorio.PegarPeloId(id);
            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutProdutos(int id, Produtos produtos)
        {
            if (id != produtos.produtoId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _produtosRepositorio.Atualizar(produtos);
                return Ok(new
                {
                    mensagem = $"Produto { produtos.nomedoProduto} atualizado com sucesso"
                });
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<ActionResult<Produtos>> PostProduto(Produtos produtos)
        {
            if (ModelState.IsValid)
            {
                produtos.usuarioId = "1";
                await _produtosRepositorio.Inserir(produtos);
                return Ok(new
                {
                    mensagem = $"Produto { produtos.nomedoProduto} cadastrado com sucesso"
                });
            }

            return BadRequest(ModelState);
        }


            // GET: Produtos/Delete/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Produtos>> Delete(int id)
        {

            var produtos = await _produtosRepositorio.PegarPeloId(id);
            if (produtos == null)
            {
                return NotFound();
            }

            await _produtosRepositorio.Excluir(id);

            return Ok(new
            {
                mensagem = $"Produto { produtos.nomedoProduto} excluido com sucesso"
            });
        }


        [HttpGet("FiltrarProdutos/{nomedoProduto}")]
        public async Task<ActionResult<IEnumerable<Produtos>>> FiltrarProdutos( string nomeProduto)
        {
            return await _produtosRepositorio.FiltrarProdutos(nomeProduto).ToListAsync(); ;
        }

    }
}
