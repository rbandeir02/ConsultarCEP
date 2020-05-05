using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ConsultarCEP.Servico.Modelo;
using Newtonsoft.Json;

namespace ConsultarCEP.Servico
{
    public class VIACepServico
    {

        private static string EnderecoURL = "http://viacep.com.br/ws/{0}/json/";

        public static Endereco BuscaEnderecoViaCEP(string cep)
        {
            string NovoEnderecoURL = string.Format(EnderecoURL, cep);

            WebClient wc =new WebClient();
            string conteudo = wc.DownloadString(NovoEnderecoURL);

            Endereco end = JsonConvert.DeserializeObject<Endereco>(conteudo);

            if (end.cep == null)
                return null;
            else
                return end;
        }

    }
}
