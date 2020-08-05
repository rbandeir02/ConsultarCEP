using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ConsultarCEP.Servico.Modelo;
using Newtonsoft.Json;
using AudioUnit;

namespace ConsultarCEP.Servico
{
    


    [PreserveAttribute(AllMembers = true)]
    public class VIACepServico
    {

        private static string EnderecoURL = "http://viacep.com.br/ws/{0}/json/";

        public static async System.Threading.Tasks.Task<Endereco> BuscaEnderecoViaCEPAsync(string cep)
        {
            string NovoEnderecoURL = string.Format(EnderecoURL, cep);

            WebClient wc =new WebClient();
            string conteudo = await wc.DownloadStringTaskAsync(new Uri(NovoEnderecoURL));
                      

            Endereco end = JsonConvert.DeserializeObject<Endereco>(conteudo);

            if (end.cep == null)
                return null;
            else
                return end;
        }

    }
}
