using System;
using System.Collections.Generic;
using System.Text;

namespace CourierBA.Models
{
    public class Elemento
    {
        public string Tracking { get; set; }
        public string Correlativo { get; set; }
        public string Estado { get; set; }

    }

    public class ListaElemntos
    {
        public List<Elemento> _elementos { get; set; }

        public ListaElemntos()
        {
            _elementos = new List<Elemento>();
            LoadElementos();
        }

        private void LoadElementos()
        {
            _elementos.Add(new Elemento 
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });
            _elementos.Add(new Elemento
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });
            _elementos.Add(new Elemento
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });
            _elementos.Add(new Elemento
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });
            _elementos.Add(new Elemento
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });
            _elementos.Add(new Elemento
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });
            _elementos.Add(new Elemento
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });
            _elementos.Add(new Elemento
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });
            _elementos.Add(new Elemento
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });
            _elementos.Add(new Elemento
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });
            _elementos.Add(new Elemento
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });
            _elementos.Add(new Elemento
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });
            _elementos.Add(new Elemento
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });
            _elementos.Add(new Elemento
            {
                Tracking = "Tracking: 89749687496874",
                Correlativo = "Correlativo: 874787",
                Estado = "Estado: En proceso",
            });

        }
    }
}
