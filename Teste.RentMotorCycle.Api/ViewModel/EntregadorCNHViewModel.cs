using System;
using System.Reflection;
using MongoDB.Bson;

namespace Test.RentMotorCycle.Api.ViewModel;

public class EntregadorCNHViewModel
{
    public string imagem_cnh { get; set; }

    public bool Validate()
    {
        if (this.imagem_cnh == null) throw new ArgumentNullException(nameof(this.imagem_cnh));
        if (!ValidExtension(this.imagem_cnh)) throw new IOException("Imagens no formato PNG ou BMP.");
        if (!IsBase64String(this.imagem_cnh)) throw new IOException("Arquivo inválido.");
        return true;
    }


    bool ValidExtension(string base64String)
    {
        int pos = base64String.IndexOf("base64,");
        pos = pos != -1 ? pos + 7 : 0;
        var data = pos == 0 ? base64String : base64String.Substring(pos, base64String.Length - pos);
        var dataextension = base64String.Substring(pos, 1);
        String[] files = ["I", "Q"];
        return files.Contains(dataextension.ToUpper());
    }

    bool IsBase64String(string base64String)
    {
        try
        {
            int pos = base64String.IndexOf("base64,");
            pos = pos != -1 ? pos + 7 : 0;
            var data = pos == 0 ? base64String : base64String.Substring(pos, base64String.Length - pos);

            Convert.FromBase64String(data);
            return true;
        }
        catch { }
        return false;
    }


}
