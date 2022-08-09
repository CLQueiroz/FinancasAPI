using FinanceApp.Api.Contexto;
using FinanceApp.Api.Exceptions;
using FinanceApp.Api.Interfaces;
using FinanceApp.Api.Mensagens;
using FinanceApp.Api.Models;
using FinanceApp.Api.Models.DTOs;
using FinanceApp.Api.Utils;
using FinanceApp.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceApp.Api.Repositories
{
    /// <summary>
    /// Repositório de usuários, para realizar as operações no banco
    /// </summary>
    public class UsuarioRepository : IUsuario
    {
        private readonly ApplicationContext _context;
        private readonly ILog _log;

        public UsuarioRepository(ApplicationContext context, ILog log)
        {
            _context = context;
            _log = log;
        }


        /// <summary>
        /// Todos usuários cadastrados
        /// </summary>
        public List<Usuario> BuscaUsuarios()
        {
            try
            {
                List<Usuario> usuarios = _context.Usuario.ToList();
                if (Validacoes.IsZero(usuarios.Count))
                {
                    throw new DomainException(Mensagens.MensagemRetorno.NaoExisteRegistros);
                }
                _log.MontaLog("GET /api/usuarios", "Lista de usuários");
                return usuarios;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Retornando o usuário pelo ID
        /// </summary>
        public Usuario BuscaUsuario(int id)
        {
            Usuario usuario = null;
            try
            {
                if (Validacoes.IsZero(id))
                {
                    throw new DomainException(MensagemRetorno.ParametroNaoPermitido);
                }

                usuario = _context.Usuario.Where(i => i.Id == id).FirstOrDefault();

                if (Validacoes.isNotNull(usuario))
                {
                    _log.MontaLog($"GET /api/usuarios/{id}", $"Retornou o Usuarío {usuario.Nome}");
                    return usuario;
                }

                throw new ArgumentException(MensagemRetorno.RegistroNaoEncontrado);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Realiza o cadastro do usuário
        /// </summary>
        public Usuario CadastraUsuario(CadastroUsuarioDTO modelo)
        {
            try
            {
                // verifica se o e-mail esta correto
                if (!Validacoes.EmailValido(modelo.Email))
                    throw new DomainException(MensagemRetorno.EmailInvalido);

                // verifica se o e-mail ja esta cadastrado
                if (EmailJaEstaCadastrado(modelo.Email))
                    throw new DomainException(MensagemRetorno.EmailJaCadastrado);

                // verificando se os dados não estão nullos ou vazio.
                if (String.IsNullOrEmpty(modelo.Nome) || String.IsNullOrEmpty(modelo.Email) || String.IsNullOrEmpty(modelo.Senha))
                    throw new DomainException(MensagemRetorno.CamposObrigatorios);

                // criando objeto usuário
                Usuario novoUsuario = new Usuario { Nome = modelo.Nome.ToString(), Email = modelo.Email.ToString(), Senha = CriptografarMD5.Criar(modelo.Senha).ToString() };

                // adiciona e salva o usuário
                if (Validacoes.isNotNull(novoUsuario))
                {
                    _context.Usuario.Add(novoUsuario);
                    _context.SaveChanges();
                    _log.MontaLog($"POST /api/usuarios", $"Cadastro novo usuário {novoUsuario.Nome}");
                }

                return novoUsuario;
            }
            catch (Exception)
            {
                throw;
            }            

        }


        /// <summary>
        /// Deleta usuário
        /// </summary>        
        public bool DeletaUsuario(int id)
        {
            try
            {
                if (UsuarioExiste(id))
                {
                    Usuario usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);
                
                    if (Validacoes.isNotNull(usuario))
                    {
                        _context.Usuario.Remove(usuario);
                        _context.SaveChanges();
                        _log.MontaLog($"DELETE /api/usuarios/{id}", $"Deletou o usuário {usuario.Email}");
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
        /// Atualiza o usuário
        /// </summary>
        public IActionResult AtualizaUsuario()
        {
            throw new NotImplementedException();
        }


        #region Métodos auxiliares

        /// <summary>
        /// Verifica se o usuário existe
        /// </summary>
        public bool UsuarioExiste(int id)
        {
            return _context.Usuario.Any(u => u.Id == id);
        }

        /// <summary>
        /// Verifica se o e-mail ja esta cadastrado.
        /// </summary>
        public bool EmailJaEstaCadastrado(string email)
        {
            return _context.Usuario.Any(u => u.Email == email);
        }      

        #endregion
    }
}
