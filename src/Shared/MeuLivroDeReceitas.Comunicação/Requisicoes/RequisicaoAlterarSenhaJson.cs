using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Comunicacao.Requisicoes;

public class RequisicaoAlterarSenhaJson
{
    public string SenhaAtual {  get; set; } 
    public string NovaSenha {  get; set; }

}
