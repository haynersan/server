#region usings

using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shift.Domain.Cadastro.EmpresaModel.Commands.Inputs;
using Shift.Domain.Cadastro.EmpresaModel.Commands.Results;
using Shift.Domain.Cadastro.EmpresaModel.Handlers;
using Shift.Domain.Cadastro.EmpresaModel.Repository;
using Shift.Domain.Core.Interfaces;
using Shift.Services.Api.Configurations;
using System.Linq;
using System;

#endregion

namespace Shift.Services.Api.Controllers.Cadastro
{
    public class EmpresaController : BaseController
    {

        
        #region Config


        private readonly EmpresaHandler         _handler;


        private readonly IEmpresaRepository     _empresaRepository;


        public EmpresaController(
                                    IUnitOfWork         uow, 
                                    IUser               user,
                                    EmpresaHandler      empresaHandler,
                                    IEmpresaRepository  empresaRepository) : base(uow, user)
        {

            _handler            = empresaHandler;

            _empresaRepository  = empresaRepository;
        }


        #endregion



        #region Escrita



        [HttpPost]
        [Route("v1/empresas")]
        [Authorize()]
        public IActionResult Post([FromBody] AdicionarEmpresaCommand command)
        {
            var result = _handler.Handle(command);

            return Response(result, _handler.Notifications);

        }
        
        

        [HttpPut]
        [Route("v1/empresas")]
        [Authorize()]
        public IActionResult Put([FromBody] AtualizarEmpresaCommand command)
        {

            var result = _handler.Handle(command);

            return Response(result, _handler.Notifications);

        }
        


        [HttpDelete]
        [Route("v1/empresas/{id}")]
        [Authorize(Policy = "PodeGravarEmpresa")]
        public IActionResult Delete(string id)
        {

            var command = new ExcluirEmpresaCommand(id);

            var result = _handler.Handle(command);

            return Response(result, _handler.Notifications);

        }


        
        #endregion



        #region Leitura



        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public object Get()
        {
            return new { inicio = "Meu Primeiro Método. Aqui foi onde tudo começou!" };
        }



        [HttpGet]
        [Route("v1/empresas")]
        [Authorize()]
        //[ResponseCache(Duration = 60)] //Curso do Balta.IO
        public IEnumerable<EmpresaCommandResult> Listar()
        {
            return _empresaRepository.ListarEmpresas();
        }


        
        [HttpGet]
        [Route("v1/empresas-paginadas")]
        [Authorize()]
        public IActionResult ListarPaginado(int pagina = 1, int qtdeItensPorPagina = 3, string nome = null)
        {


            if (pagina <= 0 || qtdeItensPorPagina <= 0)
               return BadRequest("Os parâmetros pagina e tamanhoPagina devem ser maiores que zero.");



            if (qtdeItensPorPagina > 10)
                    return BadRequest("O tamanho máximo de página permitido é 10.");



            int totalRegistros = _empresaRepository.Buscar(e => e.Excluido == false).ToList().Count;


            //v1
            //int totalPaginas = (int)Math.Ceiling(_empresaRepository.Buscar(e => e.Excluido == false).ToList().Count / Convert.ToDecimal(itensPorPagina));


            int totalPaginas = (int)Math.Ceiling(totalRegistros / Convert.ToDecimal(qtdeItensPorPagina));


            if (pagina > totalPaginas)
                return BadRequest("A página solicitada não existe.");


            


            // Mostra o Total de Registros no Header
            //Request.HttpContext.Response.Headers.Add("X-Pagination-TotalRegisters", totalRegistros.ToString());

         

            var resultado = new
            {

                dados           = _empresaRepository.ListarEmpresasPaginadas(pagina, qtdeItensPorPagina, nome),


                paginaAtual     = pagina.ToString(),


                itensPorPagina  = qtdeItensPorPagina.ToString(),


                totalPaginas    = totalPaginas.ToString(),


                totalRegistros  = totalRegistros.ToString()
            };


            return Ok(resultado);

        }

        

        [HttpGet]
        [Route("v1/empresas/{codigo}")]
        [Authorize()]
        public EmpresaCommandResult ObterPorCodigo(string codigo)
        {
            return _empresaRepository.ObterPorCodigo(codigo);
        }



        #endregion

    }
}
