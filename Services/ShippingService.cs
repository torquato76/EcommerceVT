﻿using EcommerceVT.Interface;
using EcommerceVT.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EcommerceVT.Services
{
    public class ShippingService : IShipping
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<Shipping> Get(int zipCodeSender, int zipCodeRecipient, int productWeight = 1,
            int productLength = 20, int productheight = 10, int productWidth = 20, int productValue = 0)
        {

            var url = new StringBuilder();
            url.Append("https://ws.correios.com.br/calculador/CalcPrecoPrazo.aspx?");
            url.Append("nCdEmpresa=");
            url.Append("&sDsSenha=");
            url.Append($"&sCepOrigem={zipCodeSender}");
            url.Append($"&sCepDestino={zipCodeRecipient}");
            url.Append($"&nVlPeso={productWeight}");
            url.Append("&nCdFormato=1");
            url.Append($"&nVlComprimento={productLength}");
            url.Append($"&nVlAltura={productheight}");
            url.Append($"&nVlLargura={productWidth}");
            url.Append("&sCdMaoPropria=n");
            url.Append($"&nVlValorDeclarado={productValue}");
            url.Append("&sCdAvisoRecebimento=n");
            url.Append("&nCdServico=04510");
            url.Append("&nVlDiametro=0");
            url.Append("&StrRetorno=xml");
            url.Append("&nIndicaCalculo=3");

            var doc = new XmlDocument();
            doc.Load(url.ToString());

            var shipping = new Shipping();
            shipping.freteValor = doc.GetElementsByTagName("Valor").Item(0).InnerText;
            shipping.prazoEntrega = doc.GetElementsByTagName("PrazoEntrega").Item(0).InnerText; ;

            return shipping;
        }
    }
}
