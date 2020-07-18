using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ConsultarCEP.Servico.Modelo;
using ConsultarCEP.Servico;
using System.Text.RegularExpressions;
using VersionAndBuildNumber.DependencyServices;
using Xamarin.Essentials;

namespace ConsultarCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;

            //lblVersionNumber.Text = DependencyService.Get<IAppVersionAndBuild>().GetVersionNumber();
            //lblVersionNumber.Text = Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName;
            lblVersionNumber.Text = AppInfo.VersionString;
            //lblBuildNumber.Text = DependencyService.Get<IAppVersionAndBuild>().GetBuildNumber();
            lblBuildNumber.Text = AppInfo.BuildString;

        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            //TODO - lógica

            //TODO - Validações
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = VIACepServico.BuscaEnderecoViaCEP(cep);
                    if (end != null)
                        RESULTADO.Text = string.Format("Endereço: {2} de {3} - {0},{1} ", end.localidade, end.uf, end.logradouro, end.bairro);
                    else
                    {
                        DisplayAlert("Erro", "O endereço não foi encontrado para o cep " + cep, "OK");
                        RESULTADO.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("Erro Crítico", ex.Message, "OK");
                    RESULTADO.Text = "";
                }
            }
            else
            {
                DisplayAlert("Erro", "CEP Inválido.", "OK");
            }
        }

        private bool isValidCEP(string cep)
        {
            cep = cep.Replace(".", "");
            cep = cep.Replace("-", "");
            cep = cep.Replace(" ", "");

            Regex Rgx = new Regex(@"^\d{8}$");
            if (!Rgx.IsMatch(cep))
                return false;
            else
                return true;
        }

        private void VerificarDigito(object sender, EventArgs args)
        {
            if (CEP.Text.Length == 8)
            {
                BuscarCEP(sender, args);
            }
        }
    }
}
