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

                RetornoApiSM retornoApiSM = AutenticaApi(model);
                RetornoLogarUsuario retornoLogarUsuario = new RetornoLogarUsuario();

                //if (true) //AUTENTICOU NA API SM
                if (retornoApiSM.success) //AUTENTICOU NA API SM
                {

                    retornoLogarUsuario.usuario = await _usuarioRepositorio.VerificaSeExiste(model.Login);

                    if (retornoLogarUsuario.usuario == null)
                    {
                        IdentityResult usuarioCriado;
                        string funcaoUsuario;

                        Usuario usuarioNovo = new Usuario
                        {
                            UserName = (model.Login).ToString(),
                            PasswordHash = model.Senha,
                            login = model.Login,
                            //foto = null,
                            NormalizedUserName =(model.Login).ToUpper()
                        };

                        
                        funcaoUsuario = "Administrador";
                       

                        usuarioCriado = await _usuarioRepositorio.CriarUsuario(usuarioNovo, model.Senha);

                        if (usuarioCriado.Succeeded)
                        {
                            await _usuarioRepositorio.IncluirUsuarioEmFuncao(usuarioNovo, funcaoUsuario);
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
                else
                {
                    retornoLogarUsuario.erro = retornoApiSM.error;

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

                request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{loginViewModel.Login}:{loginViewModel.Senha}")));

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
