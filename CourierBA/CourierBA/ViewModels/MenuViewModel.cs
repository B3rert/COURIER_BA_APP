using CourierBA.Models;
using CourierBA.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CourierBA.ViewModels
{
    public class MenuViewModel : BindableBase
    {
       
        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private ObservableCollection<Models.Menu> _menus;

        public ObservableCollection<Models.Menu> Menus
        {
            get => _menus;
            set => SetProperty(ref _menus, value);
        }

        private DelegateCommand<Models.Menu> _executeCommand;
        public string _user { get; set; }
        public DelegateCommand<Models.Menu> ExecuteCommand => _executeCommand ?? (_executeCommand = new DelegateCommand<Models.Menu>(SetAction));
        public MenuViewModel()
        {
            
        }

        public async void LoadData()
        {
            try
            {
                var response = await GetRootMenu(_user);
                if (!response.IsSuccess)
                {
                    return;
                }
                var options = response.Result.Table
                    .Select(async s => new Models.Menu
                    {
                        Icon = "",
                        Name = s.Name,
                        PageName = s.Name,
                        ApplicationId = s.Application,
                        Children = await LoadChildrenData(s.Application)
                    })
                    .ToList();

                var result = await Task.WhenAll(options);
                Menus = new ObservableCollection<Models.Menu>(result.ToList());
                IsRunning = false;
            }
            catch (Exception e)
            {
                var msg = e.Message;
                IsRunning = false;
            }
        }
        public async Task<ObservableCollection<Models.Menu>> LoadChildrenData(int item)
        {
            try
            {
                var response = await GetMenu(item, _user);
                if (!response.IsSuccess)
                {
                    return new ObservableCollection<Models.Menu>();
                }
                return GenerateGrupos(response.Result.Table.ToList());

            }
            catch (Exception e)
            {
                var msg = e.Message;
                return new ObservableCollection<Models.Menu>();
            }
        }

        private ObservableCollection<Models.Menu> GenerateGrupos(List<MenuResponse> data)
        {
            List<Models.Menu> detalles = new List<Models.Menu>();

            //Se generan los padres
            data.Where(w => w.User_Display_Father == null).ForEach(d =>
            {
                detalles.Add(new Models.Menu
                {
                    Icon = "",
                    PageName = d.Name,
                    Name = d.Name,
                    Children = GenerateChildren(d.User_Display, data)
                });
            });

            return new ObservableCollection<Models.Menu>(detalles);

        }

        private ObservableCollection<Models.Menu> GenerateChildren(int idPadre, List<MenuResponse> data)
        {
            var childrens = data.Where(w => w.User_Display_Father == idPadre).ToList();
            if (childrens.Count > 0)
            {
                return new ObservableCollection<Models.Menu>(childrens.Select(s => new Models.Menu
                {
                    Icon = "",
                    PageName = s.Name,
                    Name = s.Name,
                    Children = GenerateChildren(s.User_Display, data)
                }));
            }
            else
            {
                return new ObservableCollection<Models.Menu>();
            }
        }

        private  void SetAction(Models.Menu item)
        {
            if (item.Children.Count > 0 )
            {
                
                
                return;

                //await _dialogService.DisplayAlertAsync("Con Hijos", "Yo tengo hijos, solo puedes navegar en mis hijos", "OK");
            }
            else
            {
                /*
                Device.BeginInvokeOnMainThread(() => {
                    var _mp = Application.Current.MainPage as MasterDetailPage;
                    _mp.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(TestDetailPage)));

                    //aquí tu navegación
                });
               0
                */

                //(Application.Current.MainPage as MasterDetailPage).Detail 
                //  = new NavigationPage((Page)Activator.CreateInstance(TestDetailPage));

                return;
                //await _dialogService.DisplayAlertAsync("Sin Hijos", "Yo no tengo hijos, puedes mandar a otra pagina o hacer alguna otra cosa", "OK");
            }

        }

        #region API
        public async Task<Response<ResponseApi>> GetMenu(int id, string user)
        {
            try
            {

                var client = new HttpClient();

                var url = $"http://190.149.177.249:81/api/PA_bsc_User_Display_2?user={user}&application={id}";
                UriBuilder builder = new UriBuilder(url);
                //builder.Query = request.IdCliente <= 0 ? string.Format("id={0}", request.id) : string.Format("idCliente={0}", request.IdCliente);

                var response = await client.GetAsync(builder.Uri);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<ResponseApi>
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<string>(result)
                    };
                }

                var transacciones = JsonConvert.DeserializeObject<ResponseApi>(result);
                return new Response<ResponseApi>
                {
                    IsSuccess = true,
                    Result = transacciones
                };
            }
            catch (Exception ex)
            {
                string msgError;
                switch (ex.Message)
                {
                    case "No such host is known":
                        msgError = "No se encontro el servidor";
                        break;
                    default:
                        msgError = "No se pudo conectar al servidor";
                        break;
                }
                return new Response<ResponseApi>
                {
                    IsSuccess = false,
                    Message = msgError
                };
            }
        }

        public async Task<Response<RootMenuResponse>> GetRootMenu(string user)
        {
            try
            {

                var client = new HttpClient();

                var url = $"http://190.149.177.249:81/api/PA_bsc_User_Aplication?user={user}";
                UriBuilder builder = new UriBuilder(url);
                //builder.Query = request.IdCliente <= 0 ? string.Format("id={0}", request.id) : string.Format("idCliente={0}", request.IdCliente);

                var response = await client.GetAsync(builder.Uri);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<RootMenuResponse>
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<string>(result)
                    };
                }

                var transacciones = JsonConvert.DeserializeObject<RootMenuResponse>(result);
                return new Response<RootMenuResponse>
                {
                    IsSuccess = true,
                    Result = transacciones
                };
            }
            catch (Exception ex)
            {
                string msgError;
                switch (ex.Message)
                {
                    case "No such host is known":
                        msgError = "No se encontro el servidor";
                        break;
                    default:
                        msgError = "No se pudo conectar al servidor";
                        break;
                }
                return new Response<RootMenuResponse>
                {
                    IsSuccess = false,
                    Message = msgError
                };
            }
        }
        #endregion

    }
}
