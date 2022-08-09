using FinanceApp.Api.Contexto;
using FinanceApp.Api.DTOs;
using FinanceApp.Api.Exceptions;
using FinanceApp.Api.Interfaces;
using FinanceApp.Api.Mensagens;
using FinanceApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceApp.Api.Repositories
{
    using FinanceApp.Api.Utils;

    public class DespesaRepository : IDespesa
    {
        private readonly ApplicationContext _context;
        private readonly IUsuario _usuario;
        private readonly ILog _log;

        public DespesaRepository(ApplicationContext context, IUsuario usuario, ILog log)
        {
            _context = context;
            _usuario = usuario;
            _log = log;
        }


        /// <summary>
        /// Retornar todas as despesas
        /// </summary>
        public List<Despesa> BuscaDespesas()
        {
            try
            {
                return _context.Despesa.Include(d => d.Usuario).ToList();                
            }
            catch (DomainException)
            {
                throw;
            }
        }


        /// <summary>
        /// Retorna a despesa pelo id informado
        /// </summary>
        public Despesa BuscaDespesa(int id)
        {
            Despesa despesa = null;
            try
            {
                if (Validacoes.IsZero(id))
                {
                    throw new DomainException(MensagemRetorno.ParametroNaoPermitido);
                }

                despesa = _context.Despesa.AsNoTracking().Where(i => i.Id == id).FirstOrDefault();

                if (Validacoes.isNotNull(despesa))
                {
                    return despesa;
                }

                throw new ArgumentException(MensagemRetorno.RegistroNaoEncontrado);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Cadastra a despesa
        /// </summary>
        public Despesa CadastraDespesa(CadastroDespesaDTO modelo)
        {
            try
            {                
                if (!modelo.Validar())
                {                    
                    throw new DomainException(MensagemRetorno.CamposObrigatorios);
                }

                // veriifica se o usuário não existe
                if (!_usuario.UsuarioExiste(modelo.UsuarioId))
                {
                    throw new DomainException(MensagemRetorno.UsuarioNaoEncontrado);
                }

                // cria o objeto despesa
                Despesa novaDespesa = new Despesa{ Descricao = modelo.Descricao.ToString(), DataVencimento = modelo.DataVencimento, Valor = modelo.Valor, UsuarioId = modelo.UsuarioId, DespesaFixa = modelo.DespesaFixa};

                // salva a despesa
                if (Validacoes.isNotNull(novaDespesa))
                {
                    _context.Despesa.Add(novaDespesa);
                    _context.SaveChanges();
                }
                return novaDespesa;
            }
            catch (DomainException e)
            {
                throw;
            }
        }


        /// <summary>
        /// Deleta a despesa
        /// </summary>
        public bool DeletaDespesa(int id)
        {
            try
            {
                if (DespesaExiste(id))
                {
                    Despesa despesa = _context.Despesa.FirstOrDefault(u => u.Id == id);

                    if (Validacoes.isNotNull(despesa))
                    {
                        _context.Despesa.Remove(despesa);
                        _context.SaveChanges();
                        _log.MontaLog($"DELETE /api/despesas/{id}", $"Deletou a despesa {despesa.Descricao}");
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
        /// realiza a baixa da despesa
        /// </summary>
        /// <param name="id"></param>
        public bool BaixaDespesa(int id)
        {
            try
            {
                if (!DespesaExiste(id))
                {
                    return false;
                }

                Despesa despesa = BuscaDespesa(id);

                Despesa despesaAtualizada = new Despesa
                {
                    Id = despesa.Id,
                    Descricao = despesa.Descricao,
                    DataVencimento = despesa.DataVencimento,
                    Valor = despesa.Valor,
                    Baixada = true,
                    UsuarioId = despesa.UsuarioId
                };

                _context.Despesa.Attach(despesaAtualizada);
                _context.Entry(despesaAtualizada).Property(U => U.Baixada).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Atualiza a desppesa
        /// </summary>
        public IActionResult AtualizaDespesa()
        {
            throw new System.NotImplementedException();
        }


        #region Métodos Auxiliares

        /// <summary>
        /// Verifica se a despesa existe
        /// </summary>
        /// <param name="id">Tipo INT</param>
        /// <returns>True ou False</returns>
        public bool DespesaExiste(int id)
        {
            return _context.Despesa.Any(i => i.Id == id);
        }        
        #endregion
    }
}
