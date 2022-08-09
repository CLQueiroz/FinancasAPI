using FinanceApp.Api.Contexto;
using FinanceApp.Api.Exceptions;
using FinanceApp.Api.Interfaces;
using FinanceApp.Api.Mensagens;
using FinanceApp.Api.Models;
using FinanceApp.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySqlConnector;

namespace FinanceApp.Api.Repositories
{
    public class ReceitaRepository : IReceita
    {
        private readonly ApplicationContext _context;
        private readonly IUsuario _usuario;
        private readonly ILog _log;
        private readonly IConfiguration _config;

        public ReceitaRepository(ApplicationContext context, IUsuario usuario, ILog log, IConfiguration configuration)
        {
            _context = context;
            _usuario = usuario;
            _log = log;
            _config = configuration;
            var teste = "";
        }


        /// <summary>
        /// Buscar todas as receitas
        /// </summary>
        public List<Receita> BuscaReceitas()
        {
            try
            {
                List<Receita> queryReceita = new List<Receita>();
                List<Receita> retornoReceita = new List<Receita>();

                using (MySqlConnection connection = new MySqlConnection(_config.GetConnectionString("Default")))
                {
                    queryReceita = connection.Query<Receita>("SELECT * FROM vw_busca_receitas LIMIT 1000", buffered: false).ToList();
                }

                if (queryReceita?.Count() > 0)
                {
                    retornoReceita = (from a in queryReceita
                                       select new Receita
                                        {
                                            Id = a.Id,
                                            Data = a.Data,
                                            Descricao = a.Descricao,
                                            UsuarioId = a.UsuarioId,
                                            Valor = a.Valor,
                                        }).ToList();
                }
                return retornoReceita;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Buscar receita por ID
        /// </summary>
        public Receita BuscaReceita(int id)
        {
            Receita receita = null;
            try
            {
                if (Validacoes.IsZero(id))
                {
                    throw new DomainException(MensagemRetorno.ParametroNaoPermitido);
                }

                receita = _context.Receita.Where(i => i.Id == id).FirstOrDefault();

                if (Validacoes.isNotNull(receita))
                {
                    return receita;
                }

                throw new ArgumentException(MensagemRetorno.RegistroNaoEncontrado);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Cadastra a receita
        /// </summary>
        public Receita CadastraReceita(CadastroReceitaDTO modelo)
        {
            try
            {
                // verificando se os dados não estão nullos ou vazio.
                if (String.IsNullOrEmpty(modelo.Descricao) || String.IsNullOrEmpty(modelo.Data.ToString()) || String.IsNullOrEmpty(modelo.UsuarioId.ToString()))
                {
                    throw new DomainException(MensagemRetorno.CamposObrigatorios);
                }

                // veriifica se o usuário não existe
                if (!_usuario.UsuarioExiste(modelo.UsuarioId))
                {
                    throw new ArgumentException(MensagemRetorno.RegistroNaoEncontrado);
                }

                // cria o objeto receita
                Receita novaReceita = new Receita { Descricao = modelo.Descricao.ToString(), Data = modelo.Data, UsuarioId = modelo.UsuarioId, Valor = (double)modelo.Valor };

                // Adiciona e salva a despesa
                if (Validacoes.isNotNull(novaReceita))
                {
                    _context.Receita.Add(novaReceita);
                    _context.SaveChanges();
                }
                return novaReceita;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Deleta a receita
        /// </summary>
        public bool DeletaReceita(int id)
        {
            try
            {
                if (ReceitaExiste(id))
                {
                    Receita receita = _context.Receita.FirstOrDefault(u => u.Id == id);

                    if (Validacoes.isNotNull(receita))
                    {
                        _context.Receita.Remove(receita);
                        _context.SaveChanges();
                        return true;
                    }
                }
                throw new DomainException(MensagemRetorno.RegistroNaoEncontrado);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Atualiza a receita /TODO
        /// </summary>
        public IActionResult AtualizaReceita()
        {
            throw new System.NotImplementedException();
        }


        #region Métodos auxiliares


        /// <summary>
        /// Verifica se a receita existe
        /// </summary>
        public bool ReceitaExiste(int id)
        {
            return _context.Receita.Any(u => u.Id == id);
        }

        #endregion
    }
}
