using CadastroProdutos.API.ViewModels;
using CadastroProdutos.BLL.Models;
using CadastroProdutos.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CadastroProdutos.API.Services
{

    public class AutenticaSMSerevice
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public AutenticaSMSerevice(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public async Task<RetornoLogarUsuario> LogarUsuario(LoginViewModel model)
        {

            try
            {

                RetornoLogarUsuario retornoLogarUsuario = new RetornoLogarUsuario();
               // Usuario usuario = new Usuario();

                // if (rt.success) //AUTENTICOU NA API SM
                if (true) //AUTENTICOU NA API SM
                {

                    retornoLogarUsuario.usuario = await _usuarioRepositorio.VerificaSeExiste(model.Login);

                    if (retornoLogarUsuario.usuario == null)
                    {
                        IdentityResult usuarioCriado;
                        string funcaoUsuario;

                        Usuario usuarioNovo = new Usuario
                        {
                            UserName = ("Nome" +model.Login).ToString(),
                            PasswordHash = model.Senha,
                            login = model.Login,
                            profissao = "profissão",
                            foto = null,
                            NormalizedUserName = ("Nome - " + model.Login).ToUpper()
                        };

                        //if (await _usuarioRepositorio.PegarQuantidadeUsuariosRegistrados() > 0)
                        //{
                        //    funcaoUsuario = "Usuario";
                        //}
                        //else
                        //{
                        funcaoUsuario = "Administrador";
                        // }

                        usuarioCriado = await _usuarioRepositorio.CriarUsuario(usuarioNovo, model.Senha);

                        if (usuarioCriado.Succeeded)
                        {
                            await _usuarioRepositorio.IncluirUsuarioEmFuncao(usuarioNovo, funcaoUsuario);

                            //var token = TokenService.GerarToken(usuarioNovo, funcaoUsuario);
                            await _usuarioRepositorio.LogarUsuario(usuarioNovo, false);
                            retornoLogarUsuario.usuario = usuarioNovo;
                        }
                        else
                        {

                            IdentityError er = usuarioCriado.Errors.FirstOrDefault();
                            retornoLogarUsuario.erro = er.Description;
                            return retornoLogarUsuario;
                        }
                    }

                }

                return retornoLogarUsuario;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        

        public RetornoApiSM AutenticaApi(LoginViewModel loginViewModel)
        {
            try
            {

                RetornoApiSM retornoApiSM = new RetornoApiSM();
                string _baseUrl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Security")["ApiSMURL"];

                var client = new RestClient(_baseUrl);
                client.Timeout = 3000;
                var request = new RestRequest(_baseUrl, Method.POST);

                //var p = Encoding.UTF8.GetBytes(loginViewModel.Login+":"+ loginViewModel.Senha);
                //var basic = Convert.ToBase64String(p);
                //request.AddHeader("Authorization", "Bearer "+ basic);

                //client.Authenticator = new HttpBasicAuthenticator(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Security")["UserName"], new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Security")["Password"]);

                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(loginViewModel);

                IRestResponse response = client.Execute(request);

                var retorno = response.Content;

                return JsonConvert.DeserializeObject<RetornoApiSM>(retorno); ;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }


    public class RetornoLogarUsuario
    {

        public Usuario usuario { get; set; }
        public string erro { get; set; }
    }

    public class RetornoApiSM
    {
        public bool success { get; set; }
        public string  error { get; set; }
    }
}
