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
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace CadastroProdutos.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : Controller
    {
        private readonly IProdutosRepositorio _produtosRepositorio;

        public ProdutosController(IProdutosRepositorio produtosRepositorio)
        {
            _produtosRepositorio = produtosRepositorio;
        }

        // GET: Produtos
       
      
        [Authorize(Roles ="Administrador")]
        [HttpGet("PegarTodos/{usuarioID}")]
        public async Task<ActionResult<IEnumerable<Produtos>>> PegarTodos(string usuarioID)
        {
            return await  _produtosRepositorio.PegarTodos().Where(p=> p.usuarioId == usuarioID).ToListAsync();
        }


        [Authorize(Roles = "Administrador")]
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

        [Authorize(Roles = "Administrador")]
        [HttpPost("SalvarFoto")]
        public async Task<IActionResult> SalvarFoto()
        {
            var foto = Request.Form.Files[0];
            byte[] b;

            using (var openReadStream = foto.OpenReadStream())
            {
                using (var memoryStream = new MemoryStream())
                {
                    await openReadStream.CopyToAsync(memoryStream);
                    b = memoryStream.ToArray();
                }
            }
            return Ok(new
            {
                foto = b
            });
        }

        [Authorize(Roles = "Administrador")]
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

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult<Produtos>> PostProduto(Produtos produtos)
        {
            if (ModelState.IsValid)
            {
                //produtos.usuarioId = produtos.usuarioId;
                
                await _produtosRepositorio.Inserir(produtos);
                return Ok(new
                {
                    mensagem = $"Produto { produtos.nomedoProduto} cadastrado com sucesso"
                });
            }

            return BadRequest(ModelState);
        }


        // GET: Produtos/Delete/5
        [Authorize(Roles = "Administrador")]
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


        [Authorize(Roles = "Administrador")]
        [HttpGet("FiltrarProdutos/{nomedoProduto}")]
        public async Task<ActionResult<IEnumerable<Produtos>>> FiltrarProdutos( string nomedoProduto)
        {
            return await _produtosRepositorio.FiltrarProdutos(nomedoProduto).ToListAsync();
        }

        [HttpGet("RetornarFotoProduto/{produtoId}")]
        public async Task<dynamic> RetornarFotoProduto(string produtoId)
        {

            Produtos produto = await _produtosRepositorio.PegarPeloId(Convert.ToInt32(produtoId));
            return new { imagem = produto.foto };

        }

    }
}
