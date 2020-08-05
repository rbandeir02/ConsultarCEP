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
using Android.Hardware;
using Acr.UserDialogs;
using ClipboardDemo.Interfaces;

namespace ConsultarCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        string cep = "";
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = this;
            // Define a propriedade
            this.IsBusy = false;

            // BOTAO.Clicked += BuscarCEP;

            BOTAO.Clicked += async (sender, e) =>
            {

                  UserDialogs.Instance.ShowLoading("Carregando...");
                  //await Task.Yield();
                  await BuscarCEP();
                  UserDialogs.Instance.HideLoading();

               /* this.IsBusy = true;
                await BuscarCEP();
                this.IsBusy = false;*/


            };

            btnClipboard.Clicked += async (sender, e) =>
            {
                var clipboardService = DependencyService.Get<IClipboardService>();
                clipboardService?.CopyToClipboard(RESULTADO.Text);
                await DisplayAlert("Consultar Cep", "Endereço do Cep "+ cep + " copiado!", "Ok");


            };

                //lblVersionNumber.Text = DependencyService.Get<IAppVersionAndBuild>().GetVersionNumber();
                //lblVersionNumber.Text = Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName;
                lblVersionNumber.Text = AppInfo.VersionString;
            //lblBuildNumber.Text = DependencyService.Get<IAppVersionAndBuild>().GetBuildNumber();
            lblBuildNumber.Text = AppInfo.BuildString;
            btnClipboard.IsEnabled = false;

        }

        public async Task BuscarCEP()
        {
            //TODO - lógica

            //TODO - Validações
            cep = CEP.Text.Trim();
            
            Endereco end;

            //await Aviso("Cep consultado antes:" + cep);
            if (isValidCEP(cep))
            {
                try
                {
                    //await Aviso("Cep consultado depois:" + cep);
                   end = await VIACepServico.BuscaEnderecoViaCEPAsync(cep);
                   Task.Delay(3000).Wait();


                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {2} de {3} - {0},{1} ", end.localidade, end.uf, end.logradouro, end.bairro);
                        btnClipboard.IsEnabled = true;
                    }
                    else
                    {
                        await DisplayAlert("Erro", "O endereço não foi encontrado para o cep " + cep, "OK");
                        RESULTADO.Text = "";
                        btnClipboard.IsEnabled = false;
                    }
                    //actInd.IsVisible = false;
                    //actInd.IsRunning = false;

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro Crítico", ex.Message, "OK");
                    //await DisplayAlert("Saída ", saida, "OK");
                    RESULTADO.Text = "";
                    //actInd.IsVisible = false;
                    //actInd.IsRunning = false;
                }
            }
            else
            {
                await DisplayAlert("Erro", "CEP Inválido.", "OK");
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

        private async 
        Task
Aviso(string txt)
        {
            await DisplayAlert("Aviso", txt, "OK");
        }

        private async void VerificarDigito(object sender, EventArgs args)
        {
            if (CEP.Text.Length == 8)
            {
                //actInd.IsVisible = true;
                //actInd.IsRunning = true;
                //BuscarCEP(sender, args);
            }
        }
    }
}
